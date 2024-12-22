using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFitnessClub;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsGettingRoleByLogin
    {
        [TestMethod]
        public void TestGettingRoleForTheUser_Admin()
        {
            var cT = new ClassTest();
            var roleTest = cT.GetRole("/adminMED");
            var roleTrue = "администратор";
            Assert.AreEqual(roleTest, roleTrue);
        }
        [TestMethod]
        public void TestGettingRoleForTheUser_Couch()
        {
            var cT = new ClassTest();
            var roleTest = cT.GetRole("/petrovpetr666");
            var roleTrue = "тренер";
            Assert.AreEqual(roleTest, roleTrue);
        }
        [TestMethod]
        public void TestGettingRoleForTheUser_Client()
        {
            var cT = new ClassTest();
            var roleTest = cT.GetRole("iviviv1990");
            var roleTrue = "юзер";
            Assert.AreEqual(roleTest, roleTrue);
        }
    }
}
