{
  "hwinterface": "powermeasurement", // allowed values: powermeasurement, loadcontrol, rs232, analog, digital, rs485, DALI
  "address": number,  // allowed values are 0-255 (byte only); used only for rs485 (do not use this for config which is for channel) and DALI, and reserved analog channel which are a proxy for board mounted i2c/spi buses 
  "setup_commands": [
    {
      "name": "Config",
      "description": "command to configure the rs232 interface for this channel speed, parity, num bits, stop bit, delimiter, buffer lenght if delimiter is zero",
      "type": "string[]",
      "value": "9600,N,8,1,0,0",
      "response": "",
      "data": []
    },
    {
      "name": "Initialize",
      "description":  "command to initialize sensor",
      "type": "byte[]",
      "value": "0x42,0x4D,0xE1,0x00,0x00,0x01,0x70",
      "response": "",
      "data": []
    }
  ]
  "get_commands": 
  [
    {
      "name": "GetPowerData",
      "description": "power measurement data",
      "type": "", // allowed types : byte(1), sbyte(1), short(2), ushort(2), uint(4), int(4), float(4), double(8), string(variable length), values specified as string, additional data types as arrays eg byte[], sbyte[], ushort[] , short[], uint[], int[], float[], double[], string[] => values are csv string format
      "value": "", // depending on type a single value as string, or csv if type is an array
      "response": "ConvertData.GetPowerData", // response function to use from dll
      "data": // metadata  about data returned by the response function
      [
        {
          "name": "OnOff",
          "description": "1 => on, 0 => off, other => ignore",
          "type": "byte",
          "units": ""
        },
        {
          "name": "Brightness",
          "description": "range 0-100 %, other => ignore",
          "type": "ushort",
          "units": ""
        },
        {
          "name": "ActivePower",
          "description": "",
          "type": "float",
          "units": "VA"
        },
        {
          "name": "Voltage",
          "description": "",
          "type": "float",
          "units": "V"
        },
        {
          "name": "Current",
          "description": "",
          "type": "float",
          "units": "mA"
        },
        {
          "name": "LineFrequency",
          "description": "",
          "type": "float",
          "units": "Hz"
        }
      ]
    }
  ],
  "set_commands": 
  [
    {
      "name": "OnOff",
      "description": "1 => turn on, 0 => turn off",
      "type": "byte",
      "value": "1",
      "response": "",
      "data": []
    },
    {
      "name": "Brightness",
      "description": "0-100%",
      "type": "ushort",
      "value": "50",
      "response": "",
      "data": []
    }
  ]
}


