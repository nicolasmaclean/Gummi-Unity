using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Gummi.Logger;
using UnityEngine;

namespace Gummi.Tests.Logger
{
    public class StringUtilsTest
    {
        [TestCase(1, 0, TestName = "0 is a single digit")]
        [TestCase(1, 1, TestName = "1 is a single digit")]
        [TestCase(1, 9, TestName = "9 is a single digit")]
        [TestCase(2, 10, TestName = "10 is double digits")]
        [TestCase(3, 101, TestName = "101 is triple digits")]
        [TestCase(1, -1, TestName = "-1 is single digits")]
        [TestCase(2, -11, TestName = "-11 is double digits")]
        public void CountDigitsTest(int expected, int value)
        {
            Assert.AreEqual(expected, StringUtils.CountDigits(value));
        }

        [TestCaseSource(nameof(PrettyPrintLists))]
        public void PrettyPrintTest(PrettyPrintTestCase testCase)
        {
            Assert.AreEqual(testCase.expected, StringUtils.PrettyPrint(testCase.test));
        }

        static IEnumerable<PrettyPrintTestCase> PrettyPrintLists()
        {
            // case: null
            yield return new PrettyPrintTestCase("null", "null", null);


            // case: []
            string correctOutput = "";
            yield return new PrettyPrintTestCase("empty", correctOutput, new List<int>());

            // case: [1]
            List<int> nums = new List<int>();
            nums.Add(1);
            correctOutput = $"00 : {1}\n";
            yield return new PrettyPrintTestCase("[1]", correctOutput, nums);

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
            yield return new PrettyPrintTestCase("[1, -2, 4, -8, 16, -32, 64, -128, 256, 512, 1024]",correctOutput, nums);

            // case: ["hello"]
            List<string> strings = new List<string>();

            strings.Add("hello");
            correctOutput = $"00 : {"hello"}\n";
            yield return new PrettyPrintTestCase("[\"hello\"]", correctOutput, strings);

            // case: [["hello]]
            List<List<string>> lists = new List<List<string>>();

            lists.Add(strings);
            correctOutput = $"00 : {lists[0]}\n";
            yield return new PrettyPrintTestCase("[[\"hello\"]]", correctOutput, lists);
        }

        public class PrettyPrintTestCase
        {
            public string title { get; private set; }
            public string expected { get; private set; }
            public IList test { get; private set; }

            public PrettyPrintTestCase(string title, string expected, IList test)
            {
                this.title = title;
                this.expected = expected;
                this.test = test;
            }

            public override string ToString()
            {
                return title;
            }
        }
    }
}
