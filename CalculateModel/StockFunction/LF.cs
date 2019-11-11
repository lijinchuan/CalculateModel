using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 低价分形
    /// </summary>
    internal class LF : StockFun
    {
        public LF(CalCurrent pool)
            : base(pool)
        {
            
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        protected override CalResult CollectOperate()
        {
            if (valueCach == null)
            {
                object[] o = new object[CurrStockDataCalPool.Quotes.Length];

                int lastIndex = CurrStockDataCalPool.Quotes.Length - 3;
                double fxPrice = CurrStockDataCalPool.Quotes[lastIndex].Low;
                //是否有效
                bool isValid = false;

                for (int i = CurrStockDataCalPool.Quotes.Length - 1; i >= 0; i--)
                {
                    //倒数第三天才有
                    if (i > CurrStockDataCalPool.Quotes.Length - 3)
                    {
                        o[i] = 0d;
                        continue;
                    }

                    for (int j = lastIndex - 1; j > i + 2; j--)
                    {
                        if (CurrStockDataCalPool.Quotes[j].Low < fxPrice)
                        {
                            //旧的分形被突破
                            isValid = false;
                        }

                        if (CurrStockDataCalPool.Quotes[j - 2].Low >= CurrStockDataCalPool.Quotes[j].Low
                               && CurrStockDataCalPool.Quotes[j - 1].Low >= CurrStockDataCalPool.Quotes[j].Low
                               && CurrStockDataCalPool.Quotes[j].Low <= CurrStockDataCalPool.Quotes[j + 1].Low
                               && CurrStockDataCalPool.Quotes[j].Low <= CurrStockDataCalPool.Quotes[j + 2].Low)
                        {
                            //新的分形诞生
                            fxPrice = CurrStockDataCalPool.Quotes[j].Low;
                            lastIndex = j;
                            isValid = true;
                        }
                    }

                    //新的分形是否被突破,但最近两天不可能会产生新的分形
                    if (CurrStockDataCalPool.Quotes[i + 1].Low < fxPrice
                        || CurrStockDataCalPool.Quotes[i + 2].Low < fxPrice)
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

                valueCach = o;
            }

            CalResult result = new CalResult();
            if (CalCurrent.CurrentIndex >-1)
            {
                result.Result=valueCach[CalCurrent.CurrentIndex];
            }
            else
            {
                result.Results = valueCach;
            }
            result.ResultType = typeof(double);

            return result;
        }

        public override int Params
        {
            get
            {
                return 0;
            }
        }
    }
}
