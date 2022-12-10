using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public UnityAction<Order> Submit = null;
    public UnityAction Cancel= null;

    private ObjectPool<MenuItemView> objectPool = null;
    private List<MenuItemView> menus=new List<MenuItemView>();
    private Transform parent=null;
    private Order indexOrder = null;
    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("MenuItem");
        objectPool = new ObjectPool<MenuItemView>(prefab, "MenuPool");
        transform.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() => { Submit?.Invoke(indexOrder); });
        transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(CancelMenu);
    }
    public void UpdateMenu(IList<MenuItem> list)
    {
        for (int i = 0; i < this.menus.Count; i++)
        {
            objectPool.Push(this.menus[i]);
        }
        this.menus.AddRange(objectPool.Pop(list.Count));
        for (int i = 0; i < this.menus.Count; i++)
        {
            this.menus[i].transform.SetParent(parent);
            var item = this.menus[i];
            item.InitData(list[i]);
        }
    }

    public void UpMenu(Order order)
    {
        ResetMenu();
        indexOrder= order;
        this.transform.localPosition= Vector3.zero;
    }
    public void SubmitMenu(Order order)
    {
        order.menus = GetSelected();
        CancelMenu();
    }
    private void ResetMenu()
    {
        foreach (var item in menus)
        {
            item.toggle.isOn = false;
        }
    }
    public void CancelMenu()
    {
        this.transform.localPosition=new Vector3(0f,-800f,0f);
    }
    private IList<MenuItem> GetSelected()
    {
        IList<MenuItem> items = new List<MenuItem>();
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].Menu.iselected)
            {
                items.Add(menus[i].Menu);
            }
        }
        return items;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
