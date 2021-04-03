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

            object[] result=new object[data.Length];

            var maxindex = -1;
            var nextmaxindex = -1;
            for (var i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    maxindex = i;
                    result[i]= data[i];
                }
                else
                {
                    if (i - maxindex < count)
                    {
                        if ((double)data[i] >= (double)data[maxindex])
                        {
                            maxindex = i;
                            result[i] = data[i];
                            nextmaxindex = i + 1;
                        }
                        else
                        {
                            result[i] = data[maxindex];
                            if (nextmaxindex == -1)
                            {
                                nextmaxindex = i;
                            }
                            else
                            {
                                if ((double)data[i] >= (double)data[nextmaxindex])
                                {
                                    nextmaxindex = i;
                                }
                            }
                        }
                    }
                    else
                    {
                        maxindex = nextmaxindex;
                        if ((double)data[i] >= (double)data[maxindex])
                        {
                            maxindex = i;
                            nextmaxindex = i + 1;
                        }
                        else
                        {
                            if ((double)data[i] > (double)data[nextmaxindex])
                                nextmaxindex = i;
                            else
                                nextmaxindex++;
                        }
                        result[i] = data[maxindex];
                    }
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
