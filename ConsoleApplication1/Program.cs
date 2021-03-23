using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.CalculateModel;
using LJC.FrameWork.CodeExpression;
using System.IO;
using ATrade.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void MATest()
        {
            ATrade.Data.StockQuote[] stockquotes = ATrade.Server.StockServer.GetHisDayQuote("000001.IX").Take(100).ToArray();
            var data = stockquotes.Select(p => p.High).ToArray();
            double[] result = new double[data.Length];
            var count = 20;
            for (int i = 0; i < data.Length; i++)
            {
                if (i < data.Length - count)
                    result[i] = data.Skip(i).Take(count).Sum(c => c) / count;
                else
                    result[i] = 0d;
            }

            var result2 = new double[data.Length];
            var sum = 0d;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                if (i > data.Length - count)
                {
                    result2[i] = 0d;
                    sum += data[i];
                }
                else
                {
                    sum += data[i];
                    result2[i] = sum / count;
                    sum -= data[i + count - 1];
                }
            }

        }

        static void Main(string[] args)
        {
            string stkcode = "000001.IX";
            //string quotefile = LJC.FrameWork.Comm.CommFun.GetRuningPath() + stkcode + ".xml";
            //var stock = client.GetStockInfo(stkcode);

            //var stockinfo = new ATrade.Data.Stock
            //{
            //    Spell = "njxb",
            //    StockCode = "600682",
            //    StockName = "南京新百",
            //    Exchange="sse",
            //    Market=ATrade.Data.StockExchange.XSHE,
            //};

            ATrade.Data.StockQuote[] stockquotes = ATrade.Server.StockServer.GetHisDayQuote(stkcode).ToArray();
            var stockinfo = ATrade.Server.StockServer.GetStock(stkcode);

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
B0:IF (H>HF AND UP>TEETH AND TEETH>DOWN) OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1)) THEN TRUE ELSE FALSE END;
S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
IF Profit<-10 AND Profit>-30 THEN SELL('sl') ELSE IF B0 AND NOT S0 THEN BUY('buy') ELSE IF NOT B0 AND S0 THEN SELL('sell') END END END;";

            code = @"DIF:EMA(CLOSE,12)-EMA(CLOSE,26);
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
B0:IF (H>HF AND UP>TEETH AND TEETH>DOWN) OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1)) THEN TRUE ELSE FALSE END;
S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
IF Profit<-10 AND Profit>-30 THEN SELL('sl') ELSE IF B0 AND NOT S0 THEN BUY('buy') ELSE IF NOT B0 AND S0 THEN SELL('sell') END END END;";

            //            string code = @"N:13;
            //N1:8;
            //WR1:100*(HHV(HIGH,N)-CLOSE)/(HHV(HIGH,N)-LLV(LOW,N));
            //WR2:100*(HHV(HIGH,N1)-CLOSE)/(HHV(HIGH,N1)-LLV(LOW,N1));";

            for (var i = 0; i < 1; i++)
            {

                ExpressCode express = new ExpressCode(code);
                express.CalCurrent.RuntimeParam = pool;
                pool.IsTestMode = true;
                express.CalCurrent.CurrentBound = stockquotes.Length;
                //express.AnalyseExpress();

                //Console.WriteLine(sw.ElapsedMilliseconds);

                express.CallResult(pool);
                var ttt = pool.GetTestResult();
            }
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
            Calc.Print();
            //sw.Restart();
            //for (int i = 0; i < 10; i++)
            //{
            //    pool = new StockDataCalPool(tb, stockinfo, stockquotes);
            //    express.CalCurrent.RuntimeParam = pool;
            //    pool.IsTestMode = true;
            //    express.CalCurrent.CurrentBound = stockquotes.Length;
            //    express.CallResult(pool);
            //    ttt=pool.GetTestResult();
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            Console.Read();
        }
    }
}
