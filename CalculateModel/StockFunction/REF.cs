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
                    int _ref = int.Parse(param2.ToString());
                    object[] data = (object[])param1;

                    object[] result = new object[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i + _ref < data.Length)
                            result[i] = data[i + _ref];
                        else
                        {

                            if (result[0].GetType() == typeof(decimal)
                                || result[0].GetType() == typeof(decimal[]))
                                result[i] = 0M;
                            else if (result[0].GetType() == typeof(bool)
                                || result[0].GetType() == typeof(bool[]))
                                result[i] = false;
                            else
                                result[i] = 0M;
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
