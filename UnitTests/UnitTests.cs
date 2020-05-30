using System;
using System.IO;
using practical_task4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestFindX1()
        {
            double result = 0.6358;

            Assert.AreEqual(result, Math.Round(Program.FindX(0.1), 4));
        }

        [TestMethod]
        public void TestFindX2()
        {
            double result = 0;

            Assert.AreEqual(result, Program.FindX(2));
        }

        [TestMethod]
        public void TestDoubleInput()
        {
            Console.SetIn(new StreamReader("input.txt"));
            double result = 0.04;

            double input = Program.DoubleInput(lBound: 0, info: "some info");

            Assert.AreEqual(result, input);
        }
    }
}
