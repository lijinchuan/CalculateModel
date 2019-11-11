using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 最高价的高最高值
    /// </summary>
    internal class HHV : StockFun, IIteratorCollectionFun
    {

        public HHV(CalCurrent pool)
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

            double hhight = 0;
            object[] result=new object[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                
                if ((double)data[i] > hhight)
                {
                    hhight = (double)data[i];
                }

                if (i>=count)
                {
                    int setIndex=i-count;
                    result[setIndex]=hhight;
                    if (hhight == (double)data[setIndex])
                    {
                        hhight = 0;
                        for (int j = setIndex + 1; j <= i; j++)
                        {
                            if ((double)data[j] > hhight)
                                hhight = (double)data[j];
                        }
                    }
                }
            }

            hhight = 0;
            for (int i = result.Length-1; i >= result.Length-count; i--)
            {
                if (hhight < (double)data[i])
                    hhight = (double)data[i];
                result[i] = hhight;
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
