using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaiterCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        WaiterProxy waiterProxy=FacadeInstance.RetrieveProxy(WaiterProxy.Name) as WaiterProxy;
        if (notification.Type=="WANSHI")
        {
            Debug.Log("服务员休息中");
            
            waiterProxy.RemoveWaiter(notification.Body as WaiterItem);
        }
        else if(notification.Type=="SERVING")
        {
            Debug.Log("寻找服务员上菜");
            waiterProxy.ChangeWaiterServing(notification.Body as Order);
        }
        /*else if(notification.Type=="Rester")
        {
            waiterProxy.ChangeWaiterState(notification.Body as WaiterItem);
        }*/
    }
}

