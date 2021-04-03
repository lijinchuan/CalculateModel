using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Quote;
using ATrade.Data;
using ATrade.TradeBusiness;
using LJC.FrameWork.Data;
using LJC.FrameWork.CodeExpression;

namespace ATrade.CalculateModel
{
    public class StockDataCalPool
    {
        static StockDataCalPool()
        {
            CalSignFactory.Register("a", typeof(A));
            CalSignFactory.Register("abs", typeof(Abs));
            CalSignFactory.Register("close", typeof(C));
            CalSignFactory.Register("c", typeof(C));
            CalSignFactory.Register("ema", typeof(EMA));
            CalSignFactory.Register("hf", typeof(HF));
            CalSignFactory.Register("hhv", typeof(HHV));
            CalSignFactory.Register("high", typeof(High));
            CalSignFactory.Register("h", typeof(High));
            CalSignFactory.Register("ishold", typeof(ISHOLD));
            CalSignFactory.Register("lf", typeof(LF));
            CalSignFactory.Register("llv", typeof(LLV));
            CalSignFactory.Register("low", typeof(Low));
            CalSignFactory.Register("l", typeof(Low));
            CalSignFactory.Register("ma", typeof(MA));
            CalSignFactory.Register("max", typeof(Max));
            CalSignFactory.Register("min", typeof(Min));
            CalSignFactory.Register("o", typeof(O));
            CalSignFactory.Register("open", typeof(O));
            CalSignFactory.Register("profit", typeof(Profit));
            CalSignFactory.Register("lost", typeof(Profit));
            CalSignFactory.Register("ref", typeof(REF));
            CalSignFactory.Register("sar", typeof(SAR));
            CalSignFactory.Register("sma", typeof(SMA));
            CalSignFactory.Register("sum", typeof(Sum));
            CalSignFactory.Register("today", typeof(Today));
            CalSignFactory.Register("v", typeof(V));
            CalSignFactory.Register("vol", typeof(V));
            CalSignFactory.Register("buy", typeof(Buy));
            CalSignFactory.Register("sell", typeof(Sell));
        }

        private bool _isTestMode = false;
        /// <summary>
        /// 是否是测试模式
        /// </summary>
        public bool IsTestMode
        {
            get
            {
                return _isTestMode;
            }
            set
            {
                _isTestMode = value;
                if (value)
                {
                    //ResetFund();
                }
            }
        }

        public Stock Stock
        {
            get;
            set;
        }

        public StockQuote[] Quotes
        {
            private set;
            get;
        }

        public IBusiness BusiRequest
        {
            get;
            set;
        }


        public StockDataCalPool(IBusiness busiRequest,Stock stock, StockQuote[] quoteData, bool isTestMode = false)
        {
           this.BusiRequest = busiRequest;
            Stock = stock;
            Quotes = quoteData;
            if (Quotes.Length > 1 && Quotes.Last().Time < Quotes.First().Time)
            {
                Quotes = Quotes.Reverse().ToArray();
            }

            IsTestMode = isTestMode;
            
        }

        private void ResetFund()
        {
            //MoneyAccount_Test MAT = new MoneyAccount_Test();
            //MAT.FundBalance = 10000;
            //MAT.Amount = 10000;
            //MAT.Fund = 0;
            //MAT.FundBuyFrozen = 0;
            //MAT.MarketValue = 0;
            //MAT.StockValue = 0;
            //MAT.ID = 1;
            //MAT.StockCurrency = "CNY";
            
            ////new DataContextMoudle<MoneyAccount_Test>((MoneyAccount_Test)MAT).Update();
            //TestBusiness.TestAccountCach.Update(MAT);

            //TestBusiness.TestStockCmdCach.Clear();

            //TestBusiness.TestStockPostionsCach.Clear();
        }

        public StockTradeStatistics GetTestResult()
        {
            StockTradeStatistics result = new StockTradeStatistics();

            if (!IsTestMode)
                throw new Exception("IsTestMode模式关闭时无法进行测试！");

            //TestBusiness testBusi = new TestBusiness();
            //testBusi.ResetFund(this.Stock.StockCode);
            //this.CallResult();

            var list = (BusiRequest as ATrade.TradeBusiness.TestBusiness).TestStockCmdCach.ExecuteList();
            foreach (var test in list)
            {
                if (test.CmdType == CmdType.buy)
                {
                    StockTradeDetail trade = new StockTradeDetail();
                    trade.BuyPrice = test.Price;
                    trade.BuyTime = test.EffDate;
                    trade.BuyQuantity = test.Quantity;
                    trade.Cost = test.Cost;
                    trade.CostPrice = test.CostPrice;
                    trade.StockCode = test.StockCode;
                    result.Trades.Add(trade);
                }

                if (test.CmdType == CmdType.sell)
                {
                    int x = result.Trades.Count() - 1;
                    int quantity = test.Quantity;
                    while (quantity > 0)
                    {
                        StockTradeDetail trade = result.Trades[x];
                        if (trade.BuyQuantity > quantity)
                        {
                            throw new Exception("买入的股票不能比卖出的股票数据多");
                        }
                        trade.SellPrice = test.Price;
                        trade.SellQuantity = trade.BuyQuantity;
                        quantity -= trade.BuyQuantity;
                        trade.SellTime = test.EffDate;
                        trade.SellPrice = test.Price;
                        trade.Cost += test.Cost;
                        trade.Rate=((trade.SellPrice-trade.BuyPrice)*trade.BuyQuantity-trade.Cost)/(trade.BuyPrice*trade.BuyQuantity);
                        x--;
                        result.TradeTimes++;

                        if (trade.Rate > 0)
                        {
                            result.EarnTimes++;
                            if (trade.Rate > result.MaxEarnRate)
                                result.MaxEarnRate = trade.Rate;
                        }
                        else
                        {
                            result.LostTimes++;
                            if (trade.Rate < result.MaxLostRate)
                                result.MaxLostRate = trade.Rate;
                        }
                    }
                }
            }

            return result;
        }
    }
}
