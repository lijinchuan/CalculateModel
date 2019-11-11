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
    internal class SMA : StockFun, IIteratorCollectionFun
    {

        public SMA(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult SingOperate()
        {
            return base.SingOperate();
        }


        protected override CalResult CollectOperate()
        {
            try
            {
                object[] data = (object[])param1;
                int count = int.Parse(param2.ToString());
                if (count < 1)
                    return null;
                int day = int.Parse(param3.ToString());
                if (count <= day)
                {
                    throw new ExpressErrorException("参数错误，SMA第3个参数不能大于第2个参数！");
                }

                double[] result = new double[data.Length];
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    if (i == data.Length - 1)
                        result[i] = (double)data[i];
                    else
                    {
                        result[i] = (((double)data[i] * day + result[i + 1] * (count - day)) / count);
                    }
                }

                if (CalCurrent.CurrentIndex > -1)
                {
                    return new CalResult
                    {
                        Result = result[CalCurrent.CurrentIndex],
                        //ResultType = typeof(decimal)
                    };
                }

                return new CalResult
                {
                    Results = result.Select(p=>(object)p).ToArray(),
                    //ResultType = typeof(decimal[])
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override int Params
        {
            get
            {
                return 3;
            }
        }

    }
}
