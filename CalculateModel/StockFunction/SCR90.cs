using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    /// <summary>
    /// 筹码集中度90
    /// </summary>
    internal class SCR90 : StockFun, IIteratorCollectionFun
    {
        public SCR90(CalCurrent pool) : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }

        protected override CalResult CollectOperate()
        {
            if (valueCach == null)
            {
                valueCach = this.GetSCR().SelectMany(q => q.ComputePercentChipsResults.Where(m => m.Percent == 0.9).Select(n => (object)n.Concentration)).ToArray();
            }

            if (this.CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result = valueCach[CalCurrent.CurrentIndex],
                    ResultType = typeof(double)
                };
            }

            return new CalResult
            {
                Results = valueCach,
                ResultType = typeof(double)
            };
        }
    }
}
