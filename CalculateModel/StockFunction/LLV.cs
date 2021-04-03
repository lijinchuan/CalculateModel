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

            object[] result = new object[data.Length];
            var minindex = -1;
            var nextminindex = -1;
            for (var i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    minindex = i;
                    result[i] = data[i];
                }
                else
                {
                    if (i - minindex < count)
                    {
                        if ((double)data[i] <= (double)data[minindex])
                        {
                            minindex = i;
                            result[i] = data[i];
                            nextminindex = i + 1;
                        }
                        else
                        {
                            result[i] = data[minindex];
                            if (nextminindex == -1)
                            {
                                nextminindex = i;
                            }
                            else
                            {
                                if ((double)data[i] <= (double)data[nextminindex])
                                {
                                    nextminindex = i;
                                }
                            }
                        }
                    }
                    else
                    {
                        minindex = nextminindex;
                        if ((double)data[i] <= (double)data[minindex])
                        {
                            minindex = i;
                            nextminindex = i + 1;
                        }
                        else
                        {
                            if ((double)data[i] < (double)data[nextminindex])
                                nextminindex = i;
                            else
                                nextminindex++;
                        }

                        result[i] = data[minindex];
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
