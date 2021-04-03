using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    internal class REF : StockFun, IIteratorCollectionFun
    {
        public override int Params
        {
            get
            {
                return 2;
            }
        }


        protected override CalResult CollectOperate()
        {
            return SingOperate();
        }

        protected override CalResult SingOperate()
        {
            try
            {
                if (valueCach == null)
                {
                    Console.WriteLine("ref 计算");
                    int _ref = int.Parse(param2.ToString());
                    object[] data = (object[])param1;

                    object[] result = new object[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i - _ref >=0)
                            result[i] = data[i - _ref];
                        else
                        {

                            if (data[0].GetType() == typeof(double)
                                || data[0].GetType() == typeof(double[]))
                                result[i] = 0d;
                            else if (data[0].GetType() == typeof(bool)
                                || data[0].GetType() == typeof(bool[]))
                                result[i] = false;
                            else
                                result[i] = 0d;
                        }
                    }

                    this.valueCach = result;
                }

                if (CalCurrent.CurrentIndex == -1)
                {
                    return new CalResult
                    {
                        Results = valueCach,
                        ResultType = valueCach.GetType()
                    };
                }

                return new CalResult
                {
                    Result = valueCach[CalCurrent.CurrentIndex],
                    ResultType = valueCach[0].GetType()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public REF(CalCurrent pool)
            :base(pool)
        {

        }
    }
}
