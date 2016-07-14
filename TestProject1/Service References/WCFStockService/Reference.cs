﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProject1.WCFStockService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCFStockService.IStockService")]
    public interface IStockService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetHisDayQuoteSpan", ReplyAction="http://tempuri.org/IStockService/GetHisDayQuoteSpanResponse")]
        ATrade.Server.StockWCFService.StockQuoteContract[] GetHisDayQuoteSpan(string innerCode, System.DateTime start, System.DateTime end);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetHisDayQuote", ReplyAction="http://tempuri.org/IStockService/GetHisDayQuoteResponse")]
        ATrade.Server.StockWCFService.StockQuoteContract[] GetHisDayQuote(string innerCode, bool lastest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockInfo", ReplyAction="http://tempuri.org/IStockService/GetStockInfoResponse")]
        ATrade.Server.StockWCFService.StockInfoDataContract GetStockInfo(string innerCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetStockInfoBySecurityCodeA", ReplyAction="http://tempuri.org/IStockService/GetStockInfoBySecurityCodeAResponse")]
        ATrade.Server.StockWCFService.StockInfoDataContract[] GetStockInfoBySecurityCodeA(string securityCodeA);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetAllStockInfo", ReplyAction="http://tempuri.org/IStockService/GetAllStockInfoResponse")]
        ATrade.Server.StockWCFService.StockInfoDataContract[] GetAllStockInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetSimpleStockInfoList", ReplyAction="http://tempuri.org/IStockService/GetSimpleStockInfoListResponse")]
        ATrade.Server.StockWCFService.StockSimpleInfoDataContract[] GetSimpleStockInfoList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetRealQuote", ReplyAction="http://tempuri.org/IStockService/GetRealQuoteResponse")]
        ATrade.Server.StockWCFService.StockQuoteContract GetRealQuote(string innerCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetFundInfo", ReplyAction="http://tempuri.org/IStockService/GetFundInfoResponse")]
        ATrade.Server.StockWCFService.FundInfoDataContract GetFundInfo(string fundCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/NextTradeDate", ReplyAction="http://tempuri.org/IStockService/NextTradeDateResponse")]
        System.DateTime NextTradeDate(System.DateTime now);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/NextOpenTime", ReplyAction="http://tempuri.org/IStockService/NextOpenTimeResponse")]
        System.DateTime NextOpenTime(System.DateTime now);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/IsOpen", ReplyAction="http://tempuri.org/IStockService/IsOpenResponse")]
        bool IsOpen(System.DateTime dt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetCurrMonthTradeDay", ReplyAction="http://tempuri.org/IStockService/GetCurrMonthTradeDayResponse")]
        System.DateTime[] GetCurrMonthTradeDay(int year, int mon);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/SetTradeDate", ReplyAction="http://tempuri.org/IStockService/SetTradeDateResponse")]
        void SetTradeDate(System.DateTime dt, bool canTrade);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStockService/GetIndexRealQuote", ReplyAction="http://tempuri.org/IStockService/GetIndexRealQuoteResponse")]
        ATrade.Server.StockWCFService.StockQuoteContract[] GetIndexRealQuote(string[] indexCodes);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStockServiceChannel : TestProject1.WCFStockService.IStockService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockServiceClient : System.ServiceModel.ClientBase<TestProject1.WCFStockService.IStockService>, TestProject1.WCFStockService.IStockService {
        
        public StockServiceClient() {
        }
        
        public StockServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ATrade.Server.StockWCFService.StockQuoteContract[] GetHisDayQuoteSpan(string innerCode, System.DateTime start, System.DateTime end) {
            return base.Channel.GetHisDayQuoteSpan(innerCode, start, end);
        }
        
        public ATrade.Server.StockWCFService.StockQuoteContract[] GetHisDayQuote(string innerCode, bool lastest) {
            return base.Channel.GetHisDayQuote(innerCode, lastest);
        }
        
        public ATrade.Server.StockWCFService.StockInfoDataContract GetStockInfo(string innerCode) {
            return base.Channel.GetStockInfo(innerCode);
        }
        
        public ATrade.Server.StockWCFService.StockInfoDataContract[] GetStockInfoBySecurityCodeA(string securityCodeA) {
            return base.Channel.GetStockInfoBySecurityCodeA(securityCodeA);
        }
        
        public ATrade.Server.StockWCFService.StockInfoDataContract[] GetAllStockInfo() {
            return base.Channel.GetAllStockInfo();
        }
        
        public ATrade.Server.StockWCFService.StockSimpleInfoDataContract[] GetSimpleStockInfoList() {
            return base.Channel.GetSimpleStockInfoList();
        }
        
        public ATrade.Server.StockWCFService.StockQuoteContract GetRealQuote(string innerCode) {
            return base.Channel.GetRealQuote(innerCode);
        }
        
        public ATrade.Server.StockWCFService.FundInfoDataContract GetFundInfo(string fundCode) {
            return base.Channel.GetFundInfo(fundCode);
        }
        
        public System.DateTime NextTradeDate(System.DateTime now) {
            return base.Channel.NextTradeDate(now);
        }
        
        public System.DateTime NextOpenTime(System.DateTime now) {
            return base.Channel.NextOpenTime(now);
        }
        
        public bool IsOpen(System.DateTime dt) {
            return base.Channel.IsOpen(dt);
        }
        
        public System.DateTime[] GetCurrMonthTradeDay(int year, int mon) {
            return base.Channel.GetCurrMonthTradeDay(year, mon);
        }
        
        public void SetTradeDate(System.DateTime dt, bool canTrade) {
            base.Channel.SetTradeDate(dt, canTrade);
        }
        
        public ATrade.Server.StockWCFService.StockQuoteContract[] GetIndexRealQuote(string[] indexCodes) {
            return base.Channel.GetIndexRealQuote(indexCodes);
        }
    }
}
