# About
Flog is a simple threadsafe file logging tool. Flog was born of frustration
with the complexity of .NET tracing, the logging application block, and even
log4net. All of these frameworks require too much configuration for quick 
and simple logging. If you need to log to a database, rolling log files, or
anything but a single text file, Flog is not for you. If you want to be able
to enable logging with a single entry in app.config, then Flog may be for you.
One of the few requirements that I had for Flog was that it should, in the spirit
of log4net, never interfere with the operation of the host program, and it
should be disabled by default. This means that Flog is a good last resort
logger to have embedded in your program to be enabled when you need it.

# Usage
Configuration is done through appSettings section of app.config.
Flog has only a single required configuration setting. If no file is given, Flog
is completely disabled, and no file accesses will be performed.
	flogFilename (required) - relative or absolute path to output file
	flogFlush (optional) - set to false to disable flush after each write

Flog has only two logging methods, which are intended to be a very light replacement
for Trace.Assert and Trace.WriteLine:
	Flog.Assert( bool Test, string FailMessage )
	Flog.WriteLine( string Message )

# Further Work
 - Optionally cause assert to throw, or wait to attach a debugger. 
 - Assert takes the result of an expression for simplicity. Unfortunately performance
 of the application is affected even when logging is disabled since the test is performed
 before Assert() checks if Flog is enabled.
 
# License
Flog is free software licensed under an MIT license. See LICENSE for details.
Copyright 2010 Dan Newcome.