using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class MacroCommand : Notifier, ICommand, INotifier
{
    private IList<Type> m_subCommands=new List<Type>();

    public MacroCommand()
    {
        this.InitializeMacroCommand();
    }
    protected void AddSubCommand(Type commandType)
    {
        this.m_subCommands.Add(commandType);
    }
    protected virtual void InitializeMacroCommand()
    {

    }
    public void Execute(INotification notification)
    {
        while (this.m_subCommands.Count>0)
        {
            Type type = this.m_subCommands[0];
            object obj=Activator.CreateInstance(type);
            if (obj is ICommand)
            {
                ((ICommand)obj).Execute(notification);
            }
            this.m_subCommands.RemoveAt(0);
        }
    }
}

