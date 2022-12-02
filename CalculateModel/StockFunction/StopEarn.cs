using ATrade.Data;
using LJC.FrameWork.CodeExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel.StockFunction
{
    /// <summary>
    /// 止损
    /// </summary>
    internal class StopEarn : StockFun
    {
        public StopEarn(CalCurrent pool)
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
                    var stopearnrate = 0d;
                    if (cmds.Count > 1)
                    {
                        var total = cmds.Sum(p => p.Quantity) * 1.0;
                        costPrice = cmds.Sum(p => p.Price * (p.Quantity / total));
                    }
                    else
                    {
                        costPrice = cmds.First().Price;
                    }

                    if (CurrQuote.Close > costPrice)
                    {
                        var quotes = CurrStockDataCalPool.Quotes.Where(p => p.Time > cmds.Min(q => q.EffDate) && p.Time < CurrQuote.Time);
                        if (quotes.Any())
                        {
                            var maxClose = quotes.Max(p => p.Close);
                            if (maxClose > costPrice)
                            {
                                stopearnrate = (maxClose - CurrQuote.Close) * 100 / maxClose;
                            }

                            //Console.WriteLine($"{CurrQuote.Time.ToString("yyyy/MM/dd")} stopearn:"+stopearnrate);
                        }
                    }

                    return new CalResult
                    {
                        ResultType = typeof(double),
                        Result = stopearnrate
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
                    object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
                    {
                        return (object)0d;
                    }).ToArray();

                    double stopearnrate = GetStopEarn();
                    results[0] = stopearnrate;

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
                        return new CalResult
                        {
                            ResultType = typeof(double),
                            Result = GetStopEarn()
                        };
                    }
                }

            }

            double GetStopEarn()
            {
                double stopearnrate = 0;
                if (CurrStockDataCalPool.BusiRequest != null)
                {
                    var hold = CurrStockDataCalPool.BusiRequest.QueryHolds().Find(p => p.StockCode == CurrStockDataCalPool.Stock.StockCode);
                    if (hold != null)
                    {
                        var realquote = Server.StockServer.GetRealQuote(this.CurrStockDataCalPool.Stock.StockCode);
                        if (realquote.Close > hold.PositionCost)
                        {
                            var quotes = CurrStockDataCalPool.Quotes.Where(p => p.Time > hold.LastUpdateTime);
                            if (quotes.Any())
                            {
                                var maxClose = quotes.Max(p => p.Close);
                                if (maxClose > hold.PositionCost)
                                {
                                    stopearnrate = (maxClose - CurrQuote.Close) * 100 / maxClose;
                                }

                                //Console.WriteLine($"{CurrQuote.Time.ToString("yyyy/MM/dd")} stopearn:"+stopearnrate);
                            }
                        }
                    }
                }
                return stopearnrate;
            }
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }
    }
}
