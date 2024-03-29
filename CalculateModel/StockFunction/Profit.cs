﻿using System;
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
                    var cmds = (CurrStockDataCalPool.BusiRequest as ATrade.TradeBusiness.TestBusiness).TestStockCmdCach
                    .Last(p => p.CmdType == CmdType.buy && p.EffDate <= CurrQuote.Time).ToList();

                    //if (CurrQuote.Time > DateTime.Parse("2020/03/12") && CurrQuote.Time <= DateTime.Parse("2020/03/31"))
                    //{

                    //}

                    if (!cmds.Any())
                    {
                        return new CalResult
                        {
                            Result = 0d,
                            ResultType = typeof(double)
                        };
                    }

                    var costPrice = 0d;
                    if (cmds.Count > 1)
                    {
                        var total = cmds.Sum(p => p.Quantity) * 1.0;
                        costPrice = cmds.Sum(p => p.Price * (p.Quantity / total));
                    }
                    else
                    {
                        costPrice = cmds.First().Price;
                    }

                    //if (CurrQuote.Time > new DateTime(2018, 6, 26)&& CurrQuote.Time < new DateTime(2018, 7, 18))
                    //{

                    //}

                    return new CalResult
                    {
                        ResultType=typeof(double),
                        Result=(CurrQuote.Close- costPrice)*100/costPrice
                    };
                    
                }

                object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
                    (object)DelayCalResult.Delay
                    ).ToArray();

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
                    double profitRate = GetNowProfit();

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
                        double profitRate = GetNowProfit();
                        return new CalResult
                        {
                            ResultType=typeof(double),
                            Result=profitRate
                        };
                    }
                }
            }

            double GetNowProfit()
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

                return profitRate;
            }
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }
    }
}
