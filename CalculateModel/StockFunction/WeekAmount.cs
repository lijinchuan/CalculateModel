using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    internal class WeekAmount: StockFun
    {
        public WeekAmount(CalCurrent pool)
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
                    Result = this.WeekStockQuotes[this.CalCurrent.CurrentIndex].Amount,
                    ResultType = typeof(double)
                };
            }

            if (this.valueCach == null)
            {
                this.valueCach = this.WeekStockQuotes.Select(q => (object)q.Amount).ToArray();
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
