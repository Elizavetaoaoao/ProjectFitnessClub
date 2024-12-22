using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFitnessClub;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestLoginAndPasswordAuthentication
    {
        [TestMethod]
        public void TestLoginAndPasswordAuthentication_True()
        {
            var form1 = new Form1();
            var correctLoginAndPasswordTest = form1.UserLogin("iviviv1990", "moyparolivanovich199");
            var correctLoginAndPasswordTrue = true;
            Assert.AreEqual(correctLoginAndPasswordTest, correctLoginAndPasswordTrue);
        }
        [TestMethod]
        public void TestLoginAndPasswordAuthentication_False()
        {
            var form1 = new Form1();
            var correctLoginAndPasswordTest = form1.UserLogin("iviviv1990", "11111");
            var correctLoginAndPasswordTrue = false;
            Assert.AreEqual(correctLoginAndPasswordTest, correctLoginAndPasswordTrue);
        }
    }
}
