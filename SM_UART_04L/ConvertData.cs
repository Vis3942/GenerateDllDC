using System;
using System.Collections.Generic;
using UtilsLibrary;

namespace SM_UART_04L
{
    public class ConvertData
    {
        public static List<ResponseData> Get_PM2_5_Data(byte[] msg_bytes)
        {
            if (msg_bytes == null) { return null; }
            if (msg_bytes.Length < 14) { return null; }
            if (msg_bytes[6] != 0) { return null; }
            int idx = Array.IndexOf(msg_bytes, (byte)0x1c, 6);
            if (idx == -1)
            {
                return null;
            }
            else
            {
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
}
