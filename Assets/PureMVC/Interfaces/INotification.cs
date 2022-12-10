using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface INotification
{
    string ToString();
    object Body{get; set; }
    string Name{get; }
    string Type { get; set;}
}

