﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using System;
using System.Drawing;

namespace EPPlusTest.Core.Range
{
    [TestClass]
    public class RangeColumnRowTests : TestBase
    {
        static ExcelPackage _pck;
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _pck = OpenPackage("Range_RowColumn.xlsx", true);
        }
        [ClassCleanup]
        public static void Cleanup()
        {
            SaveAndCleanup(_pck);
        }

        [TestMethod]
        public void Column_SetWidthBestFitAndStyle()
        {
            var ws = _pck.Workbook.Worksheets.Add("Column_Width");
            ws.Cells["A1:E5"].EntireColumn.Width = 30;
            ws.Cells["A1:E5"].EntireColumn.Style.Fill.SetBackground(Color.Red);

            ws.Cells["C10:C20"].EntireColumn.BestFit=true;

            Assert.AreEqual(30, ws.Cells["A1"].EntireColumn.Width);
            Assert.AreEqual(30, ws.Cells["C1"].EntireColumn.Width);
            Assert.AreEqual(30, ws.Cells["D1"].EntireColumn.Width);
            Assert.IsFalse(ws.Cells["B1"].EntireColumn.BestFit);
            Assert.IsTrue(ws.Cells["C1"].EntireColumn.BestFit);
            Assert.IsFalse(ws.Cells["D1"].EntireColumn.BestFit);

            Assert.AreEqual("FFFF0000", ws.Cells["E100"].EntireColumn.Style.Fill.BackgroundColor.Rgb);
        }

        [TestMethod]
        public void Column_SetPhonetic()
        {
            var ws = _pck.Workbook.Worksheets.Add("Column_Phonetic");
            ws.Cells["D1:G5"].EntireColumn.Phonetic = true;
            ws.Cells["E1000:E5000"].EntireColumn.Phonetic = false;

            Assert.IsFalse(ws.Cells["C1"].EntireColumn.Phonetic);
            Assert.IsTrue(ws.Cells["D1"].EntireColumn.Phonetic);
            Assert.IsFalse(ws.Cells["E1"].EntireColumn.Phonetic);
            Assert.IsTrue(ws.Cells["G1"].EntireColumn.Phonetic);
            Assert.IsFalse(ws.Cells["H1"].EntireColumn.Phonetic);
        }


        [TestMethod]
        public void Column_SetMerge()
        {
            var ws = _pck.Workbook.Worksheets.Add("Column_Merge");
            ws.Cells["D1:G5"].EntireColumn.Merged = true;

            Assert.IsFalse(ws.Cells["C1"].EntireColumn.Merged);
            Assert.IsTrue(ws.Cells["D1"].EntireColumn.Merged);
            Assert.IsTrue(ws.Cells["G1"].EntireColumn.Merged);
            Assert.IsFalse(ws.Cells["H1"].EntireColumn.Merged);
        }
        [TestMethod]
        public void Column_SetHidden()
        {
            var ws = _pck.Workbook.Worksheets.Add("Column_Hidden");
            ws.Cells["F1:J5"].EntireColumn.Hidden = true;
            ws.Cells["G1"].EntireColumn.Hidden = false;

            Assert.IsFalse(ws.Cells["E10"].EntireColumn.Hidden);
            Assert.IsFalse(ws.Cells["G10"].EntireColumn.Hidden);
            Assert.IsFalse(ws.Cells["K10"].EntireColumn.Hidden);
            Assert.IsTrue(ws.Cells["F1"].EntireColumn.Hidden);
            Assert.IsTrue(ws.Cells["H1"].EntireColumn.Hidden);
            Assert.IsTrue(ws.Cells["I1"].EntireColumn.Hidden);
            Assert.IsTrue(ws.Cells["J1"].EntireColumn.Hidden);
        }
        [TestMethod]
        public void Column_SetStyleName()
        {
            var styleName = "Green Fill";
            var ns = _pck.Workbook.Styles.CreateNamedStyle(styleName);
            ns.Style.Fill.SetBackground(Color.Green);
            var ws = _pck.Workbook.Worksheets.Add("Column_StyleName"); 
            
            ws.Cells["C15:J20"].EntireColumn.StyleName = "Green Fill";

            Assert.AreEqual("Green Fill", ws.Cells["E10"].EntireColumn.StyleName);
        }
        [TestMethod]
        public void Row_SetStyleName()
        {
            var styleName = "Blue Fill";
            var ns = _pck.Workbook.Styles.CreateNamedStyle(styleName);
            ns.Style.Fill.SetBackground(Color.Blue);
            var ws = _pck.Workbook.Worksheets.Add("Row_StyleName");

            ws.Cells["C15:J20"].EntireRow.StyleName = styleName;

            Assert.AreEqual("Blue Fill", ws.Cells["E16"].EntireRow.StyleName);
        }
        [TestMethod]
        public void Row_SetPhonetic()
        {
            var ws = _pck.Workbook.Worksheets.Add("Row_Phonetic");

            ws.Cells["G15:K20"].EntireRow.Phonetic = true;
            ws.Cells["I17:J18"].EntireRow.Phonetic = false;

            Assert.IsFalse(ws.Cells["F14"].EntireRow.Phonetic);
            Assert.IsFalse(ws.Cells["I17"].EntireRow.Phonetic);
            Assert.IsFalse(ws.Cells["J18"].EntireRow.Phonetic);
            Assert.IsFalse(ws.Cells["L21"].EntireRow.Phonetic);

            Assert.IsTrue(ws.Cells["G15"].EntireRow.Phonetic);
            Assert.IsTrue(ws.Cells["H16"].EntireRow.Phonetic);
            Assert.IsTrue(ws.Cells["K19"].EntireRow.Phonetic);
        }
    }
}