using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using UtilsLibrary;

namespace TestDllLoadNet5
{
    class Program
    {
        static void Main(string[] args)
        {
            string device1 = "SM_UART_04L";
            string device2 = "ResponseOneWeatherTransmitter";
            string device3 = "PowerMeasurement";
            string device4 = "Seimens_QSM2100_PM2_5";
            string device5 = "AutomationCompsDifferentialPressure";
            string device6 = "OptySecUVIntensity";
            string device7 = "ConductivitySensorCompany1";
            string device8 = "DissolvedOxygenSensorCompany1";
            string json_fname = "";
            MethodInfo jsonCalcMethod1 = null;
            MethodInfo jsonCalcMethod2 = null;
            MethodInfo jsonCalcMethod3 = null;
            MethodInfo jsonCalcMethod4 = null;
            MethodInfo jsonCalcMethod5 = null;
            MethodInfo jsonCalcMethod6 = null;
            MethodInfo jsonCalcMethod7 = null;
            MethodInfo jsonCalcMethod8 = null;

            if ((device8 != null) && (device8.Length > 0))
            {
                json_fname = $"{device8}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device8, jc.response);
                            if ("Get Dissolved Oxygen".Equals(jc.name))
                            {
                                jsonCalcMethod8 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod8 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp2 = new byte[] {
                            0x17, 0x00, 0x1A, 0x00, 0x01, 0x00, 0x01, 0x04,
                            0x00, 0x0D, 0x04, 0x03, 0x08, 0x02, 0xCE, 0x00,
                            0x02, 0x00, 0xED, 0x00, 0x01, 0x02, 0x3B
                    };
                    List<ResponseData> res_rd2 = (List<ResponseData>)jsonCalcMethod8.Invoke(null, new object[] { data_resp2, 10, 13 });
                    Console.WriteLine(string.Format("Computed value dissolved oxy = {0}", DisplayRDValuues(res_rd2)));
                    //string json_str = JsonConvert.SerializeObject(res_rd2);
                    //dcps.WriteToPipe(json_str);
                    Console.WriteLine("              ------              ");
                }
            }


            if ((device7 != null) && (device7.Length > 0))
            {
                json_fname = $"{device7}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device7, jc.response);
                            if ("Get Conductivity".Equals(jc.name))
                            {
                                jsonCalcMethod7 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod7 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp2 = new byte[] {
                            0x06, 0x03, 0x08, 0x01,
                            0x02, 0x00, 0x00,
                            0x00, 0xB0, 0x00, 0x01, 0xAD, 0x88
                        };
                    List<ResponseData> res_rd2 = (List<ResponseData>)jsonCalcMethod7.Invoke(null, new object[] { data_resp2, 0, 13 });
                    Console.WriteLine(string.Format("Computed value conductivity = {0}", DisplayRDValuues(res_rd2)));
                    //string json_str = JsonConvert.SerializeObject(res_rd2);
                    //dcps.WriteToPipe(json_str);
                    Console.WriteLine("              ------              ");
                }
            }


            if ( (device3 != null)&& (device3.Length > 0) )
            {
                json_fname = $"{device3}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device3, jc.response);
                            if ("GetPowerData".Equals(jc.name))
                            {
                                jsonCalcMethod3 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod3 != null)
                {
                    //byte[] data_resp3 = new byte[] {
                    //0x52, 0x00, 0x04, 0x00, 0x0D, 0x00, 0x01, 0x80, 0x00, 0x02,
                    //0x80, 0x32, 0x00, 0x04, 0x00, 0x8F, 0xC2, 0xF5, 0x3C, 0x06,
                    //0x00, 0x66, 0x66, 0xF0, 0x42, 0x12, 0x00, 0xCD, 0xCC, 0x9C,
                    //0x40, 0x08, 0x00, 0x50, 0x8D, 0x70, 0x42, 0x09, 0x00, 0xA8,
                    //0xC1, 0x53, 0xBD, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A,
                    //0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x80, 0x03, 0x00, 0x00,
                    //0x00, 0x15, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                    //};
                    byte[] data_resp3 = new byte[]
                    {
                        0x4C, 0x00, 0x4A, 0x00, 0x28, 0x00, 0x03, 0x00, 0xE1, 0x7A, 0xF5, 0x42,
                        0x6F, 0x12, 0x83, 0x3A, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xF5, 0x6F, 0x42,
                        0x00, 0x00, 0x00, 0x00, 0x41, 0xa5, 0xc2, 0x8f,
                        0x00, 0x64, 0x01, 0x00, 0x00, 0x00, 0x00, 
                        0x6F, 0x12, 0x83, 0x3A, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xF5, 0x6F, 0x42, 
                        0x00, 0x00, 0x00, 0x00, 0x41, 0xa5, 0xc2, 0x8f,
                        0x00, 0x64, 0x02, 0x00, 0x00, 0x00, 0x00, 0x6F, 0x12, 0x83, 0x3A, 
                        0x00, 0x00, 0x00, 0x00, 0xC3, 0xF5, 0x6F, 0x42, 
                        0x00, 0x00, 0x00, 0x00, 0x41, 0xa5, 0xc2, 0x8f, 0x00, 0x64
                    };
                    //List<ResponseData> res_rd1 = CoreGetDataCalculateMethod(data_resp3);
                    //Console.WriteLine("              ------              ");
                    //Console.WriteLine(string.Format("Computed value getData = {0}", DisplayRDValuues(res_rd1)));
                    //Console.WriteLine("              ------              ");
                    List<ResponseData> res_rd2 = (List<ResponseData>)jsonCalcMethod3.Invoke(null, new object[] { data_resp3, 8, 26 });
                    Console.WriteLine(string.Format("Computed value getData = {0}", DisplayRDValuues(res_rd2)));
                    //string json_str = JsonConvert.SerializeObject(res_rd2);
                    //dcps.WriteToPipe(json_str);
                    Console.WriteLine("              ------              ");
                }
            }

            if ((device2 != null) && (device2.Length > 0))
            {
                json_fname = $"{device2}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device2, jc.response);
                            if ("Get Weather Data".Equals(jc.name))
                            {
                                jsonCalcMethod2 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod2 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp2 = new byte[] {
                            0x2E, 0x00, 0x18, 0x00,
                            0x01, 0x00, 0x00,
                            0x30, 0x20, 0x30, 0x30, 0x30, 0x2E, 0x30, 0x30, 0x20, 0x30,
                            0x30, 0x30, 0x2E, 0x30, 0x20, 0x30, 0x32, 0x35, 0x2E, 0x35,
                            0x20, 0x30, 0x34, 0x39, 0x2E, 0x37, 0x20, 0x31, 0x30, 0x30,
                            0x32, 0x2E, 0x39, 0x20, 0x30, 0x30, 0x2A, 0x31, 0x43, 0x0D
                        };
                    List<ResponseData> res_rd2 = (List<ResponseData>)jsonCalcMethod2.Invoke(null, new object[] { data_resp2 });
                    Console.WriteLine(string.Format("Computed value resp weather data = {0}", DisplayRDValuues(res_rd2)));
                    //string json_str = JsonConvert.SerializeObject(res_rd2);
                    //dcps.WriteToPipe(json_str);
                    Console.WriteLine("              ------              ");
                }
            }

            if ((device1 != null) && (device1.Length > 0))
            {
                json_fname = $"{device1}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device1, jc.response);
                            if ("Get PM2.5 Data".Equals(jc.name))
                            {
                                jsonCalcMethod1 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod1 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp1 = new byte[] {
                        0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x00, 
                        0x42, 0x4D, 0x00, 0x03, 0xE1, 0x03, 0xE7, 0x1C, 0x03, 0xE7, 
                        0x03, 0xE7, 0x03, 0xE7, 0x03, 0xE7, 0x00, 0x00, 0x00, 0x00, 
                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x02, 
                        0x06, 0x2F
                    };
                    List<ResponseData> res_rd1 = (List<ResponseData>)jsonCalcMethod1.Invoke(null, new object[] { data_resp1 });
                    Console.WriteLine(string.Format("Computed value pm 2.5 = {0}", DisplayRDValuues(res_rd1)));
                    //string json_str = JsonConvert.SerializeObject(res_rd2);
                    //dcps.WriteToPipe(json_str);
                    Console.WriteLine("              ------              ");
                }
            }

            if ((device4 != null) && (device4.Length > 0))
            {
                json_fname = $"{device4}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device4, jc.response);
                            if ("GetParticulateData2_5".Equals(jc.name))
                            {
                                jsonCalcMethod4 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod4 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp4 = new byte[] {
                        0x16, 0x00, 0x14, 0x00, 0x2A, 0x00, 0x03, 0x01, 0xEC, 0x51, 0x9A,
                        0x44, 0x02, 0xF6, 0x96, 0xDE, 0x45, 0x03, 0x43, 0x1C, 0x93, 0x40                    };
                    List<ResponseData> res_rd1 = (List<ResponseData>)jsonCalcMethod4.Invoke(null, new object[] { data_resp4, 18, 4 });
                    Console.WriteLine(string.Format("Computed value pm 2.5 = {0}", DisplayRDValuues(res_rd1)));
                    Console.WriteLine("              ------              ");
                }
            }

            if ((device5 != null) && (device5.Length > 0))
            {
                json_fname = $"{device5}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device5, jc.response);
                            if ("GetDifferentialPressure".Equals(jc.name))
                            {
                                jsonCalcMethod5 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod5 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp5 = new byte[] {
                        0x0c, 0x00, 0x0a, 0x00, 0x2A, 0x00, 0x01, 0x03, 0xEC, 0x51, 0x9A, 0x44
                    };
                    List<ResponseData> res_rd5 = (List<ResponseData>)jsonCalcMethod5.Invoke(null, new object[] { data_resp5, 8, 4 });
                    Console.WriteLine(string.Format("diff pressure = {0}", DisplayRDValuues(res_rd5)));
                    Console.WriteLine("              ------              ");
                }
            }


            if ((device6 != null) && (device6.Length > 0))
            {
                json_fname = $"{device6}.json";
                if (System.IO.File.Exists(json_fname))
                {
                    string json_str = System.IO.File.ReadAllText(json_fname);
                    JsonCommands dev_json = JsonSerializer.Deserialize<JsonCommands>(json_str);
                    foreach (JsonCommand jc in dev_json.get_commands)
                    {
                        if (jc.response.Length > 0)
                        {
                            jc.compute_values = JsonUtils.LoadMethodFromAssembly(null, device6, jc.response);
                            if ("GetUVIntensity".Equals(jc.name))
                            {
                                jsonCalcMethod6 = jc.compute_values;
                            }
                        }
                    }
                }
                if (jsonCalcMethod6 != null)
                {
                    // generate resp
                    // 2: len of bytes inclusive of this one, 2: unique packet identifier, remainder of packet data
                    byte[] data_resp6 = new byte[] {
                        0x0c, 0x00, 0x0a, 0x00, 0x2A, 0x00, 0x01, 0x09, 0x00, 0x00, 0x00, 0x00
                    };
                    List<ResponseData> res_rd6 = (List<ResponseData>)jsonCalcMethod6.Invoke(null, new object[] { data_resp6, 8, 4 });
                    Console.WriteLine(string.Format("uv intensity = {0}", DisplayRDValuues(res_rd6)));
                    Console.WriteLine("              ------              ");
                }
            }



            Console.ReadLine();

            Console.WriteLine("Testing load json devices file");
            string node_fname = "NCx_EB443D.json";
            Console.WriteLine($"Loading devices for node -> {node_fname}");
            NodeDevices node_devices = null;
            if (System.IO.File.Exists(node_fname))
            {
                string json_str = System.IO.File.ReadAllText(node_fname);
                try
                {
                    NodeDevices ndevs = JsonSerializer.Deserialize<NodeDevices>(json_str);
                    //NodeDevices ndevs = JsonConvert.DeserializeObject<NodeDevices>(json_str);
                    
                    if ((ndevs.computedData != null) && (ndevs.computedData.compute != null) && (ndevs.computedData.compute.Length > 0))
                    {
                        ndevs.computedData.compute_values = JsonUtils.LoadMethodFromAssembly(null, ndevs.computedData.name, ndevs.computedData.compute);
                    }
                    Dictionary<string, string> tmp_param = new Dictionary<string, string>();
                    tmp_param["hvac_v"] = "120";
                    tmp_param["duct_cs"] = "4";
                    TransferData td1 = new TransferData();
                    td1.Data = new ResponseData[1];
                    ResponseData rd1 = new ResponseData()
                    {
                        Name = "ana04_air_temperature",
                        Value = "76.50"
                    };
                    td1.Data[0] = rd1;

                    TransferData td2 = new TransferData();
                    td2.Data = new ResponseData[1];
                    ResponseData rd2 = new ResponseData()
                    {
                        Name = "ana06_rel_humidity",
                        Value = "20.58"
                    };
                    td2.Data[0] = rd2;

                    TransferData td3 = new TransferData();
                    td3.Data = new ResponseData[1];
                    ResponseData rd3 = new ResponseData()
                    {
                        Name = "ana09_current",
                        Value = "0.80"
                    };
                    td3.Data[0] = rd3;

                    TransferData td4 = new TransferData();
                    td4.Data = new ResponseData[1];
                    ResponseData rd4 = new ResponseData()
                    {
                        Name = "ana15_air_speed",
                        Value = "2.69"
                    };
                    td4.Data[0] = rd4;
                    List<TransferData> node_recvd_data = new List<TransferData>();
                    node_recvd_data.Add(td1);
                    node_recvd_data.Add(td2);
                    node_recvd_data.Add(td3);
                    node_recvd_data.Add(td4);

                    var node_rd = (List<ResponseData>)ndevs.computedData.compute_values.Invoke(null,
                                                            new object[] { node_recvd_data, tmp_param });

                    if (node_rd != null)
                    {
                        Console.WriteLine(DisplayRDValuues(node_rd));
                    }

                    node_devices = ndevs;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    node_devices = null;
                }
            }
            else
            {
                // debug
                Console.WriteLine("node json not found");
                node_devices = null;
            }

            Console.ReadLine();

        }

