## HTTP log monitoring console program ##

Create a simple console program that monitors HTTP traffic on your machine:

* Consume an actively written-to w3c-formatted HTTP access log (https://en.wikipedia.org/wiki/Common_Log_Format)
* Every 10s, display in the console the sections of the web site with the most hits (a section is defined as being what's before the second '/' in a URL. i.e. the section for "http://my.site.com/pages/create' is "http://my.site.com/pages"), as well as interesting summary statistics on the traffic as a whole.
* Make sure a user can keep the console app running and monitor traffic on their machine
* Whenever total traffic for the past 2 minutes exceeds a certain number on average, add a message saying that “High traffic generated an alert - hits = {value}, triggered at {time}”
* Whenever the total traffic drops again below that value on average for the past 2 minutes, add another message detailing when the alert recovered
* Make sure all messages showing when alerting thresholds are crossed remain visible on the page for historical reasons.
* Write a test for the alerting logic
* Explain how you’d improve on this application design

## Running the log generator ##
1. Open Visual Studio and Build code on Release Mode
2. Open a Windows Powershell and navigate to the project bin\Release folder
```
.\LogMonitor.exe -t 1000 -g
```

## Running the monitor ##
1. Open Visual Studio and Build code on Release Mode
2. Open a Windows Powershell and navigate to the project bin\Release folder
```
.\LogMonitor.exe -t 1000 -f generated.txt
```

## Running tests ##
To be done...

## Improvements ##
1. Possibility to read more than 1 log file.
2. Arguments validation is not working properly. The user can input a log to monitor and ask to generate a log. Only the latter one will be done.
3. Sites as dashes should be ignored.
4. If user supplies a log file that doesn't exist, an exception will be thrown.
5. Write Log Monitoring result to file.
6. Possibility to parse the log from a supplied path instead of only from the examples folder.
