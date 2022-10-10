using System.Collections;

namespace MakoIoT.Device.Utilities.ICalParser.Model
{
    public class ContentLine
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Hashtable Parameters { get; set; }
    }
}
