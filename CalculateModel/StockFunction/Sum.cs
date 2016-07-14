using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class Sum:StockFun,IIteratorCollectionFun
    {
        public Sum(CalCurrent pool)
            : base(pool)
        {
        }

        public override int Params
        {
            get
            {
                return 1;
            }
        }


        protected override CalResult CollectOperate()
        {
            object[] results=null;
            if (param2 == null)
            {
                object[] p1 = (object[])param1;
                decimal sumTotal = 0M;
                results = new object[p1.Length];
                for (int i = p1.Length - 1; i >= 0; i--)
                {
                    sumTotal += p1[i].ToDecimal();
                    results[i] = sumTotal;
                }
            }
            else
            {
                if (param1 is object[])
                {
                    object[] p1 = (object[])param1;
                    results = new object[p1.Length];
                    if (param2 is object[])
                    {
                        object[] p2 = (object[])param2;
                        for (int i = 0; i < p1.Length; i++)
                        {
                            results[i] = p1[i].ToDecimal() + p2[i].ToDecimal();
                        }
                    }
                    else
                    {
                        decimal p2 = ((object)param2).ToDecimal();
                        for (int i = 0; i < p1.Length; i++)
                        {
                            results[i] = p1[i].ToDecimal() + p2;
                        }
                    }
                }
                else
                {
                    decimal p1 = ((object)param1).ToDecimal();
                    object[] p2 = (object[])param2;
                    results=new object[p2.Length];
                    for (int i = 0; i < p2.Length; i++)
                    {
                        results[i] = p1.ToDecimal() + p2[i].ToDecimal();
                    }
                }
            }

            return new CalResult
            {
                ResultType=typeof(decimal),
                Results=results
            };
        }

        protected override CalResult SingOperate()
        {
            decimal total = 0M;
            for (int i = 0; i < int.MaxValue; i++)
            {
                object o = GetPara(i);
                if (o == null)
                    break;
                total += o.ToDecimal();
            }

            return new CalResult
            {
                Result = total.ToString(),
                ResultType = typeof(decimal)
            };
        }
    }
}
