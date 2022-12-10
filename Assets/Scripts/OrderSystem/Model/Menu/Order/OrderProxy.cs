using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderProxy :Proxy
{
    public new const string Name = "OrderProxy";
    public IList<Order> Orders
    {
        get { return (IList<Order>)base.Data; }
    }
    public OrderProxy():base(Name,new List<Order>())
    {
        //this.OnRegister();
    }
    public void AddOrder(Order order)
    {
        order.id = Orders.Count + 1;
        Orders.Add(order);
    }
    public void RemoveOrder(Order order)
    {
        Orders.Remove(order);
    }
}
