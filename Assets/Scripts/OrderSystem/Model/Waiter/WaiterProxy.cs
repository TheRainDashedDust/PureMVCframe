using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Progress;


public class WaiterProxy:Proxy
{
    public new const string Name = "WaiterProxy";
    public Queue<Order> WaitforServingOrder=new Queue<Order>();
    public IList<WaiterItem> Waiters
    {
        get { return (IList<WaiterItem>)base.Data; }
    }
    public WaiterProxy():base(Name,new List<WaiterItem>())
    {
        //this.OnRegister();
    }
    public override void OnRegister()
    {
        base.OnRegister();
        AddWaitor(new WaiterItem(1, "小丽"));
        AddWaitor(new WaiterItem(2, "小红"));
        AddWaitor(new WaiterItem(3, "小花"));
    }
    public void AddWaitor(WaiterItem item)
    {
        Waiters.Add(item);
    }
    public void RemoveWaiter(WaiterItem item)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (item.id == Waiters[i].id)
            {
                //Waiters[i].state = E_WaiterState.Idle;
                //SendNotification(OrderSystemEvent.REFRESH_WAITER);
                ChangeWaiterState(item);
                return;
            }
        }
    }
    public WaiterItem GetWaiter(int id)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i].id==id)
            {
                return Waiters[i];
            }
        }
        return null;
    }
    public void ChangeWaiterState(WaiterItem item)
    {
        GetWaiter(item.id).state= E_WaiterState.Idle;
        if (WaitforServingOrder.Count>0)
        {
            ChangeWaiterServing(WaitforServingOrder.Dequeue());
            return;
        }
        SendNotification(OrderSystemEvent.REFRESH_WAITER);
    }
    public void ChangeWaiterServing(Order order)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (Waiters[i].state==E_WaiterState.Idle)
            {
                Waiters[i].state = E_WaiterState.Busy;
                Waiters[i].Order= order;
                SendNotification(OrderSystemEvent.REFRESH_WAITER);
                SendNotification(OrderSystemEvent.FOOD_TO_CLIENT, Waiters[i]);
                return;
            }
        }
        UnityEngine.Debug.Log("暂无空闲服务员 请稍等片刻");
        //放入等待队列
        WaitforServingOrder.Enqueue(order);
        
    }

   
}

