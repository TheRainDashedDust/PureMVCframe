using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class ClientItemView : MonoBehaviour
{
    private Text text = null;
    private Image image = null;
    public ClientItem clientItem = null;
    public IList<Action<object>> actions= new List<Action<object>>();
    private void Awake()
    {
        text = transform.Find("Id").GetComponent<Text>();
        image=transform.GetComponent<Image>();
    }
    public void InitClient(ClientItem clientitem)
    {
        this.clientItem = clientitem;
        UpdateState();
    }
    private void UpdateState()
    {
        if (clientItem==null)
        {
            return;
        }
        Color color = Color.white;
        switch (this.clientItem.state)
        {
            case E_ClientState.WaitMenu:
                color = Color.green;
                break;
            case E_ClientState.WaitFood:
                color = Color.yellow;
                break;
            case E_ClientState.Eatting:
                color = Color.red;
                StartCoroutine(Eatting());
                break;
            case E_ClientState.Pay:
                StartCoroutine(AddGuest());
                break;
            case E_ClientState.None:
                color = Color.blue;

                break;
            default:
                break;
        }
        image.color = color;
        text.text = clientItem.ToString();
    }

    private IEnumerator AddGuest(float time=4)
    {
        yield return new WaitForSeconds(time);
        actions[0].Invoke(clientItem);
        
    }

    private IEnumerator Eatting(float time=5)
    {
        Debug.Log(clientItem.id + "号桌客人正在就餐");
        yield return new WaitForSeconds(time);
        Debug.Log(clientItem.id + "号桌客人离开饭店");
        clientItem.state=E_ClientState.Leave;
        actions[1].Invoke(clientItem);
        text.text = "该桌位暂无客人";
        image.color = Color.white;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
