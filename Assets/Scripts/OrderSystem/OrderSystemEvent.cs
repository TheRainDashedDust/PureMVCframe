using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystemEvent
{
    /// <summary>
    /// ��ʼ
    /// </summary>
    public const string STARTUP="StartUp";
    /// <summary>
    /// ֪ͨ��ʦ
    /// </summary>
    public const string CALL_COOK="CallCook";
    /// <summary>
    /// �ϲ�
    /// </summary>
    public const string SERVER_FOOD="ServerFood";
    /// <summary>
    /// ����Ա�ϲ�
    /// </summary>
    public const string FOOD_TO_CLIENT="FoodToClient";
    /// <summary>
    /// �ύ�˵�
    /// </summary>
    public const string SUBMITMENU="SubmitMenu";
    /// <summary>
    /// ȡ�����
    /// </summary>
    public const string CANCEL_ORDER="CancelOrder";
    /// <summary>
    /// �ϲ˵�
    /// </summary>
    public const string UPMENU="UpMenu";
    /// <summary>
    /// ���
    /// </summary>
    public const string ORDER = "Order";
    /// <summary>
    /// ���з���Ա
    /// </summary>
    public const string CALL_WAITER = "CallWaiter";
    /// <summary>
    /// ����
    /// </summary>
    public const string PAY = "Pay";
    /// <summary>
    /// ����Ա�տ�
    /// </summary>
    public const string GET_PAY = "GetPay";
    
    /// <summary>
    /// ˢ�·���Ա״̬
    /// </summary>
    public const string REFRESH_WAITER="RefreshWaiter";
    /// <summary>
    /// ˢ�³�ʦ״̬
    /// </summary>
    public const string REFRESH_COOK="RefreshCook";
    /// <summary>
    /// �������ˣ�ˢ�¿���
    /// </summary>
    public const string REFRESH="Refresh";
    /// <summary>
    /// ����Ҫ��ס����,ˢ�·���
    /// </summary>
    public const string REFRESH_ROOM="RefreshRoom";
    /// <summary>
    /// ������ס
    /// </summary>
    public const string CHECK_IN = "CheckIn";
    /// <summary>
    /// �뿪����
    /// </summary>
    public const string LEAVE = "Leave";
}
public class OrderCommandEvent
{
    /// <summary>
    /// ѡ����еķ���Ա
    /// </summary>
    public const string SELECT_WAITER = "SelectWaiter";
    /// <summary>
    /// ��ȡ�˵�
    /// </summary>
    public const string GET_ORDER="GetOrder";
    /// <summary>
    /// �����뿪����
    /// </summary>
    public const string GUEST_BE_AWAY="GuestBeAway";
    /// <summary>
    /// �ı����״̬
    /// </summary>
    public const string CHANGE_CLIENT_STATE="ChangeClientState";
    /// <summary>
    /// ��ʦ����
    /// </summary>
    public const string COOK_COOKING = "CookCooking";
    /// <summary>
    /// �ı�ͷ�״̬
    /// </summary>
    public const string CHANGE_ROOM_STATE = "ChangeRoomState";
 
    
    
}