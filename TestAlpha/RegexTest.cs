using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary.RegUtili;
namespace TestAlpha
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void NoExtensionCheck()
        {
            string ext = URegex.GetExtension("filename");
            Assert.IsNull(ext);
        }

        [TestMethod]
        public void ExtensionCheck()
        {
            string ext = URegex.GetExtension("c:\\prova\\pprova1\\filename.ext");
            Assert.AreEqual(".ext", ext);
        }

        [TestMethod]
        public void MultiPointCheck()
        {
            string ext = URegex.GetExtension("prova.ext1.ext2.ext3.right");
            Assert.AreEqual(".right", ext);
        }
    }
}
