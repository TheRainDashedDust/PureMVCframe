using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_WaiterState
{
    Idle,
    Busy,
    Null,
}
public class WaiterItem 
{
    public int id { get; set; }
    public string name { get; set; }
    public E_WaiterState state { get; set; }
    public Order Order { get; set; }
    public WaiterItem(int id, string name, E_WaiterState state = E_WaiterState.Idle, Order order=null)
    {
        this.id = id;
        this.name = name;
        this.state = state;
        this.Order = order;
    }
    public override string ToString()
    {
        return id+"号服务员\n"+name+"\n"+ResultState();
    }
    private string ResultState()
    {
        if (state==E_WaiterState.Idle)
        {
            return "休息中";
        }
        return "忙碌中" + Order.client.id + "送菜中";  

    }
}
