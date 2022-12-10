using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_ClientState
{
    None = 0,
    WaitMenu=1,
    WaitFood=2,
    Eatting = 3,
    Pay = 4,
    Leave = 5,
}
public class ClientItem
{
    public int id { get; set; }
    public int population { get; set; }
    public E_ClientState state { get; set; }
    public ClientItem(int id, int population, E_ClientState state)
    {
        this.id = id;
        this.population = population;
        this.state = state;
    }
    public override string ToString()
    {
        return id+"号桌"+"\n"+population+"个人"+"\n"+ ReturnState(state);
    }
    private string ReturnState(E_ClientState state)
    {
        switch (state)
        {
            case E_ClientState.WaitMenu: return "等待菜单";
            case E_ClientState.WaitFood: return "等待上菜";
            case E_ClientState.Eatting: return "就餐中";
            case E_ClientState.Pay:return "支付";
            case E_ClientState.None:return "此桌暂无客人";
            case E_ClientState.Leave:return "此桌客人已离开";
            default:
                break;
        }
        return "已经结账";
    }
}
