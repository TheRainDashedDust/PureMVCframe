using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CookProxy :Proxy
{
    public new const string Name = "CookProxy";
    public Queue<Order> WaidforCookOrder = new Queue<Order>();
    public IList<CookItem> Cooks
    {
        get { return (IList<CookItem>)base.Data; }
    }
    public CookProxy():base(Name,new List<CookItem>())
    {
        
    }
    public override void OnRegister()
    {
        base.OnRegister();
        AddCook(new CookItem(1, "强尼"));
        AddCook(new CookItem(2, "托尼"));
        AddCook(new CookItem(3, "鲍比"));
        //AddCook(new CookItem(4, "缇米"));
    }
    public void AddCook(CookItem item)
    {
        Cooks.Add(item);
    }
    public void RemoveCook(CookItem item)
    {
        Cooks.Remove(item);
    }

    public void CookCooking(Order order)
    {
        for (int i = 0; i < Cooks.Count; i++)
        {
            if (Cooks[i].state==E_CookerState.Idle)
            {
                Cooks[i].state=E_CookerState.Busy;
                Cooks[i].cooking = order.names;
                Cooks[i].cookOrder = order;
                UnityEngine.Debug.Log(order.names);
                SendNotification(OrderSystemEvent.REFRESH_COOK);
                return;
            }
        }
        WaidforCookOrder.Enqueue(order);
    }
    public CookItem GetCooker(int id)
    {
        for (int i = 0; i < Cooks.Count; i++)
        {
            if (Cooks[i].id==id)
            {
                return Cooks[i];
            }
        }
        return null;
    }
    public void ChangeCookerState(CookItem item)
    {
        GetCooker(item.id).state=E_CookerState.Idle;
        if (WaidforCookOrder.Count>0)
        {
            CookCooking(WaidforCookOrder.Dequeue());
            return;
        }
        
        SendNotification(OrderSystemEvent.REFRESH_COOK);
    }
}
