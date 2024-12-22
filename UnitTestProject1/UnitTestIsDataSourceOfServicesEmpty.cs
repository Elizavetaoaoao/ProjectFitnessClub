using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFitnessClub;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestIsDataSourceOfServicesEmpty
    {

        [TestMethod]
        public void TestIsDataSourceOfServicesForClientEmpty_False()
        {
            var form1 = new Form1();
            var formProfile = new FormProfile("iviviv1990", "юзер");
            formProfile.LoadUserServices("iviviv1990");
            var IsEmptyTest = formProfile.IsDataTableOfDGVNull();
            var IsEmptyTrue = false;
            Assert.AreEqual(IsEmptyTrue, IsEmptyTest);
        }
        [TestMethod]
        public void TestIsDataSourceOfServicesForClientEmpty_True()
        {
            var form1 = new Form1();
            var formProfile = new FormProfile("bhjhb", "null");
            formProfile.LoadUserServices("bhjhb");
            var IsEmptyTest = formProfile.IsDataTableOfDGVNull();
            var IsEmptyTrue = true;
            Assert.AreEqual(IsEmptyTrue, IsEmptyTest);
        }
    }
}
