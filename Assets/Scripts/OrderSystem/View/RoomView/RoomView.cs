using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RoomView : MonoBehaviour
{
    public UnityAction<RoomItem> checkin;
    private ObjectPool<RoomItemView> objectPool = null;
    private List<RoomItemView> rooms = new List<RoomItemView>();
    private Transform parent;
    private void Awake()
    {
        parent = transform.Find("Content");
        var prefab = Resources.Load<GameObject>("RoomItem");
        objectPool = new ObjectPool<RoomItemView>(prefab, "RoomPool");
    }
    public void UpdateRoom(IList<RoomItem> roomlist,Action<object> action)
    {
        for (int i = 0; i < this.rooms.Count; i++)
        {
            objectPool.Push(this.rooms[i]);
        }
        this.rooms.AddRange(objectPool.Pop(roomlist.Count));
        
        for (int i = 0; i < this.rooms.Count; i++)
        {
            var roomIV = this.rooms[i];
            roomIV.transform.SetParent(parent);
            roomIV.InitRoom(roomlist[i]);

            roomIV.action = action;
        }
    }
    public void UpdateState(RoomItem room)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].roomItem==null)
            {
                continue;
            }
            if (rooms[i].roomItem.id.Equals(room.id))
            {
                rooms[i].InitRoom(room);
                return;
            }
            
        }
    }
    public void RefreshRoom(IList<RoomItem> roomItems)
    {
        for (int i = 0; i < roomItems.Count; i++)
        {
            UpdateState(roomItems[i]);
        }
    }

}
