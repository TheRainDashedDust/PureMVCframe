using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IObserver
{
    //对比NotifyContext
    bool CompareNotifyContext(object obj);
    //通知观察者
    void NotifyObserver(INotification notification);
    //记录Mediator或者Command
    object NotifyContext { set; }
    //通知方法
    string NotifyMethod { set; }
}

