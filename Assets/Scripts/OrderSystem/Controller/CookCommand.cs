using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CookCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        CookProxy cookProxy=FacadeInstance.RetrieveProxy(CookProxy.Name) as CookProxy;
        
        if (notification.Type=="Busy")
        {
            Order order = notification.Body as Order;
            cookProxy.CookCooking(order);
        }
        else if(notification.Type == "Rester")
        {
            CookItem item = notification.Body as CookItem;
            cookProxy.ChangeCookerState(item);
        }
        
    }
}

