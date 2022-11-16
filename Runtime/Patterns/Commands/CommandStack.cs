using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Patterns
{
    public class CommandStack : MonoBehaviour
    {
        public int Count => _history.Count;

        [SerializeField]
        internal int maxHistory = 100;

        List<ICommand> _history = new List<ICommand>();

        /// <summary>
        /// Creates a basic, empty <see cref="CommandStack"/>
        /// </summary>
        /// <returns></returns>
        public static CommandStack Create()
        {
            return new GameObject("CMD_Stack").AddComponent<CommandStack>();
        }

        /// <summary>
        /// Performs <see cref="ICommand.Execute"/> and stores it to history.
        /// </summary>
        /// <param name="command"> The <see cref="ICommand"/> to perform. </param>
        public void Execute(ICommand command)
        {
            if (command == null)
            {
                throw new NullReferenceException();
            }

            command.Execute();
            _history.Add(command);

            if (_history.Count > maxHistory)
            {
                _history.RemoveAt(0);
            }
        }

        /// <summary>
        /// Performs <see cref="ICommand.Undo"/> from the last <see cref="ICommand"/> performed,
        /// removes it from history, and returns it.
        /// </summary>
        /// <returns> The last <see cref="ICommand"/> executed. </returns>
        public ICommand Undo()
        {
            if (_history.Count == 0)
            {
                return null;
            }

            ICommand command = _history[_history.Count - 1];
            _history.RemoveAt(_history.Count - 1);
            command.Undo();

            return command;
        }

        /// <summary>
        /// Clears all <see cref="ICommand"/>'s from the stack and returns them as a list.
        /// </summary>
        /// <returns></returns>
        public List<ICommand> Clear()
        {
            List<ICommand> history = _history;
            _history = new List<ICommand>();
            return history;
        }

        /// <summary>
        /// Returns a human-readable toString of <see cref="Instance"/>'s command history.
        /// </summary>
        /// <returns></returns>
        public string HistoryToString() => _history.PrettyPrint();
    }
}