using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Gummi.Logger;

namespace Gummi.Tests.Logger
{
    public class StringUtilsTest
    {
        [Test]
        public void CountDigitsTest()
        {
            Assert.AreEqual(1, StringUtils.CountDigits(0));
            Assert.AreEqual(1, StringUtils.CountDigits(1));
            Assert.AreEqual(1, StringUtils.CountDigits(9));
            Assert.AreEqual(2, StringUtils.CountDigits(10));
            Assert.AreEqual(3, StringUtils.CountDigits(101));
            Assert.AreEqual(1, StringUtils.CountDigits(-1));
            Assert.AreEqual(2, StringUtils.CountDigits(-11));
        }

        [Test]
        public void PrettyPrintTest()
        {
            // case: null
            Assert.AreEqual("null", StringUtils.PrettyPrint(null));

            List<int> nums = new List<int>();

            // case: []
            string correctOutput = "";
            Assert.AreEqual(correctOutput, StringUtils.PrettyPrint(nums));

            // case: [1]
            nums.Add(1);
            correctOutput = $"00 : {1}\n";
            Assert.AreEqual(correctOutput, StringUtils.PrettyPrint(nums));

            // case: [1, -2, 4, -8, 16, -32, 64, -128, 256, 512, 1024]
            nums = new List<int>() { 1, -2, 4, -8, 16, -32, 64, -128, 256, 512, 1024 };
            correctOutput = "000 : 1\n" +
                "001 : -2\n" +
                "002 : 4\n" +
                "003 : -8\n" +
                "004 : 16\n" +
                "005 : -32\n" +
                "006 : 64\n" +
                "007 : -128\n" +
                "008 : 256\n" +
                "009 : 512\n" +
                "010 : 1024\n";
            Assert.AreEqual(correctOutput, StringUtils.PrettyPrint(nums));

            // case: ["hello"]
            List<string> strings = new List<string>();

            strings.Add("hello");
            correctOutput = $"00 : {"hello"}\n";
            Assert.AreEqual(correctOutput, StringUtils.PrettyPrint(strings));

            // case: [["hello]]
            List<List<string>> lists = new List<List<string>>();

            lists.Add(strings);
            correctOutput = $"00 : {lists[0]}\n";
            Assert.AreEqual(correctOutput, StringUtils.PrettyPrint(lists));
        }
    }
}
