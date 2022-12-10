using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GuestBeAwayCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        ClientProxy clientProxy=FacadeInstance.RetrieveProxy(ClientProxy.Name) as ClientProxy;
        if (notification.Type=="Add")
        {
            ClientItem client = notification.Body as ClientItem;
            
            client.state = E_ClientState.WaitMenu;
            client.population=UnityEngine.Random.Range(3, 10);
            clientProxy.AddClient(client);
        }
        else if (notification.Type=="Remove")
        {
            UnityEngine.Debug.Log("客人离开"+notification.Body.GetType());

            clientProxy.DeleteClient(notification.Body as ClientItem);

        }
    }
}

