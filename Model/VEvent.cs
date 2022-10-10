using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace MakoIoT.Device.Utilities.ICalParser.Model
{
    public class VEvent
    {
        public Hashtable Properties { get; }

        public string Uid => GetString("UID");

        public string Location => GetString("LOCATION");

        public string Summary => GetString("SUMMARY");

        public DateTime DtStart => GetDateTime("DTSTART", DateTime.MinValue);

        public DateTime DtEnd => GetDateTime("DTEND", DateTime.MaxValue);

        public string Description => GetString("DESCRIPTION");

        public string Url => GetString("URL");

        public VEvent()
        {
            Properties = new Hashtable();
        }

        private string GetProperty(string name) =>
            Properties.Contains(name) ? ((ContentLine)Properties[name]).Value.Trim() : null;

        private string GetString(string name, string defaultValue = null)
        {
            var value = GetProperty(name);
            if (value == null)
                return defaultValue;

            var sb = new StringBuilder(value);
            sb.Replace(@"\;", ";");
            sb.Replace(@"\,", ",");
            sb.Replace(@"\N", "\n");
            sb.Replace(@"\n", "\n");
            return sb.ToString();
        }

        private DateTime GetDateTime(string name, DateTime defaultValue)
        {
            var value = GetProperty(name);
            if (value == null)
                return defaultValue;

            var dt = new DateTime(int.Parse(value.Substring(0, 4)),
                int.Parse(value.Substring(4, 2)),
                int.Parse(value.Substring(6, 2)));

            if (value.IndexOf('T') > 0)
                dt = dt.Add(new TimeSpan(int.Parse(value.Substring(9, 2)),
                    int.Parse(value.Substring(11, 2)),
                    int.Parse(value.Substring(13, 2))));

            return dt;
        }
    }
}
