using System;
using System.Collections.Generic;
using UtilsLibrary;


namespace PowerMeasurement
{
    public class ConvertData
    {
        public static List<ResponseData> GetPowerData(byte[] msg_bytes)
        {
            int data_index = 6;
            ushort data_id;
            List<ResponseData> to_ret_rd = new List<ResponseData>();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            while (data_index < msg_bytes.Length)
            {
                data_id = BitConverter.ToUInt16(msg_bytes, data_index);
                data_index += 2;
                switch (data_id)
                {
                case (ushort)0x8001: // onoff
                    ResponseData rd1 = new ResponseData();
                    rd1.Name = "OnOff";
                    if (msg_bytes[data_index] == 0)
                    {
                        rd1.Value = "0";
                    }
                    else
                    {
                        rd1.Value = "1";
                    }
                    rd1.Timestamp = dt_str;
                    to_ret_rd.Add(rd1); 
                    data_index += 1;
                    break;
                case (ushort)0x8002: // intensity
                    ResponseData rd2 = new ResponseData();
                    rd2.Name = "Brightness";
                    rd2.Value = ((ushort)BitConverter.ToUInt16(msg_bytes, data_index)).ToString();
                    rd2.Timestamp = dt_str;
                    to_ret_rd.Add(rd2); 
                    data_index += 2;
                    break;
                case (ushort)0x0004: // active power
                    ResponseData rd3 = new ResponseData();
                    rd3.Name = "ActivePower";
                    rd3.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                    rd3.Timestamp = dt_str;
                    to_ret_rd.Add(rd3); 
                    data_index += 4;
                    break;
                case (ushort)0x0005: //reactive power
                    data_index += 4;
                    break;
                case (ushort)0x0006: // voltage
                    ResponseData rd5 = new ResponseData();
                    rd5.Name = "Voltage";
                    rd5.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                    rd5.Timestamp = dt_str;
                    to_ret_rd.Add(rd5); 
                    data_index += 4;
                    break;
                case (ushort)0x0008: // line_freq
                    ResponseData rd6 = new ResponseData();
                    rd6.Name = "LineFrequency";
                    rd6.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                    rd6.Timestamp = dt_str;
                    to_ret_rd.Add(rd6); 
                    data_index += 4;
                    break;
                case (ushort)0x0009: // power fac
                    //ResponseData rd7 = new ResponseData();
                    //rd7.Name = "PowerFactor";
                    //rd7.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                    //rd7.Timestamp = dt_str;
                    //to_ret_rd.Add(rd7); 
                    data_index += 4;
                    break;
                case (ushort)0x000a: // apparent power
                    data_index += 4;
                    break;
                case (ushort)0x0012: // current
                    ResponseData rd9 = new ResponseData();
                    rd9.Name = "Current";
                    rd9.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                    rd9.Timestamp = dt_str;
                    to_ret_rd.Add(rd9); 
                    data_index += 4;
                    break;
                case (ushort)0x0010: // time
                    data_index += 12;
                    break;
                case (ushort)0x8011: // report interval
                    data_index += 4;
                    break;
                case (ushort)0x8013: // digital onoff
                    data_index += 1;
                    break;
                case (ushort)0x0016: // async msg count
                    data_index += 1;
                    break;
                case (ushort)0x0017: // lqi
                    data_index += 2;
                    break;
                case (ushort)0x8018: // zone id
                    data_index += 1;
                    break;
                case (ushort)0x0015: // reset count
                    data_index += 5;
                    break;
                }
            }
            if (to_ret_rd.Count > 0)
            {
                return to_ret_rd;
            }
            else
            {
                return null;
            }
        }
    }
}
