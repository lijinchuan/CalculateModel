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
            string stkcode = "000002.SZ";
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

            ATrade.Data.StockQuote[] stockquotes = ATrade.Server.StockServer.GetHisDayQuote(stkcode).Take(1200).ToArray();
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
LC:REF(CLOSE,1);
RSI1:SMA(MAX(CLOSE-LC,0),6,1)/SMA(ABS(CLOSE-LC),6,1)*100;
RSI2:SMA(MAX(CLOSE-LC,0),12,1)/SMA(ABS(CLOSE-LC),12,1)*100;
RSI3:SMA(MAX(CLOSE-LC,0),24,1)/SMA(ABS(CLOSE-LC),24,1)*100;
AC4:REF(AC,5)>REF(AC,4) and REF(AC,4)<REF(AC,3) and REF(AC,3)<REF(AC,2) AND REF(AC,2)<REF(AC,1) AND REF(AC,1)<AC and REF(AC,4)<0 AND (REF(RSI1,1)<=20 OR RSI1<=20);
B0:IF AC4 or (H>HF AND UP>TEETH AND TEETH>DOWN) OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1) and RSI1<20) THEN TRUE ELSE FALSE END;
S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
IF Profit<-5 THEN SELL('sl') ELSE IF B0 AND NOT S0 THEN BUY('buy') ELSE IF NOT B0 AND S0 THEN SELL('sell') END END END;";

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
LC:REF(CLOSE,1);
RSI1:SMA(MAX(CLOSE-LC,0),6,1)/SMA(ABS(CLOSE-LC),6,1)*100;
RSI2:SMA(MAX(CLOSE-LC,0),12,1)/SMA(ABS(CLOSE-LC),12,1)*100;
RSI3:SMA(MAX(CLOSE-LC,0),24,1)/SMA(ABS(CLOSE-LC),24,1)*100;
AC4:REF(AC,5)>REF(AC,4) and REF(AC,4)<REF(AC,3) and REF(AC,3)<REF(AC,2) AND REF(AC,2)<REF(AC,1) AND REF(AC,1)<AC and REF(AC,4)<0 AND (REF(RSI1,1)<=20 OR RSI1<=20);
B0:IF AC4 or (H>HF AND UP>TEETH AND TEETH>DOWN) OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1)) THEN TRUE ELSE FALSE END;
S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
FOR x:0 TO len(B0)-1 begin IF arrayof(B0,x) AND NOT arrayof(S0,x) THEN BUY('buy') ELSE IF NOT arrayof(B0,x) AND arrayof(S0,x) THEN SELL('sell') END END END;";

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
LC:REF(CLOSE,1);
RSI1:SMA(MAX(CLOSE-LC,0),6,1)/SMA(ABS(CLOSE-LC),6,1)*100;
RSI2:SMA(MAX(CLOSE-LC,0),12,1)/SMA(ABS(CLOSE-LC),12,1)*100;
RSI3:SMA(MAX(CLOSE-LC,0),24,1)/SMA(ABS(CLOSE-LC),24,1)*100;
AC4:REF(AC,5)>REF(AC,4) and REF(AC,4)<REF(AC,3) and REF(AC,3)<REF(AC,2) AND REF(AC,2)<REF(AC,1) AND REF(AC,1)<AC and REF(AC,4)<0 AND (REF(RSI1,1)<=20 OR RSI1<=20);
B0:IF AC4 or (H>HF AND UP>TEETH AND TEETH>DOWN) OR ((AO<=0 AND REF(AO,1)<REF(AO,2) AND MFI<REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1) and RSI1<20) THEN TRUE ELSE FALSE END;
S0:IF (REF(LF,1)>0 AND LF=0) OR ((REF(UP,1)<UP AND AO>0 AND REF(AO,1)>REF(AO,2) AND (MFI<=REF(MFI,1)*0.9 AND V>=REF(V,1)*1.1))) THEN TRUE ELSE F END;
IF Profit<-5 OR (NOT B0 AND S0) THEN SELL('sell') ELSE IF B0 AND NOT S0 THEN BUY('buy') END END;";
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

                var result= express.CallResult(pool);
                var ttt = pool.GetTestResult();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"买入次数:{ttt.BuyTimes},卖出次数:{ttt.SellTimes},盈利次数:{ttt.EarnTimes},亏损次数:{ttt.LostTimes},最大盈利:{ttt.MaxEarnRate},最小亏损:{ttt.MaxLostRate},TradeRate:{ttt.TradeRate}");
                sb.AppendLine("==================================================================");
                sb.AppendLine($"买入日期\t卖出日期\t买入价格\t买入数量\t卖出价格\t卖出数量\t比例");
                foreach (var trade in ttt.Trades.Where(p => p.BuyQuantity > 100).OrderByDescending(p => p.BuyTime))
                {
                    sb.AppendLine($"{trade.BuyTime.ToString("yyyy-MM-dd")}\t{trade.SellTime.ToString("yyyy-MM-dd")}\t{trade.BuyPrice}\t{trade.BuyQuantity}\t{trade.SellPrice}\t{trade.SellQuantity}\t{Math.Round(trade.Rate * 100, 2)}%");
                }

                Console.WriteLine(sb);
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
