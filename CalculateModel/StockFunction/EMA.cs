using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 指数移动平均
    /// </summary>
    internal class EMA : StockFun, IIteratorCollectionFun
    {

        public EMA(CalCurrent pool)
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
            double count = (double)param2;
            
            if (count < 1)
                return null;

            object[] result=new object[data.Length];

            for(var i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    result[i] = data[i];
                }
                else
                {
                    result[i] = ((double)data[i] * 2 + (double)result[i - 1] * (count - 1)) / (count + 1);
                }
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
