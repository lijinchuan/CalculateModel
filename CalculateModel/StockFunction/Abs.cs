using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Abs:StockFun
    {
        public Abs(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return new CalResult
            {
                Result = Math.Abs((double)RightSigelVal),
                ResultType = typeof(double)
            };
        }

        protected override CalResult CollectOperate()
        {
            if (CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result=Math.Abs((double)this.RightCollVal[CalCurrent.CurrentIndex]),
                    ResultType=typeof(double)
                };
            }

            return new CalResult
            {
                Results = this.RightCollVal.Select(s => (object)Math.Abs((double)s)).ToArray(),
                ResultType = typeof(double)
            };
        }

        public override int Params
        {
            get
            {
                return 1;
            }
        }
    }
}
