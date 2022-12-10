using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFacade:INotifier
{
    //判断
    bool HasCommand(string notificationName);
    bool HasMediator(string mediatorName);
    bool HasProxy(string proxyName);

    //注册
    void RegisterCommand(string notificationName, Type commandType);
    void RegisterMediator(IMediator mediator);
    void RegisterProxy(IProxy proxy);

    //删除
    void RemoveCommand(string notificationName);
    IMediator RemoveMediator(string mediatorName);
    IProxy RemoveProxy(string proxyName);

    //恢复
    IMediator RetrieveMediator(string mediatorName);
    IProxy RetrieveProxy(string proxyName);

    //通知观察者
    void NotifyObservers(INotification notification);
}
