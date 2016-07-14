using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Low : StockFun
    {
        public Low(CalCurrent pool)
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
                    Result = CurrQuote.Low,
                    ResultType = typeof(decimal)
                };
            }

            return new CalResult
            {
                Results = this.StockQuotes.Select(q => (object)q.Low).ToArray(),
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
