using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        #region 创建代理
        //菜单代理
        MenuProxy menuProxy = new MenuProxy();
        FacadeInstance.RegisterProxy(menuProxy);
        //客户端代理
        ClientProxy clientProxy = new ClientProxy();
        FacadeInstance.RegisterProxy(clientProxy);
        //服务员代理
        WaiterProxy waiterProxy=new WaiterProxy();
        FacadeInstance.RegisterProxy(waiterProxy);
        //厨师代理
        CookProxy cookProxy=new CookProxy();
        FacadeInstance.RegisterProxy(cookProxy);
        //房间代理
        RoomProxy roomProxy=new RoomProxy();
        FacadeInstance.RegisterProxy(roomProxy);
        //命令代理
        OrderProxy orderProxy=new OrderProxy();
        FacadeInstance.RegisterProxy(orderProxy);
        
        #endregion


        #region 逻辑启动
        MainUI mainUI = notification.Body as MainUI;
        if (mainUI == null)
        {
            throw new System.Exception("程序启动失败..");
        }
        FacadeInstance.RegisterMediator(new MenuMediator(mainUI.MenuView));
        FacadeInstance.RegisterMediator(new ClientMediator(mainUI.ClientView));
        FacadeInstance.RegisterMediator(new WaiterMediator(mainUI.WaiterView));
        FacadeInstance.RegisterMediator(new CookMediator(mainUI.CookView));
        FacadeInstance.RegisterMediator(new RoomMediator(mainUI.RoomView));

        FacadeInstance.RegisterCommand(OrderCommandEvent.GUEST_BE_AWAY,typeof(GuestBeAwayCommand));
        FacadeInstance.RegisterCommand(OrderCommandEvent.GET_ORDER,typeof(GetAndExitOrderCommand));
        FacadeInstance.RegisterCommand(OrderCommandEvent.COOK_COOKING,typeof(CookCommand));
        FacadeInstance.RegisterCommand(OrderCommandEvent.SELECT_WAITER,typeof(WaiterCommand));
        FacadeInstance.RegisterCommand(OrderCommandEvent.CHANGE_CLIENT_STATE,typeof(ClientChangeStateCommand));
        FacadeInstance.RegisterCommand(OrderCommandEvent.CHANGE_ROOM_STATE,typeof(RoomCommand));
        #endregion

    }
}
