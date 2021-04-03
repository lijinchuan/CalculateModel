using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATrade.CalculateModel.StockFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel.StockFunction.Tests
{
    [TestClass()]
    public class TestTests
    {
        public object[] TestHFOld()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").ToArray();
            object[] o = new object[quotes.Length];

            int lastIndex = quotes.Length - 3;
            double fxPrice = quotes[lastIndex].High;
            //是否有效
            bool isValid = false;

            for (int i = quotes.Length - 1; i >= 0; i--)
            {
                //倒数第三天才有
                if (i > quotes.Length - 3)
                {
                    o[i] = double.MaxValue;
                    continue;
                }

                for (int j = lastIndex - 1; j > i + 2; j--)
                {
                    if ((double)quotes[j].High > fxPrice)
                    {
                        //旧的分形被突破
                        isValid = false;
                    }

                    if (quotes[j - 2].High <= quotes[j].High
                          && quotes[j - 1].High <= quotes[j].High
                          && quotes[j].High >= quotes[j + 1].High
                          && quotes[j].High >= quotes[j + 2].High)
                    {
                        //新的分形诞生
                        fxPrice = (double)quotes[j].High;
                        lastIndex = j;
                        isValid = true;
                    }
                }

                //新的分形是否被突破,但最近两天不可能会产生新的分形
                if (quotes[i + 1].High > fxPrice
                    || quotes[i + 2].High > fxPrice)
                    isValid = false;

                if (isValid)
                {
                    o[i] = fxPrice;
                }
                else
                {
                    o[i] = double.MaxValue;
                }
            }
            return o;
        }

        [TestMethod()]
        public void TestHFTest()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").Reverse().ToArray();
            //ATrade.Data.StockQuote[] stockquotes = ATrade.Server.StockServer.GetHisDayQuote("000001.IX").Reverse().ToArray();
            //var quotes = stockquotes;
            object[] o = new object[quotes.Length];

            int lastIndex = 2;
            double fxPrice = quotes[lastIndex].High;
            //是否有效
            bool isValid = false;

            for (int i = 0; i < quotes.Length; i++)
            {
                //倒数第三天才有
                if (i < 2)
                {
                    o[i] = double.MaxValue;
                    continue;
                }

                for (int j = lastIndex + 1; j < i - 2; j++)
                {
                    if ((double)quotes[j].High > fxPrice)
                    {
                        //旧的分形被突破
                        isValid = false;
                    }

                    if (quotes[j - 2].High <= quotes[j].High
                          && quotes[j - 1].High <= quotes[j].High
                          && quotes[j].High >= quotes[j + 1].High
                          && quotes[j].High >= quotes[j + 2].High)
                    {
                        //新的分形诞生
                        fxPrice = (double)quotes[j].High;
                        lastIndex = j;
                        isValid = true;
                    }
                }

                //新的分形是否被突破,但最近两天不可能会产生新的分形
                if (quotes[i - 1].High > fxPrice
                    || quotes[i - 2].High > fxPrice)
                    isValid = false;

                if (isValid)
                {
                    o[i] = fxPrice;
                }
                else
                {
                    o[i] = double.MaxValue;
                }
            }

            var oo = TestHFOld().Reverse().ToArray();
            for (var i = 0; i < o.Length; i++)
            {
                if (((double)oo[i] - (double)o[i]) > 0.01)
                {

                }
            }
        }

        [TestMethod]
        public void TestLF()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").Reverse().ToArray();

            object[] o = new object[quotes.Length];

            int lastIndex = 2;
            double fxPrice = quotes[lastIndex].Low;
            //是否有效
            bool isValid = false;

            for (int i = 0; i < quotes.Length; i++)
            {
                //倒数第三天才有
                if (i < 2)
                {
                    o[i] = 0d;
                    continue;
                }

                for (int j = lastIndex + 1; j < i - 2; j++)
                {
                    if (quotes[j].Low < fxPrice)
                    {
                        //旧的分形被突破
                        isValid = false;
                    }

                    if (quotes[j - 2].Low >= quotes[j].Low
                           && quotes[j - 1].Low >= quotes[j].Low
                           && quotes[j].Low <= quotes[j + 1].Low
                           && quotes[j].Low <= quotes[j + 2].Low)
                    {
                        //新的分形诞生
                        fxPrice = quotes[j].Low;
                        lastIndex = j;
                        isValid = true;
                    }
                }

                //新的分形是否被突破,但最近两天不可能会产生新的分形
                if (quotes[i - 1].Low < fxPrice
                    || quotes[i - 2].Low < fxPrice)
                    isValid = false;

                if (isValid)
                {
                    o[i] = fxPrice;
                }
                else
                {
                    o[i] = 0d;
                }
            }

            var oo = TestOldLF().Reverse().ToArray();
            for (var i = 0; i < o.Length; i++)
            {
                if (((double)oo[i] - (double)o[i]) > 0.01)
                {

                }
            }
        }

        public object[] TestOldLF()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").ToArray();
            object[] o = new object[quotes.Length];

            int lastIndex = quotes.Length - 3;
            double fxPrice = quotes[lastIndex].Low;
            //是否有效
            bool isValid = false;

            for (int i = quotes.Length - 1; i >= 0; i--)
            {
                //倒数第三天才有
                if (i > quotes.Length - 3)
                {
                    o[i] = 0d;
                    continue;
                }

                for (int j = lastIndex - 1; j > i + 2; j--)
                {
                    if (quotes[j].Low < fxPrice)
                    {
                        //旧的分形被突破
                        isValid = false;
                    }

                    if (quotes[j - 2].Low >= quotes[j].Low
                           && quotes[j - 1].Low >= quotes[j].Low
                           && quotes[j].Low <= quotes[j + 1].Low
                           && quotes[j].Low <= quotes[j + 2].Low)
                    {
                        //新的分形诞生
                        fxPrice = quotes[j].Low;
                        lastIndex = j;
                        isValid = true;
                    }
                }

                //新的分形是否被突破,但最近两天不可能会产生新的分形
                if (quotes[i + 1].Low < fxPrice
                    || quotes[i + 2].Low < fxPrice)
                    isValid = false;

                if (isValid)
                {
                    o[i] = fxPrice;
                }
                else
                {
                    o[i] = 0d;
                }
            }

            return o;
        }

        public object[] TestMAOld()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").ToArray();
            var data = quotes.Select(p => p.Close).ToArray();
            var result = new object[data.Length];
            double sum = 0;
            var count = 5;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                if (i > data.Length - count)
                {
                    result[i] = 0d;
                    sum += (double)data[i];
                }
                else
                {
                    sum += (double)data[i];
                    result[i] = sum / count;
                    sum -= (double)data[i + count - 1];
                }
            }
            return result;
        }

        [TestMethod]
        public void TestMA()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").Reverse().ToArray();
            var data = quotes.Select(p => p.Close).ToArray();
            var result = new object[data.Length];
            double sum = 0;
            var count = 5;

            for (var i = 0; i < data.Length; i++)
            {
                if (i < count - 1)
                {
                    result[i] = 0d;
                    sum += (double)data[i];
                }
                else
                {
                    sum += (double)data[i];
                    result[i] = sum / count;
                    sum -= (double)data[i - count + 1];
                }
            }

            var oo = TestMAOld().Reverse().ToArray();
            for (var i = 0; i < result.Length; i++)
            {
                if (((double)oo[i] - (double)result[i]) > 0.01)
                {

                }
            }
        }


        public object[] SMAOldTest()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").ToArray();
            var data = quotes.Select(p => p.Close).ToArray();
            var result = new object[data.Length];
            var count = 10;
            int day = 5;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (i == data.Length - 1)
                    result[i] = (double)data[i];
                else
                {
                    result[i] = (((double)data[i] * day + (double)result[i + 1] * (count - day)) / count);
                }
            }

            return result;
        }

        [TestMethod]
        public void TestSMA()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").Reverse().ToArray();
            var data = quotes.Select(p => p.Close).ToArray();
            var result = new object[data.Length];
            var count = 10;
            var day = 5;

            for (var i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    result[i] = (double)data[i];
                }
                else
                {
                    result[i] = ((double)data[i] * day + (double)result[i - 1] * (count - day)) / count;
                }
            }

            var oo = SMAOldTest().Reverse().ToArray();
            for (var i = 0; i < result.Length; i++)
            {
                if (((double)oo[i] - (double)result[i]) > 0.01)
                {

                }
            }
        }

        public object[] TestSAROld()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").ToArray();
            var data = quotes.Select(p => p.Close).ToArray();

            int day = 4;
            double af = 2;
            double aaf = 2;
            double rp = 20;

            var results = new object[quotes.Length];
            double max = double.MinValue;
            double min = double.MaxValue;
            double af2 = af;
            double aaf2 = aaf;
            double sar = 0;
            bool isUP = true;
            for (int i = quotes.Length - 1; i >= 0; i--)
            {
                var qt = quotes[i];

                if (i > quotes.Length - day)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    results[i] = 0d;
                    continue;
                }
                else if (i == quotes.Length - day)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    results[i] = min;
                    isUP = (double)qt.Close > min;
                    sar = min;
                    continue;
                }

                if (isUP)
                {
                    if ((double)qt.High > max && af2 + aaf2 < rp)
                    {
                        max = (double)qt.High;
                        af2 += aaf2;
                    }
                    double newSar = (max - sar) * af2 + sar;
                    if (newSar > (double)qt.Low)
                    {
                        isUP = false;
                        af2 = af;
                        sar = max - (max - sar) * af;
                        min = (double)qt.Low;
                    }
                    else
                    {
                        sar = newSar;
                    }
                }
                else
                {
                    if ((double)qt.Low < min && af2 + aaf2 < rp)
                    {
                        min = (double)qt.Low;
                        af2 += aaf2;
                    }
                    double newsar = sar + (min - sar) * af2;
                    if (newsar < (double)qt.High)
                    {
                        isUP = true;
                        sar = (double)Math.Min(quotes[i + 1].Low, qt.Low);
                        af2 = af;
                        max = (double)qt.High;
                    }
                    else
                    {
                        sar = newsar;
                    }
                }
                results[i] = sar;
            }
            return results;
        }

        [TestMethod]
        public void TestSAR()
        {
            var quotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>("000001.IX.xml").Reverse().ToArray();

            int day = 4;
            double af = 2;
            double aaf = 2;
            double rp = 20;

            var results = new object[quotes.Length];
            double max = double.MinValue;
            double min = double.MaxValue;
            double af2 = af;
            double aaf2 = aaf;
            double sar = 0;
            bool isUP = true;
            for (int i = 0; i < quotes.Length; i++)
            {
                var qt = quotes[i];

                if (i < day - 1)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    results[i] = 0d;
                    continue;
                }
                else if (i == day - 1)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    results[i] = min;
                    isUP = (double)qt.Close > min;
                    sar = min;
                    continue;
                }

                if (isUP)
                {
                    if ((double)qt.High > max && af2 + aaf2 < rp)
                    {
                        max = (double)qt.High;
                        af2 += aaf2;
                    }
                    double newSar = (max - sar) * af2 + sar;
                    if (newSar > (double)qt.Low)
                    {
                        isUP = false;
                        af2 = af;
                        sar = max - (max - sar) * af;
                        min = (double)qt.Low;
                    }
                    else
                    {
                        sar = newSar;
                    }
                }
                else
                {
                    if ((double)qt.Low < min && af2 + aaf2 < rp)
                    {
                        min = (double)qt.Low;
                        af2 += aaf2;
                    }
                    double newsar = sar + (min - sar) * af2;
                    if (newsar < (double)qt.High)
                    {
                        isUP = true;
                        sar = (double)Math.Min(quotes[i - 1].Low, qt.Low);
                        af2 = af;
                        max = (double)qt.High;
                    }
                    else
                    {
                        sar = newsar;
                    }
                }
                results[i] = sar;
            }

            var ooold = TestSAROld();
            var oo = ooold.Reverse().ToArray();
            for (var i = 0; i < results.Length; i++)
            {
                try
                {
                    if (((double)oo[i] - (double)results[i]) > 0.01)
                    {

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}