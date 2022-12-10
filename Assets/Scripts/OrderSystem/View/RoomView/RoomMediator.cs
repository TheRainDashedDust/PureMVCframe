using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMediator : Mediator
{
    private RoomProxy roomProxy=null;
    public new const string Name = "RoomMediator";
    private RoomView View
    {
        get { return (RoomView)ViewComponent; }
    }
    public RoomMediator(RoomView roomView):base(Name,roomView)
    {
        roomView.checkin += (item) => { SendNotification(OrderSystemEvent.REFRESH_ROOM, item); };
        
    }
    public override void OnRegister()
    {
        base.OnRegister();
        roomProxy=FacadeInstance.RetrieveProxy(RoomProxy.Name) as RoomProxy;
        if (roomProxy==null)
        {
            throw new System.Exception("获取" + RoomProxy.Name + "代理失败");

        }
        View.UpdateRoom(roomProxy.Rooms, (item)=>
        {
            SendNotification(OrderSystemEvent.LEAVE,item);
        });
        
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>()
        {
            OrderSystemEvent.REFRESH_ROOM,
            OrderSystemEvent.CHECK_IN,
            OrderSystemEvent.LEAVE,
        };
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        Debug.Log(notification.Name);
        switch (notification.Name)
        {
            case OrderSystemEvent.REFRESH_ROOM:
                //有人入住
                RoomItem roomItem=notification.Body as RoomItem;
                if (roomProxy == null) { throw new System.Exception("获取"+RoomProxy.Name+"代理失败"); }
                View.UpdateState(roomItem);
                break;
            case OrderSystemEvent.CHECK_IN:
                ClientItem clientItem=notification.Body as ClientItem;
                if (roomProxy == null) { throw new System.Exception("获取" + RoomProxy.Name + "代理失败"); }
                SendNotification(OrderCommandEvent.CHANGE_ROOM_STATE, clientItem,"CheckIn");
                break;
            case OrderSystemEvent.LEAVE:
                RoomItem item=notification.Body as RoomItem;
                if (roomProxy == null) { throw new System.Exception("获取" + RoomProxy.Name + "代理失败"); }
                SendNotification(OrderCommandEvent.CHANGE_ROOM_STATE, item, "Leave");
                break;
            default:
                break;
        }
    }
}
