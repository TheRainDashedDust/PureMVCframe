using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystemEvent
{
    /// <summary>
    /// 开始
    /// </summary>
    public const string STARTUP="StartUp";
    /// <summary>
    /// 通知厨师
    /// </summary>
    public const string CALL_COOK="CallCook";
    /// <summary>
    /// 上菜
    /// </summary>
    public const string SERVER_FOOD="ServerFood";
    /// <summary>
    /// 服务员上菜
    /// </summary>
    public const string FOOD_TO_CLIENT="FoodToClient";
    /// <summary>
    /// 提交菜单
    /// </summary>
    public const string SUBMITMENU="SubmitMenu";
    /// <summary>
    /// 取消点餐
    /// </summary>
    public const string CANCEL_ORDER="CancelOrder";
    /// <summary>
    /// 上菜单
    /// </summary>
    public const string UPMENU="UpMenu";
    /// <summary>
    /// 点菜
    /// </summary>
    public const string ORDER = "Order";
    /// <summary>
    /// 呼叫服务员
    /// </summary>
    public const string CALL_WAITER = "CallWaiter";
    /// <summary>
    /// 结账
    /// </summary>
    public const string PAY = "Pay";
    /// <summary>
    /// 服务员收款
    /// </summary>
    public const string GET_PAY = "GetPay";
    
    /// <summary>
    /// 刷新服务员状态
    /// </summary>
    public const string REFRESH_WAITER="RefreshWaiter";
    /// <summary>
    /// 刷新厨师状态
    /// </summary>
    public const string REFRESH_COOK="RefreshCook";
    /// <summary>
    /// 来客人了，刷新客桌
    /// </summary>
    public const string REFRESH="Refresh";
    /// <summary>
    /// 有人要入住房间,刷新房间
    /// </summary>
    public const string REFRESH_ROOM="RefreshRoom";
    /// <summary>
    /// 有人入住
    /// </summary>
    public const string CHECK_IN = "CheckIn";
    /// <summary>
    /// 离开房间
    /// </summary>
    public const string LEAVE = "Leave";
}
public class OrderCommandEvent
{
    /// <summary>
    /// 选择空闲的服务员
    /// </summary>
    public const string SELECT_WAITER = "SelectWaiter";
    /// <summary>
    /// 获取菜单
    /// </summary>
    public const string GET_ORDER="GetOrder";
    /// <summary>
    /// 客人离开饭店
    /// </summary>
    public const string GUEST_BE_AWAY="GuestBeAway";
    /// <summary>
    /// 改变客桌状态
    /// </summary>
    public const string CHANGE_CLIENT_STATE="ChangeClientState";
    /// <summary>
    /// 厨师做菜
    /// </summary>
    public const string COOK_COOKING = "CookCooking";
    /// <summary>
    /// 改变客房状态
    /// </summary>
    public const string CHANGE_ROOM_STATE = "ChangeRoomState";
 
    
    
}