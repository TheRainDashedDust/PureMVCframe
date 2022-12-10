using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WaiterItemView : MonoBehaviour
{
    public WaiterItem waiter=null;
    public Text id;
    public Image image;
    private void Awake()
    {
        id= transform.Find("ID").GetComponent<Text>();
        image=GetComponent<Image>();
    }
    public void InitData(WaiterItem waiterItem)
    {
        waiter = waiterItem;
        switch (waiter.state)
        {
            case E_WaiterState.Idle:
                id.text = waiterItem.ToString();
                image.color = Color.green;
                break;
            case E_WaiterState.Busy:
                id.text = waiterItem.ToString();
                image.color = Color.yellow;
                break;
            default:
                image.color = Color.red;
                id.text = waiterItem.ToString();
                break;
        }
    }
    
}
