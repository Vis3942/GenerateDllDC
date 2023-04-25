using System;
using System.Collections.Generic;
using System.Globalization;
using UtilsLibrary;

namespace DissolvedOxygenSensorCompany1
{
    public class ConvertData
    {
        public static List<ResponseData> Get_Dissovled_Oxygen_Data(byte[] msg_bytes, int start_index, int length)
        {
            int value_i;
            int decimal_i;
            double tmp_d;
            if (msg_bytes == null) { return null; }

            if ((msg_bytes[start_index + 2] + 3 + 2) != length) { return null; }
            List<ResponseData> to_ret = new List<ResponseData>();

            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + "+00:00";
            ResponseData cond_rd = new ResponseData();
            cond_rd.Name = "dis_oxy";
            value_i = msg_bytes[start_index + 4] + (msg_bytes[start_index + 3] << 8);
            decimal_i = msg_bytes[start_index + 6] + (msg_bytes[start_index + 5] << 8);
            tmp_d = (double)(value_i / Math.Pow(10, decimal_i));
            cond_rd.Value = tmp_d.ToString("F3", CultureInfo.CreateSpecificCulture("en-US"));
            cond_rd.Timestamp = dt_str;
            to_ret.Add(cond_rd);
            ResponseData temp_rd = new ResponseData();
            temp_rd.Name = "temperature_do";
            value_i = msg_bytes[start_index + 8] + (msg_bytes[start_index + 7] << 8);
            decimal_i = msg_bytes[start_index + 10] + (msg_bytes[start_index + 9] << 8);
            tmp_d = (double)(value_i / Math.Pow(10, decimal_i));
            temp_rd.Value = tmp_d.ToString("F3", CultureInfo.CreateSpecificCulture("en-US"));
            temp_rd.Timestamp = dt_str;
            to_ret.Add(temp_rd);
            return to_ret;
        }
    }
}
