using nanoFramework.TestFramework;
using System;
using System.Collections;
using System.IO;
using System.Text;
using MakoIoT.Device.Utilities.ICalParser.Model;

namespace MakoIoT.Device.Utilities.ICalParser.Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse_given_single_event_should_callback()
        {
            string input = @"BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//Mozilla.org/NONSGML Mozilla Calendar V1.1//EN
BEGIN:VEVENT
CREATED:20060717T210517Z
LAST-MODIFIED:20060717T210718Z
DTSTAMP:20060717T210718Z
UID:uuid1153170430406
SUMMARY:Test event
DTSTART:20060718T100000
DTEND:20060718T110000
LOCATION:Daywest
END:VEVENT
END:VCALENDAR";

            var buffer = Encoding.UTF8.GetBytes(input);
            VEvent result = null;

            var sut = new Parser();
            using (var reader = new StreamReader(new MemoryStream(buffer)))
            {
                sut.Parse(reader, e => result = e);
            }

            Assert.NotNull(result);
            Assert.Equal("Test event", result.Summary);
            Assert.Equal("Daywest", result.Location);
            Assert.Equal("uuid1153170430406", result.Uid);
            Assert.Equal( new DateTime(2006,7,18,10,0,0), result.DtStart);
            Assert.Equal( new DateTime(2006,7,18,11,0,0), result.DtEnd);
        }

        [TestMethod]
        public void Parse_given_multiple_events_should_callback_on_each_event()
        {
            string input = @"BEGIN:VCALENDAR
VERSION:2.0
PRODID:kwywoz-rules
CALSCALE:GREGORIAN
X-WR-CALNAME:KiedyWywóz
X-APPLE-LANGUAGE:pl
X-APPLE-REGION:PL
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:9ca13e760c8f50b36129ecc5af1fd97d
DTSTART;VALUE=DATE:20221004
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:zmieszane - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji zmieszane w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:b8b8352477c429137cd8fc1a85d8a7fe
DTSTART;VALUE=DATE:20221005
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:BIO - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji BIO w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:8aa8f38edcdf86eb6f9dd21868643d84
DTSTART;VALUE=DATE:20221006
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:tworzywa - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji tworzywa w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:bb492132cad98a8f75d727c7bf3864a0
DTSTART;VALUE=DATE:20221007
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:papier - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji papier w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:6861d12872024010b2641b6198a3b8c7
DTSTART;VALUE=DATE:20221011
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:zmieszane - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji zmieszane w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:8653be5e7b42279cd3b87034734d22ac
DTSTART;VALUE=DATE:20221012
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:BIO - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji BIO w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:c3f954fa762968edb4a811a5d65620d7
DTSTART;VALUE=DATE:20221018
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:zmieszane - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji zmieszane w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:d2d4d694f1c0128ac333f6f11ebbd494
DTSTART;VALUE=DATE:20221019
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:BIO, szkło - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji BIO, szkło w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:cf4af1749dfbf1ca02b8924b9c08f342
DTSTART;VALUE=DATE:20221020
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:tworzywa - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji tworzywa w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:27adb260016911f0104a1607b31b60fd
DTSTART;VALUE=DATE:20221021
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:papier - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji papier w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:405afe639d349c4f4b81939d3749bcd1
DTSTART;VALUE=DATE:20221025
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:zmieszane - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji zmieszane w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:a3fb57761e528ac47c161132c81651f1
DTSTART;VALUE=DATE:20221026
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:BIO - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji BIO w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:7500e20f33c1c4c4bc786b387d54a22d
DTSTART;VALUE=DATE:20221102
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:zmieszane - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji zmieszane w lokalizacji Wrocław
END:VEVENT
BEGIN:VEVENT
DTSTAMP:20221004T113200Z
UID:53d12ae913e760c9cd76fa3d9f840f5c
DTSTART;VALUE=DATE:20221103
CLASS:PUBLIC
SUMMARY;LANGUAGE=pl:BIO - Wrocław
TRANSP:TRANSPARENT
CATEGORIES:KiedyWywoz, Odbior odpadow
DESCRIPTION:KiedyWywoz przypomina: odbior frakcji BIO w lokalizacji Wrocław
END:VEVENT
END:VCALENDAR";

            var buffer = Encoding.UTF8.GetBytes(input);
            var events = new ArrayList();

            var sut = new Parser();
            using (var reader = new StreamReader(new MemoryStream(buffer)))
            {
                sut.Parse(reader, e => events.Add(e));
            }

            Assert.Equal(14, events.Count);
            var e = events[13] as VEvent;
            Assert.Equal(new DateTime(2022, 11, 3), e.DtStart);
            Assert.Equal(DateTime.MaxValue, e.DtEnd);
            Assert.Equal("BIO - Wrocław", e.Summary);
            Assert.Equal("KiedyWywoz przypomina: odbior frakcji BIO w lokalizacji Wrocław", e.Description);
        }
    }
}
