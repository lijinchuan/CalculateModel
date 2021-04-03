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
                    ResultType = typeof(double)
                };
            }

            if (valueCach == null)
            {
                valueCach = this.StockQuotes.Select(q => (object)q.Low).ToArray();
            }

            return new CalResult
            {
                Results = valueCach,
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
