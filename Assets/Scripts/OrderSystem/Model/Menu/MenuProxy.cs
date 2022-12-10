using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuProxy :Proxy
{
    public new const string Name = "MenuProxy";
    public IList<MenuItem> Menus
    {
        get { return (IList<MenuItem>)base.Data; }
    }

    public MenuProxy():base(Name,new List<MenuItem>())
    {
        //this.OnRegister();
    }
    public override void OnRegister()
    {
        base.OnRegister();
        AddMenu(new MenuItem(1, "小龙虾", 99, false));
        AddMenu(new MenuItem(2, "土豆牛肉", 45, true));
        AddMenu(new MenuItem(3, "麻婆豆腐", 22, true));
        AddMenu(new MenuItem(4, "小炒肉", 38, false));
        AddMenu(new MenuItem(5, "羊肉火锅", 108, true));
        AddMenu(new MenuItem(6, "驴肉火烧", 15, true));
        AddMenu(new MenuItem(7, "麻辣香锅", 32, true));
        AddMenu(new MenuItem(8, "地三鲜", 28, false));
        AddMenu(new MenuItem(9, "土锅炖大鹅", 99, true));
        AddMenu(new MenuItem(10, "地锅鸡", 99, false));

    }
    public void AddMenu(MenuItem item)
    {
        if (!Menus.Contains(item))
        {
            Menus.Add(item);
            
        }
    }
    public void Remove(MenuItem item)
    {
        if (Menus.Contains(item))
        {
            Menus.Remove(item);
        }
    }
    public void OutOfStock(MenuItem item)
    {
        foreach (var menuItem in Menus)
        {
            if (menuItem.id==item.id)
            {
                menuItem.instock = false;
                return;
            }
        }
    }

}
