using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public class Model : IModel
{
    protected static volatile IModel m_instance;
    protected IDictionary<string,IProxy> m_proxyMap=new Dictionary<string,IProxy>();
    protected readonly object m_syncRoot = new object();
    protected static readonly object m_staticSyncRoot=new object();

    public Model()
    {
        this.InitializeModel();
    }
    public static IModel Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance==null)
                    {
                        m_instance = new Model();
                    }
                }
            }
            return m_instance;  
        }
    }
    protected virtual void InitializeModel()
    {

    }
    public virtual bool HasProxy(string proxyName)
    {
        lock(this.m_syncRoot)
        {
            return this.m_proxyMap.ContainsKey(proxyName);
        }
    }

    public virtual void RegisterProxy(IProxy proxy)
    {
        lock(this.m_syncRoot)
        {
            this.m_proxyMap[proxy.ProxyName] = proxy;
        }
        proxy.OnRegister();
    }

    public virtual IProxy RemoveProxy(string proxyName)
    {
        IProxy proxy = null;
        lock(this.m_syncRoot)
        {
            if (this.m_proxyMap.ContainsKey(proxyName))
            {
                proxy = this.RetrieveProxy(proxyName);
                this.m_proxyMap.Remove(proxyName);
            }
        }
        if (proxy != null)
        {
            proxy.OnRemove();
        }
        return proxy;
    }

    public virtual IProxy RetrieveProxy(string proxyName)
    {
        lock (this.m_syncRoot)
        {
            if (!this.m_proxyMap.ContainsKey(proxyName))
            {
                return null;
            }
            return this.m_proxyMap[proxyName];
        }
    }
}
