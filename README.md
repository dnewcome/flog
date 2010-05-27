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
