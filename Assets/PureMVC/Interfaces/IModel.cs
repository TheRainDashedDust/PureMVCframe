using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IModel
{
    //是否存在代理
    bool HasProxy(string proxyName);
    //注册代理
    void RegisterProxy(IProxy proxy);
    //移除代理
    IProxy RemoveProxy(string proxyName);
    //恢复代理
    IProxy RetrieveProxy(string proxyName);
}

