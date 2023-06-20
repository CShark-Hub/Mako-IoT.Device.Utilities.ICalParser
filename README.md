# MakoIoT.Device.Utilities.ICalParser
Simple parser for [ICalendar](https://icalendar.org/) data. Extracts event details from VEVENT objects.

## Usage
```c#
//parse calendar from web
HttpClient httpClient = new();
using var response = _httpClient.Get(_config.CalendarUrl);
response.EnsureSuccessStatusCode();

var parser = new Parser();
var events = new ArrayList();
using var reader = new StreamReader(response.Content.ReadAsStream())           
parser.Parse(reader, e => 
{ 
  //this function gets called when event object is found
  events.Add(e); 
});           
```

## Known limitations
- no support for recurring events (recurrence patters)
- no support for nestes VEVENT objects
- no time zones support
