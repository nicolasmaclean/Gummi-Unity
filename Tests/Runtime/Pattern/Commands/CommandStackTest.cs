using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Gummi.Pattern.Commands;

namespace Gummi.Tests.Pattern.Commands
{
    [TestFixture]
    public class CommandStackTest
    {
        /// <summary>
        /// An empty <see cref="CommandStack"/> for tests to use.
        /// </summary>
        CommandStack _commandStack = null;

        [SetUp]
        public void Setup()
        {
            _commandStack = CommandStack.Create();
            CMDExample.value = 0;
        }

        [TearDown]
        public void Cleanup()
        {
            if (_commandStack != null)
            {
                UnityEngine.Object.Destroy(_commandStack.gameObject);
                _commandStack = null;
            }
        }

        [Test]
        public void CreateCommandStackTest()
        {
            Assert.IsNotNull(_commandStack);
        }

        [Test]
        public void BasicExecuteTest()
        {
            Assert.Throws(typeof(System.NullReferenceException), () => _commandStack.Execute(null));

            foreach (int executions in new int[] { 0, 2, 20 })
            {
                CMDExample.value = 0;

                for (int i = 0; i < executions; i++)
                {
                    _commandStack.Execute(new CMDExample(i));
                    Assert.AreEqual(i + 1, CMDExample.value);
                }
            }
        }

        [Test]
        public void BasicUndoTest()
        {
            Assert.IsNull(_commandStack.Undo());

            int executions = 5;
            for (int i = 0; i < executions; i++)
            {
                _commandStack.Execute(new CMDExample(i));
            }

            for (int i = executions; i > 0; i--)
            {
                CMDExample cmd = (CMDExample) _commandStack.Undo();
                Assert.AreEqual(i - 1, CMDExample.value);
                Assert.AreEqual(i - 1, cmd.iVal);
            }
        }
    }
}