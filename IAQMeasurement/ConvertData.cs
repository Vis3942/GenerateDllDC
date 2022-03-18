using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UtilsLibrary;

namespace IAQMeasurement
{
    public class ConvertData
    {
        public static List<ResponseData> GetIAQData(byte[] msg_bytes, int start_index, int length)
        {
            if (length < 57) // 13 float values: 4 bytes each + 2 uint16 values + 1 uint8 value
            {
                return null;
            }

            List<ResponseData> ret_listrd = new List<ResponseData>();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";

            float res2 = BitConverter.ToSingle(msg_bytes, start_index );
            ResponseData rd2 = new ResponseData();
            rd2.Name = "pm1_0";
            rd2.Value = res2.ToString("F", CultureInfo.InvariantCulture);
            rd2.Timestamp = dt_str;
            rd2.EventId = "";
            ret_listrd.Add(rd2);

            float res3 = BitConverter.ToSingle(msg_bytes, start_index + 4);
            ResponseData rd3 = new ResponseData();
            rd3.Name = "pm2_5";
            rd3.Value = res3.ToString("F", CultureInfo.InvariantCulture);
            rd3.Timestamp = dt_str;
            rd3.EventId = "";
            ret_listrd.Add(rd3);

            float res4 = BitConverter.ToSingle(msg_bytes, start_index + 8);
            ResponseData rd4 = new ResponseData();
            rd4.Name = "pm4_0";
            rd4.Value = res4.ToString("F", CultureInfo.InvariantCulture);
            rd4.Timestamp = dt_str;
            rd4.EventId = "";
            ret_listrd.Add(rd4);

            float res5 = BitConverter.ToSingle(msg_bytes, start_index + 12);
            ResponseData rd5 = new ResponseData();
            rd5.Name = "pm10_0";
            rd5.Value = res5.ToString("F", CultureInfo.InvariantCulture);
            rd5.Timestamp = dt_str;
            rd5.EventId = "";
            ret_listrd.Add(rd5);

            float res6 = BitConverter.ToSingle(msg_bytes, start_index + 16);
            ResponseData rd6 = new ResponseData();
            rd6.Name = "pmn0_5";
            rd6.Value = res6.ToString("F", CultureInfo.InvariantCulture);
            rd6.Timestamp = dt_str;
            rd6.EventId = "";
            ret_listrd.Add(rd6);

            float res7 = BitConverter.ToSingle(msg_bytes, start_index + 20);
            ResponseData rd7 = new ResponseData();
            rd7.Name = "pmn1_0";
            rd7.Value = res7.ToString("F", CultureInfo.InvariantCulture);
            rd7.Timestamp = dt_str;
            rd7.EventId = "";
            ret_listrd.Add(rd7);

            float res8 = BitConverter.ToSingle(msg_bytes, start_index + 24);
            ResponseData rd8 = new ResponseData();
            rd8.Name = "pmn2_5";
            rd8.Value = res8.ToString("F", CultureInfo.InvariantCulture);
            rd8.Timestamp = dt_str;
            rd8.EventId = "";
            ret_listrd.Add(rd8);

            float res9 = BitConverter.ToSingle(msg_bytes, start_index + 28);
            ResponseData rd9 = new ResponseData();
            rd9.Name = "pmn4_0";
            rd9.Value = res9.ToString("F", CultureInfo.InvariantCulture);
            rd9.Timestamp = dt_str;
            rd9.EventId = "";
            ret_listrd.Add(rd9);

            float res10 = BitConverter.ToSingle(msg_bytes, start_index + 32);
            ResponseData rd10 = new ResponseData();
            rd10.Name = "pmn10_0";
            rd10.Value = res10.ToString("F", CultureInfo.InvariantCulture);
            rd10.Timestamp = dt_str;
            rd10.EventId = "";
            ret_listrd.Add(rd10);

            float res11 = BitConverter.ToSingle(msg_bytes, start_index + 36);
            ResponseData rd11 = new ResponseData();
            rd11.Name = "avg_pm_size";
            rd11.Value = res11.ToString("F", CultureInfo.InvariantCulture);
            rd11.Timestamp = dt_str;
            rd11.EventId = "";
            ret_listrd.Add(rd11);

            float res12 = BitConverter.ToSingle(msg_bytes, start_index + 40);
            ResponseData rd12 = new ResponseData();
            rd12.Name = "relative_humidity_pc";
            rd12.Value = res12.ToString("F", CultureInfo.InvariantCulture);
            rd12.Timestamp = dt_str;
            rd12.EventId = "";
            ret_listrd.Add(rd12);

            float res13 = BitConverter.ToSingle(msg_bytes, start_index + 44);
            ResponseData rd13 = new ResponseData();
            rd13.Name = "temperature";
            rd13.Value = res13.ToString("F", CultureInfo.InvariantCulture);
            rd13.Timestamp = dt_str;
            rd13.EventId = "";
            ret_listrd.Add(rd13);

            float res14 = BitConverter.ToSingle(msg_bytes, start_index + 48);
            ResponseData rd14 = new ResponseData();
            rd14.Name = "pressure";
            rd14.Value = res14.ToString("F", CultureInfo.InvariantCulture);
            rd14.Timestamp = dt_str;
            rd14.EventId = "";
            ret_listrd.Add(rd14);


            ushort res15 = BitConverter.ToUInt16(msg_bytes, start_index + 52);
            ResponseData rd15 = new ResponseData();
            rd15.Name = "co2";
            rd15.Value = res15.ToString("D", CultureInfo.InvariantCulture);
            rd15.Timestamp = dt_str;
            rd15.EventId = "";
            ret_listrd.Add(rd15);

            ushort res16 = BitConverter.ToUInt16(msg_bytes, start_index + 54);
            ResponseData rd16 = new ResponseData();
            rd16.Name = "voc";
            rd16.Value = res16.ToString("D", CultureInfo.InvariantCulture);
            rd16.Timestamp = dt_str;
            rd16.EventId = "";
            ret_listrd.Add(rd16);

            byte res17 = msg_bytes[start_index + 56];
            ResponseData rd17 = new ResponseData();
            rd17.Name = "iaq_index";
            rd17.Value = res17.ToString("D", CultureInfo.InvariantCulture);
            rd17.Timestamp = dt_str;
            rd17.EventId = "";
            ret_listrd.Add(rd17);


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
