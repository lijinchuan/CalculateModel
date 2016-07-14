using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATrade.CalculateModel
{
    public static class UseBook
    {
        public static List<string> BaseCalSign
        {
            get;
            private set;
        }
        public static List<string> Syntax
        {
            get;
            private set;
        }
        public static List<string> StockFunction
        {
            get;
            private set;
        }

        public static List<string> TestFunction
        {
            get;
            private set;
        }

        static void AddBaseCalSign(string calSign, string useMethod)
        {
            BaseCalSign.Add(string.Format("{0}:{1}",calSign,useMethod));
        }

        static void AddSyntax(string syntax, string useMethod)
        {
            Syntax.Add(string.Format("{0}:{1}",syntax,"条件表达式"));
        }

        static void AddStockFunction(string syntax, string useMehtod,string paras,string example)
        {
            StockFunction.Add(string.Format("{0}:{1};参数:{2};示例：{3}",syntax,useMehtod,paras,example));
        }

        static void AddTestFunction(string syntax, string useMethod, string paras, string example)
        {
            TestFunction.Add(string.Format("{0}:{1};参数:{2};示例：{3}",syntax,useMethod,paras,example));
        }

        static UseBook()
        {
            BaseCalSign = new List<string>();
            AddBaseCalSign("+", "两数相加");
            AddBaseCalSign("-", "两数相减");
            AddBaseCalSign("*", "两数相乘");
            AddBaseCalSign("/", "两数相除");
            AddBaseCalSign("mod", "求余数");
            AddBaseCalSign(">", "比较");
            AddBaseCalSign(">=", "比较");
            AddBaseCalSign("<", "比较");
            AddBaseCalSign("<=", "比较");
            AddBaseCalSign("=","比较是否相等");
            AddBaseCalSign(":", "赋值");
            AddBaseCalSign("and", "并操作");
            AddBaseCalSign("or", "或操作");
            AddBaseCalSign("not", "否定操作");
            AddBaseCalSign("T|True", "是");
            AddBaseCalSign("F|False", "否");

            Syntax = new List<string>();
            AddSyntax("if ... then ... [else ...] end", "条件表达式语法");

            StockFunction = new List<string>();
            AddStockFunction("max", "返回最大值","","max(h),max(c,o)");
            AddStockFunction("min", "返回最小的值", "", "min(h),min(c,o)");
            AddStockFunction("sum", "求和", "", "sum(o)");
            AddStockFunction("o", "返回开盘价", "", "o");
            AddStockFunction("h", "返回最高价", "", "h");
            AddStockFunction("c", "返回收盘价", "", "c");
            AddStockFunction("l", "返回最低价", "", "l");
            AddStockFunction("a", "返回成交金额", "", "a");
            AddStockFunction("v", "返回成交量", "", "v");
            AddStockFunction("ema", "移动平均","","");

            AddStockFunction("open", "返回开盘价", "", "open");
            AddStockFunction("high", "返回最高价", "", "high");
            AddStockFunction("close", "返回收盘价", "", "close");
            AddStockFunction("low", "返回最低价", "", "low");
            AddStockFunction("amount", "返回成交金额", "", "amount");
            AddStockFunction("vol", "返回成交量", "", "vol");

            AddStockFunction("ma", "简单移动平均", "", "ma(c,5)");
            AddStockFunction("sma", "加权移动平均", "", "sma(c,5,2)");
            AddStockFunction("hhv", "最高价的最高价", "", "");
            AddStockFunction("llv", "最高价的最高价", "", "");
            AddStockFunction("REF", "取前面的数据", "", "REF(c,1)");
            AddStockFunction("SAR", "抛物线指标", "", "SAR(4,2,2,20)");

            AddStockFunction("HF", "高价分形", "", "hf");
            AddStockFunction("LF", "低价分形", "", "lf");

            AddStockFunction("ISHOLD", "是否持仓","","ISHOLD");
            AddStockFunction("Profit|Loss|PL", "盈亏(%)", "", "Profit|Loss|PL");

            AddStockFunction("buy", "买入股票", "", "buy");
            AddStockFunction("sell", "卖出股票", "", "sell");

            TestFunction = new List<string>();
            AddTestFunction("TestBoolArray", "返回一个测试的数组", "", "TestBoolArray");

        }
    }
}
