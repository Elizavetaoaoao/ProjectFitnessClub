using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFitnessClub;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestHashing
    {
        [TestMethod]
        public void TestHashing_Password()
        {
            var hashedpassTest = Hasher.HashPassword("12345");
            var hashedpassTrue = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5";
            Assert.AreEqual(hashedpassTrue, hashedpassTest);
        }
    }
}
