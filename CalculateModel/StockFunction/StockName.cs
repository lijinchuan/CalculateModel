using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    internal class StockName : StockFun
    {
        public StockName(CalCurrent pool) : base(pool)
        {

        }

        protected override CalResult CollectOperate()
        {
            return SingOperate();
        }

        protected override CalResult SingOperate()
        {
            return new CalResult
            {
                Result = this.CurrStockDataCalPool.Stock.StockName??string.Empty,
                ResultType = typeof(string)
            };
        }
    }
}
