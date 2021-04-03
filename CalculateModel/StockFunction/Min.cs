using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Min:StockFun,IIteratorCollectionFun
    {
        public Min(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult CollectOperate()
        {
            CalResult result = new CalResult();
            if (param1 is object[])
            {
                object[] p1 = (object[])param1;
                result.Results = new object[p1.Length];
                double m = double.MaxValue;
                if (param2 == null)
                {
                    for (var i = 0; i < p1.Length; i++)
                    //for (int i = p1.Length-1; i >=0; i--)
                    {
                        if (p1[i].ToDouble(2) < m)
                            m = p1[i].ToDouble(2);
                        result.Results[i] = m;
                    }
                }
                else
                {
                    if (param2 is object[])
                    {
                        object[] p2 = (object[])param2;
                        if (p2.Length != p1.Length)
                            throw new ExpressErrorException("Min方法两边操作数不相等");
                        for (int i = 0; i < p1.Length; i++)
                        {
                            result.Results[i] = Math.Min(p1[i].ToDouble(2), p2[i].ToDouble(2));
                        }
                    }
                    else
                    {
                        double p2 = param2.ToDouble(2);
                        for (int i = 0; i < p1.Length; i++)
                        {
                            result.Results[i] = Math.Min(p1[i].ToDouble(2), p2);
                        }
                    }
                }
            }
            else
            {
                if (!(param2 is object[]))
                    throw new ExpressErrorException("Max方法参数错误，第二参数不是集合");
                double p1 = param1.ToDouble(2);
                object[] p2 = (object[])param2;
                result.Results = new object[p2.Length];
                for (int i = 0; i < p2.Length; i++)
                {
                    result.Results[i] = Math.Min(p1, p2[i].ToDouble(2));
                }
            }

            return result;
        }

        protected override CalResult SingOperate()
        {
            return new CalResult
            {
                ResultType = typeof(double),
                Result = Math.Min(double.Parse(param1.ToString()), double.Parse(param2.ToString()))
            };
        }
    }
}
