using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 取股票成交量
    /// </summary>
    internal class C : StockFun
    {
        public C(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        protected override CalResult CollectOperate()
        {
            if (this.CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = CurrQuote.Close,
                    ResultType = typeof(double)
                };
            }
            return new CalResult
            {
                Results = this.StockQuotes.Select(q => (object)q.Close).ToArray(),
                ResultType = typeof(double)
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
