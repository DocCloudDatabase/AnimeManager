using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibrary.mIO;
using System.Runtime.InteropServices;

namespace TestAlpha
{

    internal struct Prova
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string prima;
        public int seconda;
    }

    [TestClass]
    public class FileTest
    {
        /*
         * NOTA ESEGUIRE I TEST UNO ALLA VOLTA NEL ORDINE DI LETTURA
         * */
        private const string filepath = "prova.bin";
        [TestMethod]
        public void GeneralTest()
        {
            Prova[] tests = new Prova[3];
            tests[0].prima = "qualcosa";
            tests[1].prima = "altro";
            tests[2].prima = "vecchio";

            tests[0].seconda = 10;
            tests[1].seconda = 5;
            tests[2].seconda = 1;

            int written = UFile.WriteRecords<Prova>(filepath, tests);
            Assert.AreEqual<int>(3*UFile.SizeOfRecord<Prova>(), written);
        }

        [TestMethod]
        public void ReadTest()
        {
            Prova[] results = UFile.ReadRecords<Prova>(filepath);
            Assert.IsNotNull(results);
            Assert.AreEqual<int>(3, results.Length);
            Assert.AreEqual<string>(results[0].prima, "qualcosa");
            Assert.AreEqual<int>(results[1].seconda, 5);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.IsTrue(UFile.DeleteRecord<Prova, int>(filepath, CheckRecord, 5));
            Prova[] found = UFile.ReadRecords<Prova>(filepath);
            Assert.AreEqual<int>(2, found.Length);
            Assert.AreEqual<string>(found[0].prima, "qualcosa");
        }

        [TestMethod]
        public void UpdateTest()
        {
            Prova uno = new Prova();
            uno.seconda = 1;
            uno.prima = "nuovo";
            
            Assert.AreEqual<int>(1, UFile.UpdateRecords<Prova>(filepath, new Prova[]{uno}, AreEqual));
            Prova[] ris = UFile.ReadRecords<Prova>(filepath);
            Assert.IsNotNull(ris);
            Assert.AreEqual<string>("nuovo", ris[2].prima);
        }

        internal bool AreEqual(Prova left, Prova right)
        {
            return left.seconda == right.seconda;
        }

        internal bool CheckRecord(Prova input, int index)
        {
            return input.seconda == index;
        }
    }
}
