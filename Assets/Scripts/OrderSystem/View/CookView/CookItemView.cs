using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookItemView : MonoBehaviour
{
    public Text id;
    public Image image;
    public CookItem cookItem;

    private void Awake()
    {
        id = transform.Find("Id").GetComponent<Text>();
        image = GetComponent<Image>();
    }
    public void InitCookView(CookItem cookItem)
    {
        this.cookItem = cookItem;
        switch (cookItem.state)
        {
            case E_CookerState.Idle:
                image.color = Color.green;
                id.text = cookItem.ToString();
                break;
            case E_CookerState.Busy:
                image.color = Color.yellow;
                id.text = cookItem.ToString();
                break;
            default:
                image.color = Color.red;
                id.text = cookItem.ToString();
                break;
        }
    }
    
}
