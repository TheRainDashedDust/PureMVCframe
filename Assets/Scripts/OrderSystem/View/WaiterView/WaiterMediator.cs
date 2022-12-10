using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterMediator : Mediator
{
    private WaiterProxy waiterProxy=null;
    public new const string Name = "WaiterMediator";
    public WaiterView WaiterView
    {
        get { return (WaiterView)base.ViewComponent; }
    }
    private OrderProxy orderProxy = null;
    public WaiterMediator(WaiterView view):base(Name,view)
    {
        WaiterView.CallWaiter += () => { SendNotification(OrderSystemEvent.CALL_WAITER); };
        WaiterView.Order += (data) => { SendNotification(OrderSystemEvent.ORDER,data); };
        WaiterView.Pay += () => { SendNotification(OrderSystemEvent.PAY); };
        WaiterView.CallCook += () => { SendNotification(OrderSystemEvent.CALL_COOK); };
        WaiterView.ServerFood += (item) => { SendNotification(OrderCommandEvent.SELECT_WAITER,item,"WANSHI"); };
        //WaiterView.BusyAction += (item) => { SendNotification(OrderCommandEvent.SELECT_WAITER, item, "Rester"); };
        
    }
    public override void OnRegister()
    {
        base.OnRegister();
        waiterProxy=Facade.Instance.RetrieveProxy(WaiterProxy.Name) as WaiterProxy;
        orderProxy=Facade.Instance.RetrieveProxy(OrderProxy.Name) as OrderProxy;
        if (waiterProxy==null)
        {
            throw new System.Exception(WaiterProxy.Name + "is null");
        }
        if (orderProxy==null)
        {
            throw new System.Exception(OrderProxy.Name + "is null");
        }
        WaiterView.UpdateWaiter(waiterProxy.Waiters);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>() {
            OrderSystemEvent.CALL_WAITER,
            OrderSystemEvent.ORDER,
            OrderSystemEvent.GET_PAY,
            OrderSystemEvent.FOOD_TO_CLIENT,
            OrderSystemEvent.REFRESH_WAITER,
            
        };
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case OrderSystemEvent.CALL_WAITER:
                //呼叫服务员，上菜单
                ClientItem client=notification.Body as ClientItem;
                SendNotification(OrderCommandEvent.GET_ORDER,client,"Get");
                break;
            case OrderSystemEvent.ORDER:
                //上传菜单(正常流程，查找空闲服务员，提交菜单给厨师)
                SendNotification(OrderSystemEvent.CALL_COOK,notification.Body);
                break;
            case OrderSystemEvent.GET_PAY:
                //服务员拿到付款
                ClientItem item=notification.Body as ClientItem;
                SendNotification(OrderCommandEvent.GUEST_BE_AWAY, item, "Remove");
                
                break;
            case OrderSystemEvent.FOOD_TO_CLIENT:
                //服务员上菜
                WaiterItem waiterItem=notification.Body as WaiterItem;

                

                SendNotification(OrderCommandEvent.CHANGE_CLIENT_STATE, waiterItem.Order, "Eatting");
                
                SendNotification(OrderSystemEvent.PAY, waiterItem);

                //waiterItem.Order = null;
                //重新选择
                //SendNotification(OrderCommandEvent.SELECT_WAITER, waiterItem, "Rester");
                break;
            case OrderSystemEvent.REFRESH_WAITER:
                //刷新服务员状态
                waiterProxy=Facade.Instance.RetrieveProxy(WaiterProxy.Name) as WaiterProxy;
                WaiterView.Move(waiterProxy.Waiters);
                break;
                
            default:
                break;
        }
    }
}
