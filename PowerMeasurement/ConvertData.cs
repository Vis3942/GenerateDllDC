using System;
using System.Collections.Generic;
using System.Globalization;
using UtilsLibrary;


namespace PowerMeasurement
{
    public class ConvertData
    {
        public static List<ResponseData> GetPowerData(byte[] msg_bytes, int start_index, int length)
        {
            if (length < 22) // 5 floats => 4*5 bytes + 2  uint8's => 2 bytes
            {
                return null;
            }

            List<ResponseData> ret_listrd = new List<ResponseData>();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            float res = BitConverter.ToSingle(msg_bytes, start_index);
            ResponseData rd = new ResponseData();
            rd.Name = "Voltage";
            rd.Value = res.ToString("F", CultureInfo.InvariantCulture);
            rd.Timestamp = dt_str;
            rd.EventId = "";
            ret_listrd.Add(rd);

            float res2 = BitConverter.ToSingle(msg_bytes, start_index+4);
            ResponseData rd2 = new ResponseData();
            rd2.Name = "Current";
            rd2.Value = res2.ToString("F", CultureInfo.InvariantCulture);
            rd2.Timestamp = dt_str;
            rd2.EventId = "";
            ret_listrd.Add(rd2);

            float res3 = BitConverter.ToSingle(msg_bytes, start_index+8);
            ResponseData rd3 = new ResponseData();
            rd3.Name = "PowerFactor";
            rd3.Value = res3.ToString("F", CultureInfo.InvariantCulture);
            rd3.Timestamp = dt_str;
            rd3.EventId = "";
            ret_listrd.Add(rd3);

            float res4 = BitConverter.ToSingle(msg_bytes, start_index+12);
            ResponseData rd4 = new ResponseData();
            rd4.Name = "LineFrequency";
            rd4.Value = res4.ToString("F", CultureInfo.InvariantCulture);
            rd4.Timestamp = dt_str;
            rd4.EventId = "";
            ret_listrd.Add(rd4);

            float res5 = BitConverter.ToSingle(msg_bytes, start_index + 16);
            ResponseData rd5 = new ResponseData();
            rd5.Name = "ActivePower";
            rd5.Value = res5.ToString("F", CultureInfo.InvariantCulture);
            rd5.Timestamp = dt_str;
            rd5.EventId = "";
            ret_listrd.Add(rd5);

            byte bval = msg_bytes[start_index + 20];
            ResponseData rd6 = new ResponseData();
            rd6.Name = "OnOff";
            rd6.Value = bval.ToString("D", CultureInfo.InvariantCulture);
            rd6.Timestamp = dt_str;
            rd6.EventId = "";
            ret_listrd.Add(rd6);

            byte bval2 = msg_bytes[start_index + 20];
            ResponseData rd7 = new ResponseData();
            rd7.Name = "Brightness";
            rd7.Value = bval2.ToString("D", CultureInfo.InvariantCulture);
            rd7.Timestamp = dt_str;
            rd7.EventId = "";
            ret_listrd.Add(rd7);


            if (ret_listrd.Count > 0)
            {
                return ret_listrd;
            }
            else
            {
                return null;
            }
        }
    }
}
