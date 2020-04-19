using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATrade.CalculateModel
{
    public static class Calc
    {
        public static int SELL_NextTradeDate = 0;
        public static long SELL_NextTradeDate_MS = 0;
        public static int BUY_NextTradeDate = 0;
        public static long BUY_NextTradeDate_MS = 0;

        public static void Print()
        {
            Console.WriteLine("SELL_NextTradeDate:" + SELL_NextTradeDate);
            Console.WriteLine("SELL_NextTradeDate_MS:" + SELL_NextTradeDate_MS);
            Console.WriteLine("BUY_NextTradeDate:" + BUY_NextTradeDate);
            Console.WriteLine("BUY_NextTradeDate_MS:" + BUY_NextTradeDate_MS);
        }
    }
}
