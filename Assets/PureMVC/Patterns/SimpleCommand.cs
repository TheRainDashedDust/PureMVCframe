using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SimpleCommand : Notifier, ICommand, INotifier
{
    public virtual void Execute(INotification notification)
    {
        
    }
}

