﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XA_DATASETLib;

namespace OM.PP.XingApp.Api
{
    public class BaseTradeApi
    {
        protected string resName = string.Empty;
        protected string inBlock = string.Empty;
        protected string outBlock = string.Empty;
        protected string outBlock1 = string.Empty;
        protected string outBlock2 = string.Empty;

        protected XAQueryClass query;

        public BaseTradeApi()
        {      
        }
        public BaseTradeApi(string resName)
        {
            this.resName = resName;
            inBlock = resName + "InBlock";
            outBlock = resName + "OutBlock";
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
        protected virtual void query_ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
        }
        protected virtual void query_ReceiveData(string szTrCode)
        {
        }
    }
}
