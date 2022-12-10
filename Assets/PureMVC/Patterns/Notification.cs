using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Notification :INotification
{
    private object m_body;
    private string m_name;
    private string m_type;

    public Notification(string name): this(name,null,null)
    {
        
    }

    public Notification(string name, object body) : this(name, body,null)
    {
        
    }

    public Notification(string name, object body, string type)
    {
        this.m_name = name;
        this.m_body = body;
        this.m_type = type;
        
    }

    public object Body { get => this.m_body; set =>this.m_body= value; }

    public string Name{get => this.m_name;}

    public string Type{get => this.m_type;set => this.m_type = value;}
    public override string ToString()
    {
        return base.ToString();
    }
}

