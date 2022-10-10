using System;
using System.Collections;
using System.IO;
using MakoIoT.Device.Utilities.ICalParser.Model;

namespace MakoIoT.Device.Utilities.ICalParser
{
    public delegate void VEventFoundDelegate(VEvent e);

    public class Parser
    {
        public void Parse(StreamReader reader, VEventFoundDelegate vEventFoundDelegate)
        {
            VEvent currentVEvent = null;
            string line = reader.ReadLine();
            while (line != null)
            {
                if (line == String.Empty)
                    continue;

                var contentLine = ParseLine(line);
                if (contentLine.Name == "BEGIN")
                {
                    if (contentLine.Value == "VEVENT")
                    {
                        if (currentVEvent != null)
                            throw new NotSupportedException("Nested VEVENTs not supported");

                        currentVEvent = new VEvent();
                    }
                }
                else if (contentLine.Name == "END")
                {
                    if (contentLine.Value == "VEVENT")
                    {
                        vEventFoundDelegate(currentVEvent);
                        currentVEvent = null;
                    }
                }
                else if (currentVEvent != null)
                {
                    currentVEvent.Properties[contentLine.Name] = contentLine;
                }

                line = reader.ReadLine();
            }
        }

        private ContentLine ParseLine(string line)
        {
            var nvSplit = line.Split(new char[] { ':' }, 2);
            var value = nvSplit[1].Trim();
            var npSplit = nvSplit[0].Split(new char[] { ';' }, 2);
            var name = npSplit[0].Trim().ToUpper();

            Hashtable prms = null;
            if (npSplit.Length == 2)
            {
                prms = new Hashtable();
                var paramString = npSplit[1].Trim().Trim(';').Split(';');

                foreach (var pst in paramString)
                {
                    var pvs = pst.Split(new[] { '=' }, 2);
                    var pmvs = pvs[1].Trim().Split(',');
                    var values = new string[pmvs.Length];
                    for (int j = 0; j < pmvs.Length; j++)
                    {
                        values[j] = pmvs[j].Trim().Trim('\"');
                    }

                    prms.Add(pvs[0].Trim().ToUpper(), values);
                }
            }

            return new ContentLine { Name = name, Value = value, Parameters = prms };
        }
    }
}
