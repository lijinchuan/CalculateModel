using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    internal class TradeTime:StockFun
    {
        public TradeTime(CalCurrent pool) : base(pool)
        {
            
        }

        protected override CalResult CollectOperate()
        {
            if (CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = this.CurrQuote.Time,
                    ResultType = typeof(DateTime)
                };
            }

            if (valueCach == null)
            {
                valueCach = CurrStockDataCalPool.Quotes.Select(q => (object)q.Time).ToArray();
            }

            return new CalResult
            {
                Results = valueCach,
                ResultType = typeof(DateTime)
            };
        }

        public override int Params => 0;

        public override CalResult Operate()
        {
            return CollectOperate();
        }

        
    }
}
