﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XA_DATASETLib;

namespace OM.PP.XingApp.Api
{
    public delegate void LogDelegate(string log);
   
    public class BaseApi
    {
        protected string resName = string.Empty;
        protected string inBlock = string.Empty;
        protected string outBlock = string.Empty;
        protected string outBlock1 = string.Empty;
        protected string outBlock2 = string.Empty;

        protected XAQueryClass query;

        public event LogDelegate ApiLogHandler;

        protected AutoResetEvent waitHandle = new AutoResetEvent(true);

        public BaseApi()
        {      
        }
        public BaseApi(string resName)
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

        protected void OnApiLog(string log)
        {
            ApiLogHandler(log);
        }
       
        public virtual void List()
        { }
        public virtual void Insert()
        { }
        public virtual void Update()
        { }
        public virtual void Delete()
        { }
    }
}
