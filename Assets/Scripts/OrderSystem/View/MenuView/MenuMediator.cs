using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MenuMediator : Mediator
{
    private MenuProxy menuProxy = null;
    public new const string Name = "MenuMediator";
    public MenuView MenuView
    {
        get { return (MenuView)ViewComponent; }
    }
    public MenuMediator(MenuView view):base(Name,view)
    {
        MenuView.Submit += order => { SendNotification(OrderSystemEvent.SUBMITMENU, order); };
        MenuView.Cancel += () => { SendNotification(OrderSystemEvent.CANCEL_ORDER); };
    }
    public override void OnRegister()
    {
        base.OnRegister();
        menuProxy=Facade.Instance.RetrieveProxy(MenuProxy.Name) as MenuProxy;
        if (menuProxy==null)
        {
            throw new Exception(MenuProxy.Name + "is null!");

        }
        MenuView.UpdateMenu(menuProxy.Menus);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>
        {
            OrderSystemEvent.UPMENU,
            OrderSystemEvent.CANCEL_ORDER,
            OrderSystemEvent.SUBMITMENU
        };
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case OrderSystemEvent.UPMENU:
                Order order = notification.Body as Order;
                if (order==null)
                {
                    throw new Exception("order is null");
                }
                MenuView.UpMenu(order);
                break;
            case OrderSystemEvent.CANCEL_ORDER:
                Order order1= notification.Body as Order;
                if (order1==null)
                {
                    throw new Exception("order is null");
                }
                MenuView.CancelMenu();
                SendNotification(OrderCommandEvent.GET_ORDER, order1, "Exit");
                break;
            case OrderSystemEvent.SUBMITMENU:
                Order selectedOrder=notification.Body as Order;
                
                MenuView.SubmitMenu(selectedOrder);
                SendNotification(OrderSystemEvent.ORDER, selectedOrder);
                //SendNotification(OrderCommandEvent.CHANGECLIENTSTATE, selectedOrder,"WaitFood");
                break;
            default:
                break;
        }
    }
}
