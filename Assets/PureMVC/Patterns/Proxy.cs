using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Proxy : Notifier, IProxy, INotifier
{
    protected object m_data;
    protected string m_proxyName;
    public static string Name = "Proxy";

    public Proxy():this(Name,null)
    {
        //this.OnRegister();
    }

    public Proxy(string proxyName):this(proxyName,null)
    {
        //this.OnRegister();
    }

    public Proxy( string proxyName, object data)
    {
        this.m_proxyName = (proxyName!=null)?proxyName:Name;
        if (data!=null)
        {
            this.m_data = data;
        }
        //this.OnRegister();
    }

    public object Data { get => this.m_data; set => this.m_data=value; }

    public string ProxyName
    {
        get
        {
            return this.m_proxyName;
        }
    }

    public virtual void OnRegister()
    {
        
    }

    public virtual void OnRemove()
    {
        
    }
}

