using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CookerState
{
    Idle,
    Busy,
    Null,
}

public class CookItem
{
    public Order cookOrder { get; set; }

    public int id { get; set; }
    public string name { get; set; }
    public E_CookerState state { get; set; }
    public string cooking { get; set; }
    public CookItem(int id, string name, E_CookerState state = E_CookerState.Idle, string cooking="",Order cookOrder=null)
    {
        this.id = id;
        this.name = name;
        this.state = state;
        this.cooking = cooking;
        this.cookOrder = cookOrder;
    }
    public override string ToString()
    {
        return id+"号厨师\n"+name+"\n"+ResultState();
    }
    private string ResultState()
    {
        if (state== E_CookerState.Idle)
        {
            return "休息中";
        }
        return "忙碌中:"+cookOrder.client.id+"做菜中";
    }
}
