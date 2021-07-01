using System;
using System.Collections.Generic;
using UtilsLibrary;

namespace ResponseOneWeatherTransmitter
{
    public class ConvertData
    {
        public static int Area { get; set; }
        public static List<ResponseData> Get_Weather_Data(byte[] msg_bytes)
        {
            if (msg_bytes == null) { return null; }
            if (msg_bytes.Length < 14) { return null; }
            if (msg_bytes[6] != 0) { return null; }
            //int resp_data_len = BitConverter.ToUInt16(msg_bytes, 0) - 7; // 2 length, 2 id, 1 (config/command), 1 address,  1 status;
            //string cmd_resp_str = System.Text.Encoding.ASCII.GetString(msg_bytes, 7, resp_data_len);
            List<ResponseData> to_ret = new List<ResponseData>();
            int resp_data_len = BitConverter.ToUInt16(msg_bytes, 0) - 7; // 2 length, 2 id, 1 (config/command), 1 address,  1 status;
            string cmd_resp_str = System.Text.Encoding.ASCII.GetString(msg_bytes, 7, resp_data_len);
            // cmd_resp_str format "a www.ww ddd.d ttt.t hhh.h bbbb.b ppppp ss*cc<CR>"
            //   www.ww wind_speed, ddd.d wind_direction ttt.t temperature hhh.h relative_humidity bbbb.b pressure
            //Console.WriteLine(cmd_resp_str);
            string[] resp_str_arr = cmd_resp_str.Trim().Split(' ');
            if (resp_str_arr.Length < 7) { return null; } // "ppppp is optoinal"
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + "+00:00";
            ResponseData wds_rd = new ResponseData();
            wds_rd.Name = "wds";
            wds_rd.Value = resp_str_arr[1];
            wds_rd.Timestamp = dt_str;
            to_ret.Add(wds_rd);
            ResponseData wdd_rd = new ResponseData();
            wdd_rd.Name = "wdd";
            wdd_rd.Value = resp_str_arr[2];
            wdd_rd.Timestamp = dt_str;
            to_ret.Add(wdd_rd);
            ResponseData tmp_rd = new ResponseData();
            tmp_rd.Name = "tmp";
            tmp_rd.Value = resp_str_arr[3];
            tmp_rd.Timestamp = dt_str;
            to_ret.Add(tmp_rd);
            ResponseData rlh_rd = new ResponseData();
            rlh_rd.Name = "rlh";
            rlh_rd.Value = resp_str_arr[4];
            rlh_rd.Timestamp = dt_str;
            to_ret.Add(rlh_rd);
            ResponseData prs_rd = new ResponseData();
            prs_rd.Name = "prs";
            prs_rd.Value = resp_str_arr[5];
            prs_rd.Timestamp = dt_str;
            to_ret.Add(prs_rd);
            return to_ret;
        }

        public static List<ResponseData> Config(byte[] msg_bytes)
        {
            if (msg_bytes == null) { return null; }
            if (msg_bytes.Length < 14) { return null; }
            if (msg_bytes[6] != 0) { return null; }
            //int resp_data_len = BitConverter.ToUInt16(msg_bytes, 0) - 7; // 2 length, 2 id, 1 (config/command), 1 address,  1 status;
            //string cmd_resp_str = System.Text.Encoding.ASCII.GetString(msg_bytes, 7, resp_data_len);
            List<ResponseData> to_ret = new List<ResponseData>();
            var rd_sts = new ResponseData();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + "+00:00";
            rd_sts.Name = "Status";
            rd_sts.Timestamp = dt_str;
            if (msg_bytes[6] == 0)
            {
                rd_sts.Value = "Success";
            }
            else
            {
                rd_sts.Value = $"Error {msg_bytes[6]}";
            }
            return to_ret;
        }

    }
}
