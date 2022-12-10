using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CookView : MonoBehaviour
{
    public UnityAction CallCook = null;
    public UnityAction<CookItem> ServerFood = null;
    private ObjectPool<CookItemView> objectPool = null;
    private List<CookItemView> cooks=new List<CookItemView>();
    private Transform parent = null;
    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("CookItem");
        objectPool = new ObjectPool<CookItemView>(prefab,"CookPool");
    }
    public void UpdateCook(IList<CookItem> cooks)
    {
        for (int i = 0; i < this.cooks.Count; i++)
        {
            objectPool.Push(this.cooks[i]);
        }
        this.cooks.AddRange(objectPool.Pop(cooks.Count));
        RefreshCook(cooks);


    }
  

    public void RefreshCook(IList<CookItem> cooks)
    {
        for (int i = 0; i < this.cooks.Count; i++)
        {
            this.cooks[i].transform.SetParent(parent);

            var item = cooks[i];
            this.cooks[i].InitCookView(cooks[i]);
            if (cooks[i].state==E_CookerState.Busy)
            {
                StartCoroutine(Cooking(cooks[i]));
            }
        }
    }

    private IEnumerator Cooking(CookItem cookItem,float time=4)
    {
        cookItem.state = E_CookerState.Null;

        yield return new WaitForSeconds(time);
        //cookItem.state = E_CookerState.Idle;
        //Debug.Log(cookItem.cookOrder);
        ServerFood.Invoke(cookItem);
    }
}
