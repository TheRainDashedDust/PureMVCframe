using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Observer : IObserver
{
    private object m_notifyContext;
    private string m_notifyMethod;
    protected readonly object m_syncRoot=new object();

    public Observer(string notifyMethod,object notifyContext)
    {
        m_notifyMethod = notifyMethod;
        m_notifyContext = notifyContext;
        
    }
    
    public object NotifyContext 
    {
        private get
        {
            return this.m_notifyContext;
        }
        set
        {
            this.m_notifyContext = value;
        }
    }
    public string NotifyMethod 
    {
        private get
        {
            return this.m_notifyMethod;
        }
        set
        {
            this.m_notifyMethod = value;
        }
    }

    public bool CompareNotifyContext(object obj)
    {
        lock (m_syncRoot)
        {
            return this.NotifyContext.Equals(obj);
        }
    }

    public void NotifyObserver(INotification notification)
    {
        object notifyContext;
        lock (m_syncRoot)
        {
            notifyContext = this.NotifyContext;
        }

        //反射
        Type type= notifyContext.GetType();
        //设置查找条件
        BindingFlags bindingAttr=BindingFlags.Public| BindingFlags.Instance|BindingFlags.IgnoreCase;
        //根据方法名及条件查找对应方法发
        MethodInfo method=type.GetMethod(this.NotifyMethod, bindingAttr);
        method.Invoke(notifyContext, new object[] { notification });
    }
}
