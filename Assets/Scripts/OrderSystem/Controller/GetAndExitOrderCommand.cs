using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class GetAndExitOrderCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        OrderProxy orderProxy = FacadeInstance.RetrieveProxy("OrderProxy") as OrderProxy;
        MenuProxy menuProxy = FacadeInstance.RetrieveProxy("MenuProxy") as MenuProxy;
        Debug.Log(notification.Type);
        if (notification.Type=="Get")
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Menus);
            orderProxy.AddOrder(order);
            SendNotification(OrderSystemEvent.UPMENU, order);
        }
        else if(notification.Type=="Exit")
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Menus);
            orderProxy.RemoveOrder(order);
        }
    }
}

