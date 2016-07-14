using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 移动平均
    /// </summary>
    internal class MA : StockFun, IIteratorCollectionFun
    {

        public MA(CalCurrent pool)
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

            object[] result=new object[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < data.Length - count)
                    result[i] = data.Skip(i).Take(count).Sum(c => (decimal)c) / count;
                else
                    result[i] = 0M;
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