        public static List<ResponseData> CoreGetDataCalculateMethod(byte[] msg_bytes)
        {
            int data_index = 6;
            ushort data_id;
            List<ResponseData> to_ret_rd = new List<ResponseData>();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + "+00:00";
            while (data_index < msg_bytes.Length)
            {
                data_id = BitConverter.ToUInt16(msg_bytes, data_index);
                data_index += 2;
                switch (data_id)
                {
                    case (ushort)0x8001: // onoff
                        ResponseData rd1 = new ResponseData();
                        rd1.Name = "onoff";
                        if (msg_bytes[data_index] == 0)
                        {
                            rd1.Value = "0";
                        }
                        else
                        {
                            rd1.Value = "1";
                        }
                        rd1.Timestamp = dt_str;
                        to_ret_rd.Add(rd1); data_index += 1;
                        break;
                    case (ushort)0x8002: // intensity
                        ResponseData rd2 = new ResponseData();
                        rd2.Name = "intensity";
                        rd2.Value = ((ushort)BitConverter.ToUInt16(msg_bytes, data_index)).ToString();
                        rd2.Timestamp = dt_str;
                        to_ret_rd.Add(rd2); data_index += 2;
                        break;
                    case (ushort)0x0004: // active power
                        ResponseData rd3 = new ResponseData();
                        rd3.Name = "activepwr";
                        rd3.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                        rd3.Timestamp = dt_str;
                        to_ret_rd.Add(rd3); data_index += 4;
                        break;
                    case (ushort)0x0005: //reactive power
                        data_index += 4;
                        break;
                    case (ushort)0x0006: // voltage
                        ResponseData rd5 = new ResponseData();
                        rd5.Name = "volt";
                        rd5.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                        rd5.Timestamp = dt_str;
                        to_ret_rd.Add(rd5); data_index += 4;
                        break;
                    case (ushort)0x0008: // line_freq
                        ResponseData rd6 = new ResponseData();
                        rd6.Name = "volt";
                        rd6.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                        rd6.Timestamp = dt_str;
                        to_ret_rd.Add(rd6); data_index += 4;
                        break;
                    case (ushort)0x0009: // power fac
                        ResponseData rd7 = new ResponseData();
                        rd7.Name = "powerfac";
                        rd7.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                        rd7.Timestamp = dt_str;
                        to_ret_rd.Add(rd7); data_index += 4;
                        break;
                    case (ushort)0x000a: // apparent power
                        data_index += 4;
                        break;
                    case (ushort)0x0012: // current
                        ResponseData rd9 = new ResponseData();
                        rd9.Name = "current";
                        rd9.Value = ((float)BitConverter.ToSingle(msg_bytes, data_index)).ToString("F");
                        rd9.Timestamp = dt_str;
                        to_ret_rd.Add(rd9); data_index += 4;
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

        static string DisplayRDValuues(List<ResponseData> to_disp)
        {
            StringBuilder sb = new StringBuilder();
            if (to_disp != null)
            {
                for (int ii = 0; ii < to_disp.Count; ii++)
                {
                    sb.AppendFormat("{0} => {1} \r\n", ii, to_disp[ii].ToString());
                }
            }
            else
            {
                sb.AppendLine("null value.");
            }
            return sb.ToString();
        }


    }
}
