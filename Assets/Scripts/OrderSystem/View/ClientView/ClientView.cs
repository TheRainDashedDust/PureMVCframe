using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientView:MonoBehaviour
{
    public UnityAction<ClientItem> CallWaiter = null;
    public UnityAction<Order> Order = null;
    public UnityAction Pay = null;

    private ObjectPool<ClientItemView> objectPool = null;
    private List<ClientItemView> clients=new List<ClientItemView>();
    private Transform parent = null;
    private void Awake()
    {
        //
        parent=this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("ClientItem");
        objectPool=new ObjectPool<ClientItemView>(prefab,"ClientPool");
    }
    
    public void UpdateClient(IList<ClientItem> clientList, IList<Action<object>> objs)
    {

        for (int i = 0; i < this.clients.Count; i++)
            objectPool.Push(this.clients[i]);

        this.clients.AddRange(objectPool.Pop(clientList.Count));

        for (int i = 0; i < this.clients.Count; i++)
        {
            var clientIV = this.clients[i];
            clientIV.transform.SetParent(parent);
            clientIV.InitClient(clientList[i]);
            clientIV.actions = objs;
            clientIV.GetComponent<Button>().onClick.RemoveAllListeners();
            clientIV.GetComponent<Button>().onClick.AddListener(() => {
                if (clientIV.clientItem==null)
                {
                    throw new Exception("ClientItem is null");
                    
                }
                if (clientIV.clientItem.state == E_ClientState.WaitMenu) CallWaiter(clientIV.clientItem); });
        }
    }
    public void UpdateState(ClientItem client)
    {
        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i].clientItem==null)
            {
                continue;
            }
            if (clients[i].clientItem.id.Equals(client.id))
            {
                clients[i].InitClient(client);
                return;
            }
        }
    }
    public void RefreshClient(IList<ClientItem> reclients)
    {
        for (int i = 0; i < reclients.Count; i++)
        {
            UpdateState(reclients[i]);
        }
    }
}

