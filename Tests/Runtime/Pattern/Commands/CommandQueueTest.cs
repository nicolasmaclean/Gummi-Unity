using System.Collections.Generic;
using Gummi.Patterns.Commands;
using NUnit.Framework;
using UnityEngine;

namespace Gummi.Tests.Patterns.Commands
{
    [TestFixture]
    public class CommandQueueTest
    {
        /// <summary>
        /// An empty <see cref="CommandQueue"/> for tests to use.
        /// </summary>
        CommandQueue _commandQueue = null;

        [SetUp]
        public void Setup()
        {
            _commandQueue = CommandQueue.Create();
            CMDExample.value = 0;
        }

        [TearDown]
        public void Cleanup()
        {
            if (_commandQueue != null)
            {
                Object.Destroy(_commandQueue.gameObject);
                _commandQueue = null;
            }
        }

        [Test]
        public void CreateCommandQueueTest()
        {
            Assert.IsNotNull(_commandQueue);
        }

        [Test]
        public void BasicAddTest()
        {
            foreach (int adds in new int[] { 0, 2, 10 })
            {
                CMDExample.value = 0;
                for (int i = 0; i < adds; i++)
                {
                    int historyCount = _commandQueue.HistoryCount;
                    _commandQueue.Add(new CMDExample(i));
                    Assert.AreEqual(i + 1, _commandQueue.Count);
                    Assert.AreEqual(historyCount, _commandQueue.HistoryCount);
                }
                _commandQueue.Clear();
            }
        }

        [Test]
        public void BasicExecuteTest()
        {
            Assert.IsNull(_commandQueue.Execute());

            CMDExample cmd_1 = new CMDExample(28);
            _commandQueue.Add(cmd_1);
            Assert.AreSame(cmd_1, _commandQueue.Execute());

            CMDExample cmd_2 = new CMDExample(97);
            _commandQueue.Add(cmd_1);
            _commandQueue.Add(cmd_2);
            Assert.AreSame(cmd_1, _commandQueue.Execute());
            Assert.AreSame(cmd_2, _commandQueue.Execute());

            _commandQueue.Add(cmd_1);
            _commandQueue.Add(cmd_2);
            List<ICommand> cmds = _commandQueue.ExecuteAll();
            Assert.AreSame(cmd_1, cmds[0]);
            Assert.AreSame(cmd_2, cmds[1]);
        }

        [Test]
        public void UndoAndRequeueTest()
        {
            CMDExample cmd_1 = new CMDExample(28);
            CMDExample cmd_2 = new CMDExample(97);
            _commandQueue.Add(cmd_1);
            _commandQueue.Add(cmd_2);
            _commandQueue.ExecuteAll();

            Assert.AreEqual(cmd_2, _commandQueue.UndoAndRequeue());
            _commandQueue.Add(cmd_1);
            Assert.AreEqual(cmd_2, _commandQueue.Execute());
        }

        [Test]
        public void UndoAndPriorityRequeueTest()
        {
            CMDExample cmd_1 = new CMDExample(28);
            CMDExample cmd_2 = new CMDExample(97);
            _commandQueue.Add(cmd_1);
            _commandQueue.Add(cmd_2);
            _commandQueue.ExecuteAll();

            _commandQueue.Add(cmd_1);
            Assert.AreEqual(cmd_2, _commandQueue.UndoAndPriorityRequeue());
            Assert.AreEqual(cmd_2, _commandQueue.Execute());
        }
    }
}