using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Data;
using ATrade.Quote;
using LJC.FrameWork.CodeExpression;
using LJC.FrameWork.Comm;
using LJC.FrameWork.SOA;

namespace ATrade.CalculateModel
{
    internal class StockFun : CustomFun, ICollectionResultFun
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

        private StockQuote[] _weekStockQuotes;
        protected StockQuote[] WeekStockQuotes
        {
            get
            {
                if (_weekStockQuotes != null)
                {
                    return _weekStockQuotes;
                }

                _weekStockQuotes = this.CurrStockDataCalPool.Quotes.Select(p => new
                {
                    weekfirst = DateTimeHelper.GetWeekFirstDate(p.Time),
                    quote = p
                }).GroupBy(p => p.weekfirst).Select(p => new StockQuote
                {
                    Close = p.First().quote.Close,
                    High = p.Max(q => q.quote.High),
                    Amount = p.Sum(q => q.quote.Amount),
                    Low = p.Min(q => q.quote.Low),
                    Open = p.Last().quote.Open,
                    Time = p.First().quote.Time,
                    Volumne = p.Sum(q => q.quote.Volumne)
                }).ToArray();
                return _weekStockQuotes;
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
            : base(pool)
        {
            CurrStockDataCalPool = (pool.RuntimeParam as StockDataCalPool);
        }

        public StockDataCalPool CurrStockDataCalPool
        {
            get;
            set;
        }

        public List<LJC.Com.StockService.Contract.SCRResult> GetSCR()
        {
            var key = "___scr___";
            if (!CalCurrent.VarDataPool.ContainsKey(key))
            {
                var list = ESBClient.DoSOARequest2<LJC.Com.StockService.Contract.GetSCRResponse>(LJC.Com.StockService.Contract.Consts.ServiceNo,
                    LJC.Com.StockService.Contract.Consts.FunID_GetSCR, new LJC.Com.StockService.Contract.GetSCRRequest
                    {
                        InnerCode = CurrStockDataCalPool.Stock.StockCode,
                        Begin = this.StockQuotes.First().Time,
                        End = this.StockQuotes.Last().Time
                    }).SCRResults;

                CalCurrent.VarDataPool.Add(key, new CalResult
                {
                    Result = list
                });
            }
            var scrlist = this.CalCurrent.VarDataPool[key].Result as List<LJC.Com.StockService.Contract.SCRResult>;
            return scrlist;
        }
    }
}
