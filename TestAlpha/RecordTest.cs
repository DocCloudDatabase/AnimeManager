using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimeReport.subelements;
using AnimeReport;
namespace TestAlpha
{
    [TestClass]
    public class RecordTest
    {

        [TestMethod]
        public void TestVisual()
        {
            MainWindow win = new MainWindow();
            try
            {
                var flag = win.ShowDialog();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void TestInit()
        {
            ReportRecord _base = new ReportRecord();
        }

        [TestMethod]
        public void TestSimple()
        {
            ReportRecord simple = new ReportRecord("Acchi Kocchi", 10, 8, "Fansub Name");
        }

        [TestMethod]
        public void TestComplete()
        {
            ReportRecord full = new ReportRecord("Titolo serie", 10, 8, "Fansub Name", RecordStatus.Stall, 8);
        }

        [TestMethod]
        public void PropertyTest()
        {
            ReportRecord full = new ReportRecord("abcd", 10, 8, "def", RecordStatus.Stall, 8);
            Assert.AreEqual("abcd", full.Title);
            Assert.AreEqual(10, full.Available);
            Assert.AreEqual(8, full.Download);
            Assert.AreEqual("def", full.Source);
            Assert.AreEqual("Stall", full.Status);
            Assert.AreEqual(8, full.Ausiliar);
        }

        [TestMethod]
        public void TestFill()
        {
            ReportRecord record = new ReportRecord("Titolo", 10, 8, "Fansub");
            try
            {
                ReportRow row = new ReportRow(record);
                row = new ReportRow();
            }catch(Exception ex){
                Assert.Fail(ex.ToString());
            }
        }
    }
}
