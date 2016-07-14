using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 最低价的高最低值
    /// </summary>
    internal class LLV : StockFun, IIteratorCollectionFun
    {

        public LLV(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return base.SingOperate();
        }


        protected override CalResult CollectOperate()
        {
            object[] data = (object[])param1;
            int count = int.Parse(param2.ToString());
            if (count < 1)
                return null;

            decimal llow = decimal.MaxValue;
            object[] result=new object[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                
                if ((decimal)data[i] < llow)
                {
                    llow = (decimal)data[i];
                }

                if (i>=count)
                {
                    int setIndex=i-count;
                    result[setIndex]=llow;
                    if (llow == (decimal)data[setIndex])
                    {
                        llow = decimal.MaxValue;
                        for (int j = setIndex + 1; j <= i; j++)
                        {
                            if ((decimal)data[j] < llow)
                                llow = (decimal)data[j];
                        }
                    }
                }
            }

            llow = decimal.MaxValue;
            for (int i = result.Length-1; i >= result.Length-count; i--)
            {
                if (llow > (decimal)data[i])
                    llow = (decimal)data[i];
                result[i] = llow;
            }

            return new CalResult
            {
                Results=result,
                ResultType=typeof(object[])
            };
        }

        public override int Params
        {
            get
            {
                return 2;
            }
        }

    }
}
