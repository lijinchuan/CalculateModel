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
                var quotes = CurrStockDataCalPool.Quotes;
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
