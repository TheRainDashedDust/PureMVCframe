using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookMediator : Mediator
{
    
    private CookProxy cookProxy = null;
    public new const string Name = "CookMediator";
    public CookView CookView
    {
        get { return (CookView)base.ViewComponent; }
    }
    public CookMediator(CookView view):base (Name,view)
    {
        CookView.CallCook += () => { SendNotification(OrderSystemEvent.CALL_COOK); };
        CookView.ServerFood += (item) => { SendNotification(OrderSystemEvent.SERVER_FOOD, item); };
    }
    public override void OnRegister()
    {
        base.OnRegister();
        cookProxy=Facade.Instance.RetrieveProxy(CookProxy.Name) as CookProxy;
        if (cookProxy==null)
        {
            throw new System.Exception(CookProxy.Name + "is null.");
        }
        CookView.UpdateCook(cookProxy.Cooks);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>
        {
            OrderSystemEvent.CALL_COOK,
            OrderSystemEvent.SERVER_FOOD,
            OrderSystemEvent.REFRESH_COOK,
        };
        return notifications;
        
        
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case OrderSystemEvent.CALL_COOK:
                Order order = notification.Body as Order;
                if (order == null) { throw new System.Exception("order is null"); }
                Debug.Log("厨师收到前台订单，开始炒菜:" + order.names);
                SendNotification(OrderCommandEvent.COOK_COOKING , order,"Busy");
                break;
            case OrderSystemEvent.SERVER_FOOD:

                Debug.Log("厨师通知服务员上菜");
                CookItem cook = notification.Body as CookItem;
                //先刷新厨师状态，然后由代理proxy发起刷新

                //SendNotification(OrderSystemEvent.REFRESH_COOK);
                SendNotification(OrderCommandEvent.SELECT_WAITER, cook.cookOrder, "SERVING");
                cook.cookOrder = null;
                SendNotification(OrderCommandEvent.COOK_COOKING, cook, "Rester");
                
                
                break;
            case OrderSystemEvent.REFRESH_COOK:
                cookProxy = FacadeInstance.RetrieveProxy(CookProxy.Name) as CookProxy;
                if (cookProxy==null)
                {
                    throw new System.Exception(CookProxy.Name + "is null");
                }
                CookView.RefreshCook(cookProxy.Cooks);
                break;
            default:
                break;
        }
    }
}
