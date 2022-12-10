using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemView : MonoBehaviour
{
    public MenuItem Menu = null;
    public Toggle toggle = null;
    private void Awake()
    {
        toggle=transform.Find("Toggle").GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(isOn => { Menu.iselected=isOn;});
    }
    public void InitData(MenuItem item)
    {
        Menu = item;
        transform.Find("Price").GetComponent<Text>().text = item.price+"Ôª";
        string menuName=item.name;
        if (!item.instock)
        {
            menuName += "(ÎÞ»õ)";
        }
        toggle.transform.Find("Label").GetComponent<Text>().text = menuName;
        toggle.interactable=item.instock;
        toggle.isOn = item.iselected;
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
