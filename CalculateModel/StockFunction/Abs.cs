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
                Result = Math.Abs((decimal)RightSigelVal),
                ResultType = typeof(decimal)
            };
        }

        protected override CalResult CollectOperate()
        {
            if (CalCurrent.CurrentIndex > -1)
            {
                return new CalResult
                {
                    Result=Math.Abs((decimal)this.RightCollVal[CalCurrent.CurrentIndex]),
                    ResultType=typeof(decimal)
                };
            }

            return new CalResult
            {
                Results = this.RightCollVal.Select(s => (object)Math.Abs((decimal)s)).ToArray(),
                ResultType = typeof(decimal)
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
