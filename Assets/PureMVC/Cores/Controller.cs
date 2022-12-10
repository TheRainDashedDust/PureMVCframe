using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller :IController
{
    protected IDictionary<string,Type> m_commandMap=new Dictionary<string,Type>();
    protected static volatile IController m_instance;
    protected static readonly object m_staticSyncRoot=new object();
    protected readonly object m_syncRoot=new object();
    protected IView m_view;

    public Controller()
    {
        this.InitializeController();
    }

    protected virtual void InitializeController()
    {
        this.m_view = View.Instance;
    }

    public static IController Instance
    {
        get
        {
            if (m_instance==null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance==null)
                    {
                        m_instance=new  Controller();
                    }
                }
            }
            return m_instance;
        }
    }

    public virtual void ExecuteCommand(INotification note)
    {
        Type type = null;
        lock (this.m_syncRoot)
        {
            if (!this.m_commandMap.ContainsKey(note.Name))
            {
                return;
            }
            type = this.m_commandMap[note.Name];
        }
        object obj=Activator.CreateInstance(type);
        if (obj is ICommand)
        {
            ((ICommand)obj).Execute(note);
        }


    }

    public virtual bool HasCommand(string notificationName)
    {
        lock(this.m_syncRoot)
        {
            return this.m_commandMap.ContainsKey(notificationName);
        }
    }

    public virtual void RegisterCommand(string notificationName, Type commandType)
    {
        lock(this.m_syncRoot)
        {
            if (!this.m_commandMap.ContainsKey(notificationName))
            {
                this.m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
            }
            this.m_commandMap[notificationName] = commandType;
        }
    }

    public virtual void RemoveCommand(string notificationName)
    {
        lock (this.m_syncRoot)
        {
            if (this.m_commandMap.ContainsKey(notificationName))
            {
                this.m_view.RemoveObserver(notificationName, this);
                this.m_commandMap.Remove(notificationName);
            }
        }
    }
}
