using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public class Mediator : Notifier, IMediator
{
    protected string m_mediatorName;
    protected object m_viewComponent;
    public const string Name = "Mediator";

    public Mediator():this(Name,null)
    {
    }

    public Mediator(string mediatorName):this(mediatorName,null)
    {
        
    }

    public Mediator(string mediatorName, object viewComponent)
    {
        this.m_mediatorName = (mediatorName!=null)?mediatorName:Name;
        m_viewComponent = viewComponent;
    }

    public string MediatorName => this.m_mediatorName;

    public object ViewComponent { get => this.m_viewComponent; set => this.m_viewComponent=value; }

    public virtual void HandleNotification(INotification notification)
    {
        
    }

    public virtual IList<string> ListNotificationInterests()
    {
        return new List<string>();
    }

    public virtual void OnRegister()
    {
        
    }

    public virtual void OnRemove()
    {
        
    }
}

