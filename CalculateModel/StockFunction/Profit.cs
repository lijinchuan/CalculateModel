using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Data;
using LJC.FrameWork.Data;
using LJC.FrameWork.CodeExpression;

namespace ATrade.CalculateModel
{
    internal class Profit:StockFun
    {
        public Profit(CalCurrent pool)
            : base(pool)
        {

        }
        static int totalcount = 0;
        protected override CalResult CollectOperate()
        {
            if (CurrStockDataCalPool.IsTestMode)
            {
                if (CalCurrent.CurrentIndex > -1)
                {
                    var cmd = (CurrStockDataCalPool.BusiRequest as ATrade.TradeBusiness.TestBusiness).TestStockCmdCach
                    .Last();

                    if (cmd == null || cmd.CmdType == CmdType.sell || cmd.EffDate > CurrQuote.Time)
                    {
                        return new CalResult
                        {
                            Result = 0d,
                            ResultType = typeof(double)
                        };
                    }

                    return new CalResult
                    {
                        ResultType=typeof(double),
                        Result=(CurrQuote.Close- cmd.Price)*100/cmd.Price
                    };
                    
                }

                object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
                    (object)0d
                    ).ToList().ToArray();

                return new CalResult
                {
                    Results = results,
                    ResultType = typeof(double)
                };
            }
            else
            {
                if (CalCurrent.CurrentIndex == -1)
                {
                    double profitRate = 0;
                    if (this.CurrStockDataCalPool.BusiRequest != null)
                    {
                        var hold = this.CurrStockDataCalPool.BusiRequest.QueryHolds()
                            .FirstOrDefault(p => p.StockCode.Equals(this.CurrStockDataCalPool.Stock.StockCode));
                        if (hold != null)
                        {
                            profitRate = hold.BuyProfitLost * 100 / hold.BuyCost;
                        }
                    }

                    object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
                    {
                        return (object)0d;
                    }).ToArray();

                    if (profitRate > -100 && profitRate <= -10)
                    {
                        var realquote = ATrade.Server.StockServer.GetRealQuote(this.CurrStockDataCalPool.Stock.StockCode);
                        if (realquote.ChangeRate < 10)
                            results[0] = (object)profitRate;
                    }

                    return new CalResult
                    {
                        Results = results,
                        ResultType = typeof(double)
                    };
                }
                else
                {
                    if (CalCurrent.CurrentIndex > 0)
                    {
                        return new CalResult
                        {
                            Result = 0d,
                            ResultType = typeof(double)
                        };
                    }
                    else
                    {
                        double profitRate = 0;
                        if (this.CurrStockDataCalPool.BusiRequest != null)
                        {
                            var hold = this.CurrStockDataCalPool.BusiRequest.QueryHolds()
                                .FirstOrDefault(p => p.StockCode.Equals(this.CurrStockDataCalPool.Stock.StockCode));
                            if (hold != null)
                            {
                                profitRate = hold.BuyProfitLost * 100 / hold.BuyCost;
                            }
                        }
                        return new CalResult
                        {
                            ResultType=typeof(double),
                            Result=profitRate
                        };
                    }
                }
            }
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }
    }
}
