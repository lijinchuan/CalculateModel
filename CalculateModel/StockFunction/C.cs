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
            if (this.valueCach == null)
            {
                this.valueCach = this.StockQuotes.Select(q => (object)q.Close).ToArray();
            }
            return new CalResult
            {
                Results = this.valueCach,
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
