﻿using OM.Lib.Base.Enums;
using OM.PP.Chakra.Ctx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XA_DATASETLib;

namespace OM.PP.XingApp.Api
{
    /// <summary>
    /// 코스피 200 지수
    /// </summary>
    class Api_WorldFutureTrade : BaseTradeApi
    {
        public string ItemCode
        {
            get;
            set;
        }
        public Api_WorldFutureTrade()
        {   
            this.resName = "CIDBT00100";
            inBlock = resName + "InBlock1";            
            outBlock1 = resName + "OutBlock1";
            outBlock2 = resName + "OutBlock2";

            if (query == null)
            {
                query = new XAQueryClass();
                query.ReceiveData += query_ReceiveData;
                query.ReceiveMessage += query_ReceiveMessage;
                query.LoadFromResFile($".\\res\\{resName}.res");
            }
        }

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecCnt"></param>
        /// <param name="OrdDt">YYYYMMDD</param>       
        /// <param name="AcntNo"></param>
        /// <param name="Pwd"></param>
        /// <param name="IsuCodeVal">종목코드값</param>
        /// <param name="FutsOrdTpCode">1:신규</param>
        /// <param name="BnsTpCode">1:매도 2:매수</param>
        /// <param name="AbrdFutsOrdPtnCode">1:시장가 2:지정가</param>      
        /// <param name="OvrsDrvtOrdPrc">주문가격</param>
        /// <param name="CndiOrdPrc">조건주문가격</param>
        /// <param name="OrdQty"></param>        
      

        public void Query(      
              string RecCnt = ""
            , string OrdDt = ""            
            , string AcntNo = ""
            , string Pwd = ""
            , string IsuCodeVal = ""
            , string FutsOrdTpCode = ""
            , string BnsTpCode = ""
            , string AbrdFutsOrdPtnCode = ""
            , string OvrsDrvtOrdPrc = ""
            , string CndiOrdPrc = ""            
            , string OrdQty = ""            
            )
        {
            ItemCode = IsuCodeVal;            
            query.SetFieldData(inBlock, "RecCnt", 0, RecCnt.Length == 0 ? "1" : RecCnt);
            query.SetFieldData(inBlock, "OrdDt", 0, OrdDt);
            query.SetFieldData(inBlock, "BrnCode", 0, String.Empty);
            query.SetFieldData(inBlock, "AcntNo", 0, AcntNo);
            query.SetFieldData(inBlock, "Pwd", 0, Pwd);
            query.SetFieldData(inBlock, "IsuCodeVal", 0, IsuCodeVal);
            query.SetFieldData(inBlock, "FutsOrdTpCode", 0, FutsOrdTpCode);
            query.SetFieldData(inBlock, "BnsTpCode", 0, BnsTpCode);
            query.SetFieldData(inBlock, "AbrdFutsOrdPtnCode", 0, AbrdFutsOrdPtnCode);
            query.SetFieldData(inBlock, "CrcyCode", 0, String.Empty);
            query.SetFieldData(inBlock, "OvrsDrvtOrdPrc", 0, OvrsDrvtOrdPrc.Length==0 ? "0" : OvrsDrvtOrdPrc);
            query.SetFieldData(inBlock, "CndiOrdPrc", 0, CndiOrdPrc.Length == 0 ? "0" : CndiOrdPrc);
            query.SetFieldData(inBlock, "OrdQty", 0, OrdQty);
            query.SetFieldData(inBlock, "PrdtCode", 0, String.Empty);
            query.SetFieldData(inBlock, "DueYymm", 0, String.Empty);
            query.SetFieldData(inBlock, "ExchCode", 0, String.Empty);

            query.Request(false);
        }

        protected override void query_ReceiveData(string szTrCode)
        {            
            string OrdDt = query.GetFieldData(outBlock1, "OrdDt", 0);
            string IsuCodeVal = query.GetFieldData(outBlock1, "IsuCodeVal", 0);
            string FutsOrdTpCode = query.GetFieldData(outBlock1, "FutsOrdTpCode", 0);
            string BnsTpCode = query.GetFieldData(outBlock1, "BnsTpCode", 0);
            string OvrsDrvtOrdPrc = query.GetFieldData(outBlock1, "OvrsDrvtOrdPrc", 0);
            string OrdQty = query.GetFieldData(outBlock1, "OrdQty", 0);

            //OnApiLog("Api_WorldFutureTrade ::: query_ReceiveData");
        }
        #endregion

    }
}
