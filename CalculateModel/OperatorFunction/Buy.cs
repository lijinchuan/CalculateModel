using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Data;
using ATrade.Server;
using ATrade.TradeBusiness;
using LJC.FrameWork.Data.QuickDataBase;
using LJC.FrameWork.CodeExpression;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 买股票
    /// </summary>
    internal class Buy:StockFun
    {
        public Buy(CalCurrent pool)
            : base(pool)
        {

        }

        protected override CalResult CollectOperate()
        {
            return SingOperate();
        }

        protected override CalResult SingOperate()
        {
            StockCmd cmd = new StockCmd();
            cmd.StockCode = this.CurrStockDataCalPool.Stock.StockCode;
            cmd.StockName = this.CurrStockDataCalPool.Stock.StockName;
            cmd.CmdReason = string.Empty;
            cmd.CmdType = CmdType.buy;
            cmd.CreateTime = DateTime.Now;
            cmd.SubmitTime = new DateTime(1900, 1, 1);
            //cmd.EffDate =TradeCalanderServer.NextTradeDate(this.CurrStockDataCalPool.Quotes[this.CurrStockDataCalPool.CurrIndex].Time).AddDays(1);

            if (!string.IsNullOrWhiteSpace((string)param1))
                cmd.CmdReason = param1.ToString();

            if (!this.CurrStockDataCalPool.IsTestMode)
            {
                cmd.EffDate = TradeCalanderServer.NextTradeDate(this.CurrQuote.Time);
                if (cmd.EffDate == TradeCalanderServer.NextOpenTime(DateTime.Now).Date)
                    LocalDB.AddStockCmd(cmd);
                
            }
            else
            {
                cmd.EffDate = this.StockQuotes.FirstOrDefault(p => p.Time > this.CurrQuote.Time)?.Time??default(DateTime);
                if (cmd.EffDate == default(DateTime))
                {
                    cmd.EffDate = this.CurrQuote.Time.Date.AddDays(1);
                }
                if (CalCurrent.CurrentIndex > 60)
                {
                    (CurrStockDataCalPool.BusiRequest as TestBusiness).SetTradeTime(CurrQuote.Time).Order(StockOrderSide.buy, 0, CurrStockDataCalPool.Stock, 0, CurrQuote.Close, false);
                }
            }

            CalResult result = new CalResult
            {
                ResultType = typeof(OperatorResult),
                Result = new OperatorResult
                {
                    OperatorType = "B",
                    Time = this.CurrQuote.Time,
                    //因为是延迟一个交易日
                    Price = this.CurrQuote.Close
                }
            };

            return result;
        }

        public override int Params
        {
            get
            {
                return 0;
            }
        }
    }
}
