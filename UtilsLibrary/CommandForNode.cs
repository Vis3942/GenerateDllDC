using System;
using System.Collections.Generic;
using System.Text;

namespace UtilsLibrary
{
    public class CommandForNode
    {
        public string Node { get; set; }
        public string UUID { get; set; }
        public string EventId { get; set; }
        public Command[] Commands { get; set; } 
    }

    public class Command
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
