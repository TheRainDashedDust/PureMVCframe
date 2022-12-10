using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WaiterView : MonoBehaviour
{
    
    public UnityAction CallWaiter = null;
    public UnityAction<Order> Order = null;
    public UnityAction Pay = null;
    public UnityAction CallCook= null;
    public UnityAction<WaiterItem> ServerFood= null;
    //public UnityAction<WaiterItem> BusyAction= null;

    private ObjectPool<WaiterItemView> objectPool= null;
    private List<WaiterItemView> waiters= new List<WaiterItemView>();
    private Transform parent=null;

    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("WaiterItem");
        objectPool = new ObjectPool<WaiterItemView>(prefab, "WaiterPool");
    }
    public void UpdateWaiter(IList<WaiterItem> waiters)
    {
        for (int i = 0; i < this.waiters.Count; i++)
        {
            objectPool.Push(this.waiters[i]);
        }
        this.waiters.AddRange(objectPool.Pop(waiters.Count));
        Move(waiters);
    }
    public void Move(IList<WaiterItem> waiters)
    {
        for (int i = 0; i < this.waiters.Count; i++)
        {
            this.waiters[i].transform.SetParent(parent);
            this.waiters[i].InitData(waiters[i]);
            if (waiters[i].state==E_WaiterState.Busy)
            {
                StartCoroutine(WaiterServing(waiters[i]));
            }
            
            
        }
    }
    public IEnumerator WaiterServing(WaiterItem item,float timer=4)
    {
        item.state = E_WaiterState.Null;
        yield return new WaitForSeconds(timer);
        //item.state = E_WaiterState.Idle;
        
        ServerFood?.Invoke(item);
        //BusyAction?.Invoke(item);
    }
}
