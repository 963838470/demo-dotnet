﻿using NUnit.Framework;
using MathCalculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCalculate.MathCalculateNUnit3Tests
{
    [TestFixture()]
    public class DivisionTests
    {
        public static DateTime date = DateTime.Now;

        [Test()]
        [TestCase(700, 100, ExpectedResult = 7)]
        [TestCase(3, 2, ExpectedResult = 1.5)]
        [TestCase(0, 2, ExpectedResult = 0)]
        [TestCase(-3, 1, ExpectedResult = -3)]
        public double CalculationTest(double divisor, double dividend)
        {
            return Division.Calculation(divisor, dividend);
        }

        [Test()]
        public void CalculationTest_DivideZero_ThrowException()
        {
            Assert.Throws<DivideByZeroException>(delegate ()
            {
                Division.Calculation(12, 0);
            });
        }

        /// <summary>
        /// 获取格式化的时间文本
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public static string GetFormatDateStr(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}