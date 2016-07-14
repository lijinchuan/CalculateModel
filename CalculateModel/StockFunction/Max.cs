using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Max:StockFun,IIteratorCollectionFun
    {
        public Max(CalCurrent pool)
            : base(pool)
        {

        }

        public override string Sign
        {
            get
            {
                return "Max";
            }
        }

        protected override CalResult CollectOperate()
        {
            CalResult result = new CalResult();
            if (param1 is object[])
            {
                object[] p1 = (object[])param1;
                result.Results = new object[p1.Length];
                decimal m = decimal.MinValue;
                if (param2 == null)
                {
                    for (int i = p1.Length-1; i >=0; i--)
                    {
                        if ((decimal)p1[i] > m)
                            m = (decimal)p1[i];
                        result.Results[i] = m;
                    }
                }
                else
                {
                    if (param2 is object[])
                    {
                        object[] p2 = (object[])param2;
                        if (p2.Length != p1.Length)
                            throw new ExpressErrorException("Max方法两边操作数不相等");
                        for (int i = 0; i < p1.Length; i++)
                        {
                            result.Results[i] = Math.Max((decimal)p1[i], (decimal)p2[i]);
                        }
                    }
                    else
                    {
                        decimal p2 = (decimal)param2;
                        for (int i = 0; i < p1.Length; i++)
                        {
                            result.Results[i] = Math.Max((decimal)p1[i], p2);
                        }
                    }
                }
            }
            else
            {
                if (!(param2 is object[]))
                    throw new ExpressErrorException("Max方法参数错误，第二参数不是集合");
                decimal p1 = (decimal)param1;
                object[] p2 = (object[])param2;
                result.Results=new object[p2.Length];
                for (int i = 0; i < p2.Length; i++)
                {
                    result.Results[i] = Math.Max(p1, (decimal)p2[i]);
                }
            }

            return result;
        }

        protected override CalResult SingOperate()
        {
            return new CalResult
            {
                ResultType = typeof(decimal),
                Result = Math.Max((decimal)param1, (decimal)param2)
            };
        }
    }
}
