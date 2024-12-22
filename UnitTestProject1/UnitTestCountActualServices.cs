using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFitnessClub;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestCountActualServices
    {
        [TestMethod]
        public void TestCountActualServices_ForClients()
        {
            var f1 = new Form1();
            var formAdmin = new FormAdmin("/adminMED", f1);
            var date = new DateTime(2024,10,1);
            var formTableForm = new TableForm(formAdmin, true, date);
            formTableForm.GetClintsData();
            var countedServicesTest = formTableForm.CounterServices();
            var countedServicesTrue = 1;
            Assert.AreEqual(countedServicesTrue, countedServicesTest);
        }
        [TestMethod]
        public void TestCountActualServices_ForCouches()
        {
            var form1 = new Form1();
            var formAdmin = new FormAdmin("/adminMED", form1);
            var date = new DateTime(2024, 12, 1);
            var formTableForm = new TableForm(formAdmin, false, date);
            formTableForm.GetCouchesData();
            var countedServicesTest = formTableForm.CounterServices();
            var countedServicesTrue = 4;
            Assert.AreEqual(countedServicesTrue, countedServicesTest);
        }
    }
}
