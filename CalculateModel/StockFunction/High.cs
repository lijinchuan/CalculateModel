using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class High:StockFun
    {
        public High(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        public override int Params
        {
            get
            {
                return 0;
            }
        }

        protected override CalResult CollectOperate()
        {
            if (this.CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = this.CurrQuote.High,
                    ResultType = typeof(double)
                };
            }

            if (valueCach == null)
            {
                valueCach = this.StockQuotes.Select(q => (object)q.High).ToArray();
            }

            return new CalResult
            {
                Results = valueCach,
                ResultType = typeof(double)
            };
        }
    }
}
