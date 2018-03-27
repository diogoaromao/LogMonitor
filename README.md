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

## Nuget Restore Error ##
If you get an error related with restoring nuget packages running one of the commands below, please do the following:
1. Open Solution on Visual Studio
2. Right-Click on the solution.
3. Restore NuGet Packages.

If by any chance the powerhsell commands below still don't work, please do the following:

1. Open Solution on Visual Studio.
2. Right-Click on the solution.
3. Manage NuGet Packages for Solution.
4. Select Updates tab
5. Select all packages
6. Update

The powershell commands below should now work.

## Running the log generator ##
1. Please make sure you have Visual Studio 2017 Community version installed on your computer.
2. Open a Windows Powershell and navigate to the project Powershell folder.
3. Run
```
.\Build.ps1
```
4. Navigate to the project's bin\Release folder
5. Run
```
.\LogMonitor.exe [-t threshold] -g
Ex: .\LogMonitor.exe -t 1000 -g
```

## Running the monitor ##
1. Please make sure you have Visual Studio 2017 Community version installed on your computer.
2. Open a Windows Powershell and navigate to the project Powershell folder.
3. Run
```
.\Build.ps1
```
4. Navigate to the project's bin\Release folder
5. Run
```
.\LogMonitor.exe [-t threshold] -f generated.txt
Ex: .\LogMonitor.exe -t 1000 -f generated.txt
```

## Running tests ##
1. Please make sure you have Visual Studio 2017 Community version installed on your computer.
2. Open a Windows Powershell and navigate to the project Powershell folder.
3. Run
```
.\Build.ps1
```
```
.\Test.ps1
```

## Improvements ##
1. Possibility to read from more than 1 log file at the same time.
2. Arguments validation is not working properly. The user can input a log to monitor and ask to generate a log. Only the latter one will be done.
3. When two or more arguments are not valid, the corresponding number of errors should be shown and not only the first one.
4. Possibility to parse the log from a supplied path instead of only from the examples folder.
5. Possibility to write the resulting log to a supplied path instead of only to the results folder.
6. Should be possible to generate a log file without the need to specify the threshold.
7. Components like Notification, Timer and Logger should be testable.
8. Use dependency injection (Autofac).
9. Possibility to set how frequently a timer runs and the timespan of the logs it's interested in.
10. Statistic showing host which did the most requests.
11. Powershell build command should also restore NuGet packages.
