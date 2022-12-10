using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProxy : Proxy
{
    public new const string Name = "RoomProxy";
    public Queue<ClientItem> WaitforRoom= new Queue<ClientItem>();
    public IList<RoomItem> Rooms
    {
        get { return (IList<RoomItem>)base.Data; }
    }
    public RoomProxy():base(Name,new List<RoomItem>())
    {

    }
    public override void OnRegister()
    {
        base.OnRegister();
        AddRoom(new RoomItem(1, 2, E_RoomState.Idle));
        //AddRoom(new RoomItem(2, 6, E_RoomState.Idle));
        //AddRoom(new RoomItem(3, 8, E_RoomState.Idle));
        //AddRoom(new RoomItem(4, 4, E_RoomState.Idle));
        //AddRoom(new RoomItem(5, 1, E_RoomState.Idle));
        //AddRoom(new RoomItem(6, 12, E_RoomState.Idle));
    }
    public void AddRoom(RoomItem item) 
    {
        Rooms.Add(item);
        //UpdateRoom(item);
    }
   /* public void UpdateRoom(RoomItem item)
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].id==item.id)
            {
                Rooms[i] = item;
                SendNotification(OrderSystemEvent.REFRESH_ROOM, Rooms[i]);
                return;
            }
        }
    }*/
    public void DelRoom(RoomItem item)
    {
        Rooms.Remove(item);
    }
    public void ChangeRoomState(RoomItem item)
    {
        GetRoom(item.id).state = E_RoomState.Idle;
        if (WaitforRoom.Count>0)
        {
            CheckIn(WaitforRoom.Dequeue());
            return;
        }
        SendNotification(OrderSystemEvent.REFRESH_ROOM,item);
    }
    public RoomItem GetRoom(int id)
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].id==id)
            {
                return Rooms[i];
            }
        }
        return null;
    }
    public void CheckIn(ClientItem clientItem)
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].state==E_RoomState.Idle)
            {
                Rooms[i].state = E_RoomState.Busy;
                Rooms[i].ClientItem = clientItem;
                Rooms[i].population = clientItem.population;
                SendNotification(OrderSystemEvent.REFRESH_ROOM, Rooms[i]);
                return;
            }
        }
        WaitforRoom.Enqueue(clientItem);
    }
}
