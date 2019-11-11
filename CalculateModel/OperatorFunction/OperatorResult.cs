using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    public class OperatorResult
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
        /// <summary>
        /// 操作价格
        /// </summary>
        public double Price
        {
            get;
            set;
        }
        /// <summary>
        /// B or S
        /// </summary>
        public string OperatorType
        {
            get;
            set;
        }
    }
}
