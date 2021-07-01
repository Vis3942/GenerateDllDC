using System;
using System.Collections.Generic;
using System.Text;

namespace UtilsLibrary
{
    public class TransferData
    {
        public string NodeName { get; set; }
        public string UUID { get; set; }
        public ResponseData[] Data { get; set; }
    }
}
