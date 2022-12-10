using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RoomCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        RoomProxy roomProxy=FacadeInstance.RetrieveProxy(RoomProxy.Name) as RoomProxy;
        if (notification.Type=="CheckIn")
        {
            roomProxy.CheckIn(notification.Body as ClientItem);
        }
        else if (notification.Type == "Leave")
        {
            roomProxy.ChangeRoomState(notification.Body as RoomItem);
        }
    }
}

