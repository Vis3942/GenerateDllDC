using System;
using System.Collections.Generic;
using UtilsLibrary;

namespace Seimens_QSM2100
{
    public class ConvertData
    {
        public static List<ResponseData> Get_PM2_5_Data(byte[] msg_bytes, int start_idx)
        {
            if (msg_bytes == null) { return null; }
            if (msg_bytes.Length < (start_idx + 5) ) { return null; }
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            List<ResponseData> ret_listrd = new List<ResponseData>();
            /* computation of value next line */
            int res = msg_bytes[idx + 3] * 256 + msg_bytes[idx + 4];
            ResponseData rd = new ResponseData();
            rd.Name = "pm2";
            rd.Value = res.ToString();
            rd.Timestamp = dt_str;
            ret_listrd.Add(rd);
            return ret_listrd;
        }
    }
}
