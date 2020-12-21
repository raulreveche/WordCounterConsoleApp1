using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var sw = new StringWriter())
            {
                using (var inputs = new StringReader("c:/temp/files/input/Article.txt\nc:/temp/files/input/Words.txt\nc:/temp/files/output/wordstats.txt"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(inputs);
                    // Act
                    WordCounterConsoleApp1.Program.Main();
                    // Assert
                    var result = sw.ToString();
                    Assert.IsTrue(result.Contains("a. this {3:1,6,8}"));
                }
            }
        }
    }
}
