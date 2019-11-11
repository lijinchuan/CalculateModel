using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.CalculateModel;
using LJC.FrameWork.CodeExpression;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
          
            string stkcode = "600031.sh";
            string quotefile = LJC.FrameWork.Comm.CommFun.GetRuningPath() + stkcode + ".xml";
            //var stock = client.GetStockInfo(stkcode);
            

            var stockinfo = new ATrade.Data.Stock
            {
                Spell = "syzg",
                StockCode = "600031",
                StockName = "三一重工",
                Exchange="sse",
                Market=ATrade.Data.StockExchange.XSHE,
            };

            ATrade.Data.StockQuote[] stockquotes = null;
            if (File.Exists(quotefile))
            {
                stockquotes = LJC.FrameWork.Comm.SerializerHelper.DeSerializerFile<ATrade.Data.StockQuote[]>(quotefile);
            }
            else
            {
                ATrade.Data.StockQuote[] quotes = null;
                stockquotes = new ATrade.Data.StockQuote[quotes.Length];
                for (int i = 0; i < stockquotes.Length; i++)
                {
                    stockquotes[i] = new ATrade.Data.StockQuote
                    {
                        Amount = quotes[i].Amount,
                        ChangePrice = quotes[i].ChangePrice,
                        ChangeRate = quotes[i].ChangeRate,
                        Close = quotes[i].Close,
                        High = quotes[i].High,
                        Low = quotes[i].Low,
                        Open = quotes[i].Open,
                        PreClose = quotes[i].PreClose,
                        Time = quotes[i].Time,
                        Volumne = quotes[i].Volumne,
                    };
                }
                LJC.FrameWork.Comm.SerializerHelper.SerializerToXML<ATrade.Data.StockQuote[]>(stockquotes, quotefile);
            }

            ATrade.TradeBusiness.TestBusiness tb = new ATrade.TradeBusiness.TestBusiness();
            StockDataCalPool pool = new StockDataCalPool(tb, stockinfo, stockquotes);

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Restart();

            string code = @"DIF:EMA(CLOSE,12)-EMA(CLOSE,26);
                            DEA:EMA(DIF,9);
                            MACD:(DIF-DEA)*2;
                            VAR1:(H+L)/2;
                            UP:REF(SMA(VAR1,5,1),3);
                            TEETH:REF(SMA(VAR1,8,1),5);
                            DOWN:REF(SMA(VAR1,13,1),8);
                            MFI:(H-L)*10000000/V;
                            MA1:MA((HIGH+LOW)/2,5);
                            MA2:MA((HIGH+LOW)/2,21);
                            AO:MA1-MA2;
                            AC:AO-MA(AO,5);
                            B0:IF H>HF OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1)) THEN TRUE ELSE FALSE END;
                            S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
                            IF B0 AND NOT S0 THEN BUY('buy') ELSE IF NOT B0 AND S0 THEN SELL('sell') END END;";

//            string code = @"N:13;
//N1:8;
//WR1:100*(HHV(HIGH,N)-CLOSE)/(HHV(HIGH,N)-LLV(LOW,N));
//WR2:100*(HHV(HIGH,N1)-CLOSE)/(HHV(HIGH,N1)-LLV(LOW,N1));";

            ExpressCode express = new ExpressCode(code);
            express.CalCurrent.RuntimeParam = pool;
            pool.IsTestMode = true;
            express.CalCurrent.CurrentBound = stockquotes.Length;
            //express.AnalyseExpress();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            express.CallResult(pool);
            var ttt= pool.GetTestResult();

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();
            for (int i = 0; i < 10; i++)
            {
                pool = new StockDataCalPool(tb, stockinfo, stockquotes);
                express.CalCurrent.RuntimeParam = pool;
                pool.IsTestMode = true;
                express.CalCurrent.CurrentBound = stockquotes.Length;
                express.CallResult(pool);
                ttt=pool.GetTestResult();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            Console.Read();
        }
    }
}
