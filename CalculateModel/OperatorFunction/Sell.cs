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
    internal class Sell:StockFun
    {
        /// <summary>
        /// 卖股票
        /// </summary>
        /// <param name="pool"></param>
        public Sell(CalCurrent pool)
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
            //cmd.CmdReason = this.CurrStockDataCalPool.CalExpressCodeDesc;
            cmd.CmdType = CmdType.sell;
            cmd.CreateTime = DateTime.Now;
            cmd.SubmitTime = new DateTime(1900, 1, 1);

            if (!string.IsNullOrWhiteSpace((string)param1))
                cmd.CmdReason = this.param1.ToString();

            if (!this.CurrStockDataCalPool.IsTestMode)
            {
                cmd.EffDate = TradeCalanderServer.NextTradeDate(this.CurrQuote.Time);
                if (cmd.EffDate == TradeCalanderServer.NextOpenTime(DateTime.Now).Date)
                    LocalDB.AddStockCmd(cmd);
                //result.Result = true;
            }
            else
            {
                //if (CurrQuote.Time > new DateTime(2018, 6, 26) && CurrQuote.Time < new DateTime(2018, 7, 18))
                //{

                //}
                if (StockQuotes.Length > CalCurrent.CurrentIndex + 1)
                {
                    cmd.EffDate = StockQuotes[CalCurrent.CurrentIndex + 1].Time;
                }
                if (cmd.EffDate == default)
                {
                    cmd.EffDate = CurrQuote.Time.Date.AddDays(1);
                }
                if (CalCurrent.CurrentIndex > 60)
                {
                    (CurrStockDataCalPool.BusiRequest as TestBusiness).SetTradeTime(CurrQuote.Time).Order(StockOrderSide.sell, 0, CurrStockDataCalPool.Stock, 0, CurrQuote.Close, false);
                }
            }

            CalResult result = new CalResult
            {
                ResultType = typeof(OperatorResult),
                Result = new OperatorResult
                {
                    OperatorType = "S",
                    Time=CurrQuote.Time,
                    //因为是延迟一个交易日
                    Price=(double)CurrQuote.Close
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
