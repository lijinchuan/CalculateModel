using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATrade.Data;
using LJC.FrameWork.Data.QuickDataBase;
using LJC.FrameWork.CodeExpression;

namespace ATrade.CalculateModel
{
    /// <summary>
    /// 是否持仓
    /// </summary>
    internal class ISHOLD:StockFun
    {
        public ISHOLD(CalCurrent _poll)
            : base(_poll)
        {

        }

        protected override CalResult CollectOperate()
        {
            throw new NotImplementedException();
            //if (CurrStockDataCalPool.IsTestMode)
            //{
            //    List<StockCmd_Test> SCTS = DataContextMoudelFactory<StockCmd_Test>.GetDataContext()
            //       .WhereEq("StockCode", CurrStockDataCalPool.Stock.StockCode)
            //       .ExecuteList().OrderBy(s => s.SubmitTime).ToList();

            //    object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
            //    {
            //        StockCmd_Test SCT = SCTS.Where(c => c.SubmitTime < q.Time).OrderByDescending(c => c.SubmitTime).FirstOrDefault();

            //        if (SCT == null || SCT.CmdType == CmdType.sell)
            //            return (object)false;

            //        return (object)true;
            //    }).ToArray();

            //    return new CalResult
            //    {
            //        Results = results,
            //        ResultType = typeof(bool)
            //    };
            //}
            //else
            //{
            //    List<StockPostions> list = DataContextMoudelFactory<StockPostions>.GetDataContext().ExecuteList();
            //    StockPostions sp = list.Find(l => l.StockCode == this.CurrStockDataCalPool.Stock.StockCode);

            //    object[] results = this.CurrStockDataCalPool.Quotes.Select(q =>
            //    {
            //        if (sp == null)
            //            return (object)false;

            //        if (q.Time.Date >= sp.LastUpdateTime.Date)
            //            return (object)true;

            //        return (object)false;
            //    }).ToArray();

            //    return new CalResult
            //    {
            //        Results = results,
            //        ResultType = typeof(bool)
            //    };
            //}
        }

        protected override CalResult SingOperate()
        {
            return CollectOperate();
        }
    }
}
