using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class SAR : StockFun, IIteratorCollectionFun
    {
        //4,2,2.20
        public SAR(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult CollectOperate()
        {
            if (this.CalCurrent.CurrentIndex > -1)
            {
                if (valueCach != null)
                {
                    return new CalResult
                    {
                        Result = valueCach[this.CalCurrent.CurrentIndex],
                        ResultType = typeof(double)
                    };
                }
            }

            CalResult result = new CalResult();
            result.ResultType = typeof(double[]);
            int day = (int)param1;
            double af = param2.ToDouble() / 100;
            double aaf = param3.ToDouble() / 100;
            double rp = param4.ToDouble() / 100;

            result.Results = new object[this.StockQuotes.Length];
            double max = double.MinValue;
            double min = double.MaxValue;
            double af2 = af;
            double aaf2 = aaf;
            double sar = 0;
            bool isUP = true;
            for (int i = 0; i < this.StockQuotes.Length; i++)
            {
                var qt = StockQuotes[i];

                if (i < day - 1)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    result.Results[i] = 0d;
                    continue;
                }
                else if (i == day - 1)
                {
                    if ((double)qt.High > max)
                        max = (double)qt.High;
                    if ((double)qt.Low < min)
                        min = (double)qt.Low;
                    result.Results[i] = min;
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
                        sar = (double)Math.Min(StockQuotes[i - 1].Low, qt.Low);
                        af2 = af;
                        max = (double)qt.High;
                    }
                    else
                    {
                        sar = newsar;
                    }
                }
                sar = sar.ToDouble(2);
                result.Results[i] = sar;
            }

            valueCach = result.Results;
            if (CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = result.Results[CalCurrent.CurrentIndex],
                    ResultType = typeof(double)
                };
            }

            return result;
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        public override int Params
        {
            get
            {
                return 4;
            }
        }
    }
}
