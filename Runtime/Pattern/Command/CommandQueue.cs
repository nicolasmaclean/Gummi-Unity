using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Core.Logger;

namespace Gummi.Pattern.Command
{
    public class CommandQueue : MonoBehaviour
    {
        public int Count => _queue.Count;
        public int HistoryCount => _history.Count;

        [SerializeField]
        internal int maxHistory = 100;

        List<ICommand> _queue = new List<ICommand>();
        CommandStack _history;

        /// <summary>
        /// Creates a basic, empty <see cref="CommandQueue"/>
        /// </summary>
        /// <returns></returns>
        public static CommandQueue Create()
        {
            return new GameObject("CMD_Queue").AddComponent<CommandQueue>();
        }

        void Awake()
        {
            // create command stack to track history
            _history = CommandStack.Create();
            _history.maxHistory = maxHistory;
            _history.transform.parent = transform;
        }

        /// <summary>
        /// Enqueues <paramref name="command"/> to be performed later 
        /// by <see cref="Execute"/>.
        /// </summary>
        /// <param name="command"> The <see cref="ICommand"/> to enqueue </param>
        public void Add(ICommand command)
        {
            if (command == null)
            {
                throw new System.NullReferenceException();
            }

            _queue.Add(command);
        }

        /// <summary>
        /// Executes the first <see cref="ICommand"/> in the queue and 
        /// returns a reference to it.
        /// </summary>
        /// <returns> The <see cref="ICommand"/> performed </returns>
        public ICommand Execute()
        {
            if (_queue.Count == 0)
            {
                return null;
            }

            ICommand command = _queue[0];
            _queue.RemoveAt(0);
            _history.Execute(command);

            return command;
        }

        /// <summary>
        /// Executes all queued <see cref="ICommand"/>'s and returns them as a list.
        /// </summary>
        /// <returns></returns>
        public List<ICommand> ExecuteAll()
        {
            List<ICommand> executed = new List<ICommand>();

            while (Count > 0)
            {
                executed.Add(Execute());
            }

            return executed;
        }

        /// <summary>
        /// Undoes the last <see cref="ICommand"/> performed with 
        /// <see cref="Execute"/> and returns it.
        /// </summary>
        /// <returns> The command undone </returns>
        public ICommand Undo() => _history.Undo();

        /// <summary>
        /// Requeues the <see cref="ICommand"/> given by <see cref="Undo"/>.
        /// </summary>
        /// <returns> The <see cref="ICommand"/> requeued </returns>
        public ICommand UndoAndRequeue()
        {
            ICommand command = Undo();

            if (command == null)
            {
                return null;
            }

            Add(command);
            return command;
        }

        /// <summary>
        /// Requeues the <see cref="ICommand"/> given by <see cref="Undo"/>, 
        /// but inserts it at the front of the queue instead of the back.
        /// </summary>
        /// <returns> The <see cref="ICommand"/> requeued </returns>
        public ICommand UndoAndPriorityRequeue()
        {
            ICommand command = Undo();

            if (command == null)
            {
                return null;
            }

            _queue.Insert(0, command);
            return command;
        }

        /// <summary>
        /// Clears all <see cref="ICommand"/>'s from the queue and returns it as a list.
        /// </summary>
        /// <returns></returns>
        public List<ICommand> Clear()
        {
            List<ICommand> queue = _queue;
            _queue = new List<ICommand>();
            return queue;
        }

        /// <summary>
        /// Clears history and returns it as a list.
        /// </summary>
        /// <returns></returns>
        public List<ICommand> ClearHistory() => _history.Clear();

        /// <summary>
        /// Returns a human-readable toString of the command history.
        /// </summary>
        /// <returns></returns>
        public string HistoryToString() => _history.HistoryToString();

        /// <summary>
        /// Returns a human-readable toString of the queue.
        /// </summary>
        /// <returns></returns>
        public string QueueToString() => StringUtils.PrettyPrint(_queue);
    }
}