using System;
using System.Collections.Generic;
using System.Globalization;
using UtilsLibrary;

namespace ComputedAhDpHi
{
    public class ConvertData
    {
        public static List<ResponseData> GetComputedData(List<TransferData> curr_data, Dictionary<string, string> compute_parameters)
        {
            // to compute absolute humidity (name: abs_humidity (XVI)):
            /*
                                                    6.112 × e^[(17.67 × T)/(T+243.5)] × rh × 2.1674
                 Absolute Humidity (grams/m3) =    ------------------------------------------------
                                                           (273.15+T) x 100 x 0.08314
                        T is in deg C, and rh = relative humidity in percentage
                requires: temperature_f (convert to deg c before using in calculation), relative_humidity_pc, 
            */
            // to compute dew point: (name: dew_point (XIV)): 
            /*
                H = (log10(RH)-2)/0.4343 + (17.62*T)/(243.12+T);
                Dp = 243.12*H/(17.62-H); // this is the dew point in Celsius
             requires: temperature_f (convert to deg c before using in calculation), relative_humidity_pc, 
            */
            // to compute heat index: (name: heat_index(XV)): 
            /*
             * first compute using simple formula
             * HI = 0.5 * {T + 61.0 + [(T-68.0)*1.2] + (RH*0.094)}
             * if (HI is 80 deg F or higher), use regression formula with corrections
             *   Regression formula with adjustments
                 HI = -42.379 + 2.04901523*T + 10.14333127*RH - .22475541*T*RH - .00683783*T*T 
                        - .05481717*RH*RH + .00122874*T*T*RH + .00085282*T*RH*RH - .00000199*T*T*RH*RH
                 T is in deg F, RH in relative humidity pc
                 -> Case 1: Compute adjustment if RH < 13%, and 80 <= temp deg F <= 112
                        adjust_1 = [(13-RH)/4]*SQRT{[17-ABS(T-95.)]/17}
                        HI = HI - adjust1
                 -> Case 2: Compute adjustment if RH > 85% and 80 <= temp deg F <= 87
                        adjust_2 = [(RH-85)/10] * [(87-T)/5]
                        HI = HI + adjust_2
                 requires: temperature_f, relative_humidity_pc, 

             */
            /*
             * require <descriptive name> (<responseData.Name>)
             * require temperature_f (I)
             * require relative humidity (II)
             */
            if (curr_data == null)
            {
                return null;
            }

            if (curr_data.Count == 0)
            {
                return null;
            }

            ResponseData temperature_f_rd = null;
            ResponseData rel_hum_pc_rd = null;
            ResponseData air_flow_rd = null;
            ResponseData amps_rd = null;
            int ii=0, jj=0;
            while (ii < curr_data.Count) 
            {
                jj = 0;
                while (jj < curr_data[ii].Data.Length)
                {
                    var rd = curr_data[ii].Data[jj];
                    if (rd.Name.EndsWith("air_temperature"))
                    {
                        temperature_f_rd = rd;
                    }
                    else if (rd.Name.EndsWith("rel_humidity"))
                    {
                        rel_hum_pc_rd = rd;
                    }
                    else if (rd.Name.EndsWith("current"))
                    {
                        amps_rd = rd;
                    }
                    else if (rd.Name.EndsWith("air_speed"))
                    {
                        air_flow_rd = rd;
                    }
                    jj++;
                }
                if  ( 
                        (temperature_f_rd != null) && (rel_hum_pc_rd != null) &&
                        (air_flow_rd != null) && (amps_rd != null)
                    )
                {
                    break;
                }
                ii++;
            }

            List<ResponseData> to_ret = new List<ResponseData>();
            string dt_str = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss") + " +00:00";
            if ((temperature_f_rd != null) && (rel_hum_pc_rd != null))
            {
                // compute abs humidity
                bool sts;
                float temp_f;
                sts = float.TryParse(temperature_f_rd.Value, out temp_f);
                if (sts)
                {
                    float rel_hum_pc;
                    sts = float.TryParse(rel_hum_pc_rd.Value, out rel_hum_pc);
                    if (sts)
                    {
                        ResponseData rd_ah = new ResponseData();
                        rd_ah.Name = "abs_humidity";
                        ResponseData rd_dew_point = new ResponseData();
                        rd_dew_point.Name = "dew_point";
                        ResponseData rd_heat_index = new ResponseData();
                        rd_heat_index.Name = "heat_index";
                        float temp_c = (temp_f - 32) / 1.8f;
                        double tmpval1 = 6.112 * Math.Pow( Math.E, ( 17.67 * temp_c) / (temp_c + 243.5) ) *rel_hum_pc * 2.1674;
                        double tmpval2 = (273.15 + temp_c) * 100 * 0.08314;
                        double tmpval3 = tmpval1 / tmpval2;
                        rd_ah.Value = tmpval3.ToString("F", CultureInfo.InvariantCulture);
                        rd_ah.Timestamp = dt_str;
                        rd_ah.EventId = "";
                        to_ret.Add(rd_ah);
                        // rd_dew_point
                        var H = ( ( Math.Log10(rel_hum_pc) - 2) / 0.4343 ) + ( (17.62 * temp_c) / (243.12 + temp_c) );
                        var Dp = 243.12 * H / (17.62 - H); // this is the dew point in Celsius
                        tmpval3 = Dp * 1.8 + 32.0;
                        rd_dew_point.Value = tmpval3.ToString("F", CultureInfo.InvariantCulture);
                        rd_dew_point.Timestamp = dt_str;
                        rd_dew_point.EventId = "";
                        to_ret.Add(rd_dew_point);
                        // compute: rd_heat_index
                        var HI = 0.5 * (temp_f + 61.0 + ((temp_f - 68.0) * 1.2) + (rel_hum_pc * 0.094));
                        if (HI >= 80)
                        {
                            tmpval1 = -42.379 + 2.04901523 * temp_f + 10.14333127 * rel_hum_pc - .22475541 * temp_f * rel_hum_pc;
                            tmpval2 = rel_hum_pc * rel_hum_pc;
                            tmpval3 = temp_f * temp_f;
                            tmpval1 = tmpval1 -.00683783 * tmpval3 - .05481717 * tmpval2;
                            tmpval1 = tmpval1 + .00122874 * tmpval3 * rel_hum_pc + .00085282 * temp_f * tmpval2 - .00000199 * tmpval3 * tmpval2;
                            // if RH < 13%, and 80 <= temp deg F <= 112
                            if ( (rel_hum_pc < 13) && ( (temp_f >= 80 ) && (temp_f <= 112) ) )
                            {
                                //[(13 - RH)/ 4]*SQRT{[17 - ABS(T - 95.)]/ 17}
                                tmpval2 = (13 - rel_hum_pc) * Math.Sqrt((17 - Math.Abs(temp_f - 95)) / 17);
                                tmpval3 = tmpval1 - tmpval2;
                            }
                            //Compute adjustment if RH > 85% and 80 <= temp deg F <= 87
                            else if ( (rel_hum_pc > 85) && ( (temp_f >= 80 ) && ( temp_f <= 87 )  ) )
                            {
                                //[(RH - 85)/ 10] * [(87 - T) / 5]
                                tmpval2 = ((rel_hum_pc - 85) / 10) * ( (87 - temp_f) / 5 );
                                tmpval3 = tmpval1 + tmpval2;
                            }
                            else
                            {
                                tmpval3 = tmpval1;
                            }
                        }
                        else
                        {
                            tmpval3 = HI;
                        }
                        rd_heat_index.Value = tmpval3.ToString("F", CultureInfo.InvariantCulture);
                        rd_heat_index.Timestamp = dt_str;
                        rd_heat_index.EventId = "";
                        to_ret.Add(rd_heat_index);
                    }
                }
            }

            if ((compute_parameters != null) && (compute_parameters.Count > 0) )
            {
                // power consumption (XIII) reqs: (param: hvac_v), (measured value:   amps (XII))
                if ( (amps_rd != null) && compute_parameters.ContainsKey("hvac_v") )
                {
                    float conv_f;
                    bool sts = float.TryParse(compute_parameters["hvac_v"], out conv_f);
                    float conv_f2;
                    bool sts2 = float.TryParse(amps_rd.Value, out conv_f2);
                    if (sts && sts2)
                    {
                        ResponseData power_rd = new ResponseData();
                        power_rd.Name = "power";
                        power_rd.Timestamp = dt_str;
                        power_rd.EventId = "";
                        power_rd.Value = (conv_f * conv_f2).ToString("F", CultureInfo.InvariantCulture);
                        to_ret.Add(power_rd);
                    }
                }
                // flow rate (XV11) reqs: (param: duct_cs) , (measured value: air speed (XI)) 
                if ((air_flow_rd != null) && compute_parameters.ContainsKey("duct_cs"))
                {
                    float conv_f;
                    bool sts = float.TryParse(compute_parameters["duct_cs"], out conv_f);
                    float conv_f2;
                    bool sts2 = float.TryParse(air_flow_rd.Value, out conv_f2);
                    if (sts && sts2)
                    {
                        ResponseData flowrate_rd = new ResponseData();
                        flowrate_rd.Name = "flow_rate";
                        flowrate_rd.Timestamp = dt_str;
                        flowrate_rd.EventId = "";
                        flowrate_rd.Value = (conv_f * conv_f2).ToString("F", CultureInfo.InvariantCulture);
                        to_ret.Add(flowrate_rd);
                    }
                }
            }
            if (to_ret.Count > 0)
            {
                return to_ret;
            }
            else
            {
                return null;
            }
        }
    }
}
