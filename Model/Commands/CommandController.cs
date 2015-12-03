using System.Collections.Generic;
using System.Drawing.Imaging;

namespace LevelEditor.Model.Commands
{
    public class CommandController
    {
        private readonly List<Command> _commands;
        private short _currentCommand;
        public CommandController()
        {
            _commands = new List<Command>();
            _currentCommand = 0;
        }
        /// <summary>
        /// Adds a command to the list of _commands and executes it
        /// </summary>
        /// <param name="command">Command to execute</param>
        public void Add(Command command)
        {
            if (_currentCommand < _commands.Count)
                _commands.RemoveRange(_currentCommand, _commands.Count - _currentCommand);
            _commands.Insert(_currentCommand, command);
            
            Execute();
        }
        public void Remove(Command command)
        {
            _commands.Remove(command);
        }
        public void Execute()
        {
            if (_currentCommand < _commands.Count)
            {
                _commands[_currentCommand].Execute();
                _currentCommand++;
            }

        }
        public void Undo()
        {
            if (_commands.Count > 0 && _currentCommand != 0)
            {
                if(_currentCommand > 0)
                    _currentCommand--;
                _commands[_currentCommand].Undo();
            }
        }
    }
}
