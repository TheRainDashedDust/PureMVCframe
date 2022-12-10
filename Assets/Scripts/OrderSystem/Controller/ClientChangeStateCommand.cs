using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ClientChangeStateCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        Order order = notification.Body as Order;
        ClientProxy clientProxy = FacadeInstance.RetrieveProxy(ClientProxy.Name) as ClientProxy;
        switch (notification.Type)
        {
            case "WaitFood":
                clientProxy.ChangeClientState(order.client, E_ClientState.WaitFood);
                break;
            case "Eatting":
                clientProxy.ChangeClientState(order.client, E_ClientState.Eatting);
                break;
            default:
                break;
        }
    }
}

