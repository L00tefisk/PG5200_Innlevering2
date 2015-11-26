using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    public class CommandController
    {
        private List<ICommandPattern> commands;
        private short currentCommand;
        public CommandController()
        {
            commands = new List<ICommandPattern>();
            currentCommand = 0;
        }
        /// <summary>
        /// Adds a command to the list of commands and executes it
        /// </summary>
        /// <param name="command">Command to execute</param>
        public void Add(ICommandPattern command)
        {
            commands.Add(command);
            Execute();
        }
        public void Remove(ICommandPattern command)
        {
            commands.Remove(command);
        }
        public void Execute()
        {
            if (currentCommand < commands.Count)
            {
                commands[currentCommand].Execute();
                currentCommand++;
            }
                
        }
        public void Undo()
        {
            if (currentCommand <= 0)
            {
                commands[currentCommand].Undo();
                currentCommand--;
            }
        }
    }
}
