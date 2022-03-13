using Gummi.Utility.Command;

namespace Gummi.Tests.Command
{
    public class CMDExample : ICommand
    {
        public static int value = 0;

        public int iVal;

        public CMDExample(int iVal)
        {
            this.iVal = iVal;
        }

        public void Execute()
        {
            value++;
        }

        public void Undo()
        {
            value--;
        }
    }
}