using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItemView : MonoBehaviour
{
    private Text text=null;
    private Image image = null;
    public RoomItem roomItem = null;
    public Action<object> action;
    private void Awake()
    {
        text=transform.Find("Id").GetComponent<Text>();
        image = transform.GetComponent<Image>();
    }
    public void InitRoom(RoomItem roomItem)
    {
        this.roomItem = roomItem;
        UpdateState();
    }
    private void UpdateState()
    {
        if (roomItem == null) { return; }
        switch (roomItem.state)
        {
            case E_RoomState.None:
                image.color = Color.red;
                text.text = "房间正在收拾";

                break;
            case E_RoomState.Idle:
                image.color = Color.green;
                text.text=roomItem.ToString();
                break;
            case E_RoomState.Busy:
                image.color = Color.yellow;
                text.text = roomItem.ToString();
                StartCoroutine(LeaveRoom());
                break;
            default:
                break;
        }
    }

    private IEnumerator LeaveRoom(float time=3)
    {
        roomItem.state = E_RoomState.None;
        Debug.Log(roomItem.id+"号房间有人入住");
        yield return new WaitForSeconds(time);
        //roomItem.state = E_RoomState.None;
        text.text = "该房间无人，正在收拾";
        action?.Invoke(roomItem);
        
    }

    
}
