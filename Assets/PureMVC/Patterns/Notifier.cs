using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Notifier : INotifier
{
    /// <summary>
    /// 不使用命名空间，Facade可能直接引用本类的同名属性
    /// </summary>
    private IFacade m_facade=Facade.Instance;
    
    public void SendNotification(string notificationName)
    {
      
        this.m_facade.SendNotification(notificationName);
    }

    public void SendNotification(string notificationName, object body)
    {
        this.m_facade.SendNotification(notificationName,body);
    }

    public void SendNotification(string notificationName, object body, string type)
    {
        this.m_facade.SendNotification(notificationName, body,type);
    }
    protected IFacade FacadeInstance
    {
        get { return this.m_facade; }
    }
}

