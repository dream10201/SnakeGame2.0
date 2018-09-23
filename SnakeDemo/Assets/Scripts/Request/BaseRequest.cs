﻿/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/21 21:24:54
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    public abstract class BaseRequest
    {
        public abstract void SendRequest(RequestCode ReqCode, object data);

        public abstract void Response(Message msg);
    }
}