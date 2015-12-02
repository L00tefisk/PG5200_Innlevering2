using System.Collections.Generic;
using System.Drawing.Imaging;

namespace LevelEditor.Model.Commands
{
    public class CommandController
    {
        private readonly List<Command> commands;
        private short currentCommand;
        public CommandController()
        {
            commands = new List<Command>();
            currentCommand = 0;
        }
        /// <summary>
        /// Adds a command to the list of commands and executes it
        /// </summary>
        /// <param name="command">Command to execute</param>
        public void Add(Command command)
        {
            if (currentCommand < commands.Count)
                commands.RemoveRange(currentCommand, commands.Count - currentCommand);
            commands.Insert(currentCommand, command);
            
            Execute();
        }
        public void Remove(Command command)
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
            if (commands.Count > 0 && currentCommand != 0)
            {
                if(currentCommand > 0)
                    currentCommand--;
                commands[currentCommand].Undo();
            }
        }
    }
}
