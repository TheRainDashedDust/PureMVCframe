using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ClientMediator:Mediator
{
    private ClientProxy clientProxy = null;
    public new const string Name = "ClientMediator";
    
    
   
    private ClientView View
    {
        get { return (ClientView)ViewComponent; }
    }
    public ClientMediator(ClientView view):base(Name,view)
    {
        view.CallWaiter += data => { SendNotification(OrderSystemEvent.CALL_WAITER, data); };
        view.Order += data => { SendNotification(OrderSystemEvent.ORDER, data); };
        view.Pay += () => { SendNotification(OrderSystemEvent.PAY); };
    }
    public override void OnRegister()
    {
        base.OnRegister();
        clientProxy = Facade.Instance.RetrieveProxy(ClientProxy.Name) as ClientProxy;
        if (null == clientProxy)
            throw new Exception("获取" + ClientProxy.Name + "代理失败");

        IList<Action<object>> actionList = new List<Action<object>>()
            {
                item =>  SendNotification(OrderCommandEvent.GUEST_BE_AWAY, item, "Add"),
                // item => {SendNotification(OrderSystemEvent.ADD_GUEST,item);},
                item =>  SendNotification(OrderSystemEvent.GET_PAY, item),
            };
        View.UpdateClient(clientProxy.Clients, actionList);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>()
        {
            OrderSystemEvent.CALL_WAITER,
            OrderSystemEvent.ORDER,
            OrderSystemEvent.PAY,
            OrderSystemEvent.REFRESH,
        };
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        Debug.Log(notification.Name);
        switch (notification.Name)
        {
            case OrderSystemEvent.CALL_WAITER:
                ClientItem client=notification.Body as ClientItem;
                Debug.Log(client.id + "号桌顾客呼叫服务员,索要菜单");
                
                break;
            case OrderSystemEvent.ORDER:
                Order order=notification.Body as Order;
                if (order==null)
                {
                    throw new Exception("order is null");
                }
                SendNotification(OrderCommandEvent.CHANGE_CLIENT_STATE, order,"WaitFood");
                
                break;
            case OrderSystemEvent.PAY:
                break;
            case OrderSystemEvent.REFRESH:
                ClientItem clientItem=notification.Body as ClientItem;
                if (clientProxy==null)
                {
                    throw new Exception("获取" + ClientProxy.Name + "代理失败");
                }
                View.UpdateState(clientItem);
                break;
            default:
                break;
        }
    }
}

