using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class SAR : StockFun,IIteratorCollectionFun
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
                         Result=valueCach[this.CalCurrent.CurrentIndex],
                         ResultType=typeof(decimal)
                     };
                 }
             }

             CalResult result = new CalResult();
             result.ResultType = typeof(decimal[]);
             int day = (int)param1;
             decimal af = param2.ToDecimal() / 100;
             decimal aaf = param3.ToDecimal() / 100;
             decimal rp = param4.ToDecimal() / 100;

             result.Results = new object[this.StockQuotes.Length];
             decimal max = decimal.MinValue;
             decimal min = decimal.MaxValue;
             decimal af2 = af;
             decimal aaf2 = aaf;
             decimal sar = 0m;
             bool isUP = true;
             for (int i = this.StockQuotes.Length - 1; i >= 0; i--)
             {
                 var qt = StockQuotes[i];

                 if (i > StockQuotes.Length - day)
                 {
                     if (qt.High > max)
                         max = qt.High;
                     if (qt.Low < min)
                         min = qt.Low;
                     result.Results[i] = 0;
                     continue;
                 }
                 else if (i == StockQuotes.Length - day)
                 {
                     if (qt.High > max)
                         max = qt.High;
                     if (qt.Low < min)
                         min = qt.Low;
                     result.Results[i] = min;
                     isUP = qt.Close > min;
                     sar = min;
                     continue;
                 }

                 if (isUP)
                 {
                     if (qt.High > max && af2 + aaf2 < rp)
                     {
                         max = qt.High;
                         af2 += aaf2;
                     }
                     decimal newSar = (max - sar) * af2 + sar;
                     if (newSar > qt.Low)
                     {
                         isUP = false;
                         af2 = af;
                         sar = max - (max - sar) * af;
                         min = qt.Low;
                     }
                     else
                     {
                         sar = newSar;
                     }
                 }
                 else
                 {
                     if (qt.Low < min && af2 + aaf2 < rp)
                     {
                         min = qt.Low;
                         af2 += aaf2;
                     }
                     decimal newsar = sar + (min - sar) * af2;
                     if (newsar < qt.High)
                     {
                         isUP = true;
                         sar = Math.Min(StockQuotes[i + 1].Low, qt.Low);
                         af2 = af;
                         max = qt.High;
                     }
                     else
                     {
                         sar = newsar;
                     }
                 }
                 sar = sar.ToDecimal(2);
                 result.Results[i] = sar;
             }

             valueCach = result.Results;
             if (CalCurrent.CurrentIndex > -1)
             {
                 return new CalResult
                 {
                     Result = result.Results[CalCurrent.CurrentIndex],
                     ResultType = typeof(decimal)
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
