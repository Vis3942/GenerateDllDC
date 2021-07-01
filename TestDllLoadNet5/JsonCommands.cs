using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace TestDllLoadNet5
{
    public class JsonCommand
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string response { get; set; }
        public MethodInfo compute_values { get; set; }
        public JsonData[] data { get; set; }
    }

    public class JsonCommands
    {
        public string hwinterface { get; set; }
        public int address { get; set; }
        public JsonCommand[] get_commands { get; set; }
        public JsonCommand[] set_commands { get; set; }
        public JsonCommand[] setup_commands { get; set; }
    }


    public class JsonData
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string units { get; set; }

    }
    //public class JsonUtils
    //{
    //    public static MethodInfo LoadMethodFromAssembly(string ass_name, string func_name)
    //    {
    //        // func_name format "classname.methodname"
    //        // Load the assembly
    //        string path = Path.Combine(Directory.GetCurrentDirectory(), ass_name);
    //        if (!System.IO.File.Exists($"{path}.dll"))
    //        {
    //            Console.WriteLine($"Failed to locate assembly {path}.dll");
    //            return null;
    //        }
    //        Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{path}.dll");
    //        string[] tmp_str = func_name.Split('.');
    //        if (tmp_str.Length != 2)
    //        {
    //            return null;
    //        }
    //        MethodInfo result = asm.GetType($"{ ass_name }.{tmp_str[0]}").GetMethod(tmp_str[1]);
    //        return result;
    //    }
    //}
}
