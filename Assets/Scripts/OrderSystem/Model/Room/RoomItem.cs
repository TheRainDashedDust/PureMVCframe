using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum E_RoomState
{
    None = 0,
    Idle=1,
    Busy=2,
}
public class RoomItem 
{
    public int id { get; set; }
    public int population { get; set; }
    public ClientItem ClientItem { get; set; }
    public E_RoomState state { get; set; }
    public RoomItem(int id, int population, E_RoomState state)
    {
        this.id = id;
        this.population = population;
        this.state = state;
    }
    public override string ToString()
    {
        return id+"�ŷ���"+"\n"+population+"�˼�"+"\n"+ ReturnState(state);
    }
    private string ReturnState(E_RoomState roomState )
    {
        switch (roomState)
        {
            case E_RoomState.None:
                return "";
                
            case E_RoomState.Idle:
                return "����";
            case E_RoomState.Busy:
                return "��Ա";
            default:
                return "";
        }
        
    }
}
