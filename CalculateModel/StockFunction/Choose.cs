using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    internal class Choose:StockFun
    {
        public Choose(CalCurrent pool) : base(pool)
        {

        }

        public override int Params => 0;

        protected override CalResult CollectOperate()
        {
            return SingOperate();
        }

        protected override CalResult SingOperate()
        {
            return new CalResult
            {
                Result=this.CurrStockDataCalPool.Stock.StockCode,
                ResultType=typeof(string)
            };
        }
    }
}
