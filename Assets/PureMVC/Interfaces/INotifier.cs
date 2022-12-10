﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface INotifier
{
    void SendNotification(string notificationName);
    void SendNotification(string notificationName,object body);
    void SendNotification(string notificationName,object body,string type);
}

