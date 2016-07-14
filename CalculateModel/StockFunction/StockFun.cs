using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Data;
using ATrade.Quote;
using LJC.FrameWork.CodeExpression;

namespace ATrade.CalculateModel
{
    internal class StockFun:CustomFun,ICollectionResultFun
    {
        private StockQuote[] _stockQuotes;
        protected StockQuote[] StockQuotes
        {
            get
            {
                if (_stockQuotes != null)
                {
                    return _stockQuotes;
                }

                _stockQuotes = this.CurrStockDataCalPool.Quotes;
                return _stockQuotes;
            }
        }

        /// <summary>
        /// 保存缓存结果
        /// </summary>
        protected object[] valueCach;

        protected StockQuote CurrQuote
        {
            get
            {
                if (StockQuotes.Length > this.CalCurrent.CurrentIndex)
                    return StockQuotes[this.CalCurrent.CurrentIndex];

                return null;
            }
        }


        public StockFun(CalCurrent pool)
            :base(pool)
        {
            CurrStockDataCalPool = (pool.RuntimeParam as StockDataCalPool);
        }

        public StockDataCalPool CurrStockDataCalPool 
        { 
            get; 
            set; 
        }
    }
}
