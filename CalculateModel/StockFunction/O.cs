using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 取股票收盘价
    /// </summary>
    internal class O : StockFun
    {
        public O(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        protected override CalResult CollectOperate()
        {
            if (CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = this.CurrQuote.Open,
                    ResultType = typeof(decimal)
                };
            }

            return new CalResult
            {
                Results = CurrStockDataCalPool.Quotes.Select(q => q.Open.ToString()).ToArray(),
                ResultType = typeof(decimal)
            };
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
