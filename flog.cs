/**
* Flog is a super-simple threadsafe .NET trace/assert tool, whose output
*	is directed to a plain text log file. Flog is free software provided 
*	under the MIT license.
*
*	See LICENSE file for full text of the license.
*	Copyright 2010 Dan Newcome.
*/

using System;
using System.IO;
using System.Configuration;

/**
* Specify log file in app.config otherwise Flog will do nothing. 
* Primary goal is to make this safe so that it should never throw 
* an exception or cause the host program to fail.
*
* Configuration is done through appSettings section of app.config.
* Flog has only a single required configuration setting.
*	flogFilename (required) - relative or absolute path to output file
*	flogFlush (optional) - set to false to disable flush after each write
*
* Possible later extensions - cause assert to throw, or wait
* to attach a debugger. 
*/
namespace Djn {
public class Flog
{
	private static TextWriter logger;
	private static bool ms_initialized = false;
	private static bool ms_flush = true;
	
	/**
	* All initialization and configuration is done in the type initializer.
	* If initialization fails, Flog will be effectively disabled for the 
	* remainder of the process lifetime.
	*/
	static Flog() {
		try {
			string filename = ConfigurationManager.AppSettings["flogFilename"];
			if( !String.IsNullOrEmpty( filename ) ) {
				logger = TextWriter.Synchronized( 
					new StreamWriter( filename, true ) 
				);
				ms_initialized = true;
				
				WriteLine( "---------" );
				WriteLine( "Flog initialized, writing to " + filename );
			}
		} catch {}
		
		try {
			string flush = ConfigurationManager.AppSettings["flogFlush"];
			if( !String.IsNullOrEmpty( flush ) ) {
				ms_flush = Convert.ToBoolean( flush );
				WriteLine( "Flog flush set to " + flush.ToString() + " via app.config" );
			}
		} catch {}
	}
	
	public static void Assert( bool condition, string message ) {
		if( condition == false ) {
			WriteLine( message );
		}
	}
		
	public static void WriteLine( string message ) {
		if( ms_initialized == true ) {
			logger.WriteLine( DateTime.Now.ToString() + " - " + message );
			if( ms_flush == true ) {
				logger.Flush();
			}
		}
	}
}
} // namespace