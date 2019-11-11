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
    internal class V:StockFun
    {
        public V(CalCurrent pool)
            :base(pool)
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
                    Result = CurrQuote.Volumne,
                    ResultType = typeof(double)
                };
            }

            return new CalResult
            {
                Results = this.StockQuotes.Select(q => (object)q.Volumne).ToArray(),
                //ResultType = typeof(decimal)
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
