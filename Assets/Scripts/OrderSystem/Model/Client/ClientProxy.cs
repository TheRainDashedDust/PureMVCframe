using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientProxy:Proxy
{
    public new const string Name = "ClientProxy";
    public IList<ClientItem> Clients
    {
        get { return (IList<ClientItem>)base.Data; }
    }
    public ClientProxy():base(Name,new List<ClientItem>())
    {
        //this.OnRegister();
    }

    public override void OnRegister()
    {
        base.OnRegister();
        AddClient(new ClientItem(1, 2, E_ClientState.WaitMenu));
        AddClient(new ClientItem(2, 1, E_ClientState.WaitMenu));
        AddClient(new ClientItem(3, 4, E_ClientState.WaitMenu));
        AddClient(new ClientItem(4, 5, E_ClientState.WaitMenu));
        AddClient(new ClientItem(5, 12, E_ClientState.WaitMenu));
    }

    public void AddClient(ClientItem item)
    {
        if (Clients.Count<5)
        {
            Clients.Add(item);
        }
        UpdateClient(item);
        //判断是否已拥有?
        //Clients.Add(item);
        
       
    }
    public void DeleteClient(ClientItem item)
    {
        for (int i = 0; i < Clients.Count; i++)
        {
            if (Clients[i].id == item.id)
            {
                Clients[i].state = E_ClientState.Pay;
                SendNotification(OrderSystemEvent.REFRESH, Clients[i]);
                //入住
                SendNotification(OrderSystemEvent.CHECK_IN, Clients[i]);
                return;
            }
            
        }
        Debug.Log("刷新");
    }
    public void UpdateClient(ClientItem item)
    {
        for (int i = 0; i < Clients.Count; i++)
        {
            if (Clients[i].id==item.id)
            {
                Clients[i] = item;
                SendNotification(OrderSystemEvent.REFRESH, Clients[i]);
                return;
            }
        }
    }

    public void ChangeClientState(ClientItem client, E_ClientState state)
    {
        client.state = state;
        SendNotification(OrderSystemEvent.REFRESH, client);
    }
}
