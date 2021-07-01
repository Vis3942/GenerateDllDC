using System;
using System.Collections.Generic;
using System.Globalization;
using UtilsLibrary;

namespace DS7505TemperatureSensor
{
    public class ConvertData
    {
        // this function only processes data in message after the channel number. 
        // The code calling this function should identify the start_index (position after the channel number) 
        //      and the length of data in this message
        public static List<ResponseData> GetBoardTemperature(byte[] msg_bytes, int start_index, int length)
        {
            if (length < 2)
            {
                return null;
            }
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            List<ResponseData> ret_listrd = new List<ResponseData>();
            /* computation of value next line */
            short res_int16 = BitConverter.ToInt16(msg_bytes, start_index);
            float res_degc = 0.0039f * res_int16;
            float res = (res_degc * 1.8f) + 32.0f;
            ResponseData rd = new ResponseData();
            rd.Name = "amb_temp_f";
            rd.Value = res.ToString("F", CultureInfo.InvariantCulture);
            rd.Timestamp = dt_str;
            rd.EventId = "";
            ret_listrd.Add(rd);
            return ret_listrd;
        }
    }
}
