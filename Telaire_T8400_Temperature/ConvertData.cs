using System;
using System.Collections.Generic;
using System.Globalization;
using UtilsLibrary;

namespace Telaire_T8400_Temperature
{
    public class ConvertData
    {
        // this function only processes data in message after the channel number. 
        // The code calling this function should identify the start_index (position after the channel number) 
        //      and the length of data in this message
        public static List<ResponseData> GetTemperature(byte[] msg_bytes, int start_index, int length)
        {
            if (length < 4)
            {
                return null;
            }
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            List<ResponseData> ret_listrd = new List<ResponseData>();
            /* computation of value next line */
            float res = BitConverter.ToSingle(msg_bytes, start_index);
            res = 9.0f * res + 32.0f; // Temperature(°F) = (9.0*volts)+32.0
            ResponseData rd = new ResponseData();
            rd.Name = "air_temperature";
            rd.Value = res.ToString("F", CultureInfo.InvariantCulture);
            rd.Timestamp = dt_str;
            rd.EventId = "";
            ret_listrd.Add(rd);
            return ret_listrd;
        }
    }
}
