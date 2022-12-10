using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : IView
{
    protected static volatile IView m_instance;
    protected IDictionary<string,IMediator> m_mediatorMap=new Dictionary<string,IMediator>();
    protected IDictionary<string,IList<IObserver>> m_observerMap=new Dictionary<string,IList<IObserver>>();
    protected static readonly object m_staticSyncRoot=new object();
    protected readonly object m_syncRoot = new object();

    public View()
    {
        this.InitializeView();
    }
    public static IView Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance==null)
                    {
                        m_instance = new View();
                    }
                }
            }
            return m_instance;
        }
    }
    protected virtual void InitializeView()
    {

    }
    public virtual bool HasMediator(string mediatorName)
    {
        lock (m_syncRoot)
        {
            return this.m_mediatorMap.ContainsKey(mediatorName);
        }
    }

    public virtual void NotifyObservers(INotification notification)
    {
        IList<IObserver> list = null;
        lock (this.m_syncRoot)
        {
            if (this.m_observerMap.ContainsKey(notification.Name))
            {
                IList<IObserver> observers = this.m_observerMap[notification.Name];
                list=new List<IObserver>(observers);
            }
        }
        if (list!=null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].NotifyObserver(notification);
            }
        }
    }

    public virtual void RegisterMediator(IMediator mediator)
    {
        lock(m_syncRoot)
        {
            if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
            {
                return;
            }
            this.m_mediatorMap[mediator.MediatorName] = mediator;
            IList<string> list=mediator.ListNotificationInterests();
            if (list.Count>0)
            {
                IObserver observer = new Observer("handleNotification", mediator);
                for (int i = 0; i < list.Count; i++)
                {
                    this.RegisterObserver(list[i].ToString(), observer);
                }
            }

        }
        mediator.OnRegister();
    }

    public virtual void RegisterObserver(string notificationName, IObserver observer)
    {
        lock (m_syncRoot)
        {
            if (!this.m_observerMap.ContainsKey(notificationName))
            {
                this.m_observerMap[notificationName] = new List<IObserver>();
            }
            if (!this.m_observerMap[notificationName].Contains(observer))
            {
                this.m_observerMap[notificationName].Add(observer);
            }
            
        }
    }

    public virtual IMediator RemoveMediator(string mediatorName)
    {
        IMediator notifyContext = null;
        lock (this.m_syncRoot)
        {
            if (!this.m_mediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }
            notifyContext = this.m_mediatorMap[mediatorName];
            IList<string> list = notifyContext.ListNotificationInterests();
            for (int i = 0; i < list.Count; i++)
            {
                this.RemoveObserver(list[i], notifyContext);
            }
            this.m_mediatorMap.Remove(mediatorName);

        }
        if (notifyContext!=null)
        {
            notifyContext.OnRemove();
        }
        return notifyContext;
    }

    public virtual void RemoveObserver(string notificationName, object notifyContext)
    {
        lock (this.m_syncRoot)
        {
            if (this.m_observerMap.ContainsKey(notificationName))
            {
                IList<IObserver> list = this.m_observerMap[notificationName];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].CompareNotifyContext(notifyContext))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                if (list.Count==0)
                {
                    this.m_observerMap.Remove(notificationName);
                }
            }
        }
    }

    public virtual IMediator RetrieveMediator(string mediatorName)
    {
        lock(this.m_syncRoot)
        {
            if (!this.m_mediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }
            return this.m_mediatorMap[mediatorName];
        }
    }

    
}
