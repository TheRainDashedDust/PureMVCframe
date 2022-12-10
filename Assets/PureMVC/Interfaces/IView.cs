using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IView
{
    //判断是否存在中介者
    bool HasMediator(string mediatorName);
    //注册中介者
    void RegisterMediator(IMediator mediator);
    //移除中介者
    IMediator RemoveMediator(string mediatorName);
    //恢复中介者
    IMediator RetrieveMediator(string mediatorName);
    //向观察者下发事件通知
    void NotifyObservers(INotification notification);
    //注册观察者
    void RegisterObserver(string notificationName,IObserver observer);
    //移除观察者
    void RemoveObserver(string notificationName,object notifyContext);
}
