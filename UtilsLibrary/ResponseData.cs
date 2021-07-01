using System;
using System.Text;

namespace UtilsLibrary
{
    public class ResponseData
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Timestamp { get; set; }
        public string EventId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Name = {0}, ", Name);
            sb.AppendFormat("Value = {0}, ", Value);
            sb.AppendFormat("Timestamp = {0}, ", Timestamp);
            sb.AppendFormat("EventId = {0}, ", EventId);
            return sb.ToString();
        }
    }
}
