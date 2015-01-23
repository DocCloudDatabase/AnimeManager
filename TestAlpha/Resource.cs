using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary.Reports;
namespace TestAlpha
{
    [TestClass]
    public class ResourceTest
    {
        private ListResource<string> resTest = new ListResource<string>("0.1.0;Image Extension List;25/12/2014;Start from index 1,.png,.jpeg,.jpg,.bmp");
        [TestMethod]
        public void TestParse()
        {
            bool flag = true;
            try
            {
                ListResource<string> test = new ListResource<string>("qwoieoiqwjejqw;weqoue;ejqowje;");
            }
            catch (Exception)
            {
                flag = false;
            }
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void VersionCheck()
        {
            Assert.AreEqual("0.1.0", resTest.Version);
        }

        [TestMethod]
        public void DescriptionCheck()
        {
            Assert.AreEqual("Image Extension List", resTest.Description);
        }

        [TestMethod]
        public void DateTimeCheck()
        {
            Assert.AreNotEqual(DateTime.MinValue, resTest.LastUpdate);
        }

        [TestMethod]
        public void CommentsCheck()
        {
            Assert.AreEqual("Start from index 1", resTest.Comments);
        }

        [TestMethod]
        public void ValueAccessCheck()
        {
            Assert.AreEqual(".png", resTest.Items[1]);
        }

        [TestMethod]
        public void ContainsCheck()
        {
            Assert.IsTrue(resTest.Contains(".png"));
            Assert.IsFalse(resTest.Contains(".gif"));
        }
    }
}
