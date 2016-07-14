using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Today:StockFun
    {
        public Today(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult CollectOperate()
        {
            return SingOperate();
        }

        protected override CalResult SingOperate()
        {
            if (CurrStockDataCalPool.Quotes[0].Time
                <= DateTime.Now.AddDays(-2).Date)
            {
                return null;
            }

            if (param1 is object[])
            {
                return new CalResult
                {
                    Result= param1.ToArr()[0],
                    ResultType=typeof(object)
                };
            }

            return null;
        }
            
    }
}
