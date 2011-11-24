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
	
	/**
	 * Flush is now configurable from code.. This was done for the test suite, not
	 * sure yet if this violates my principles of simplicity for Flog. Rest of 
	 * config should be set this way as well if I decide this is a good thing.
	 */ 
	public static bool Flush { get { return ms_flush; } set { ms_flush = value; } }

	private static bool ms_flush = true;
	private static bool ms_useConsole = false;
	
	/**
	* All initialization and configuration is done in the type initializer.
	* If initialization fails, Flog will be effectively disabled for the 
	* remainder of the process lifetime.
	*/
	static Flog() {
		try {
			string console = ConfigurationManager.AppSettings["useConsole"];
			if( !String.IsNullOrEmpty( console ) ) {
				bool useConsole = Convert.ToBoolean( console );
				if( useConsole == true ) {
					// if console is not available, WriteLine() should throw.
					// this keeps us from setting useConsole to true
					Console.WriteLine( "Flog set to log to console" );
					ms_useConsole = true;
				}
			}
		}
		catch {}
		
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

		// Flush logger when app exits
		System.AppDomain.CurrentDomain.ProcessExit += new EventHandler( OnProcessExit );
	}

	/**
	 * Flushes the logger when the parent process exits. Disabling flush during 
	 * execution speeds up logging ~2x. Handling process exit enables us to disable
	 * flush after each log event and still not lose data at the end if the buffer
	 * hasn't automatically flushed to disk.
	 */
	private static void OnProcessExit( object sender, EventArgs args ) {
		logger.Flush();
	}

	/**
	 * TODO: this assertion should be moved to separate assertion library.
	 * It is currently here for convenience.
	 */
	public static void Assert( bool condition, string message ) {
		if( condition == false ) {
			WriteLine( message );
		}
	}
		
	/**
	 * Main Log interface - provided as a convenience to avoid uneccessary 
	 * string concatenation if logging is disabled, and to formalize via the 
	 * method signature the various components desired in a logger message.
	 */
	public static void Log( string severity, string loggername, string message ) {
		if( ms_initialized == true || ms_useConsole ) { 
			WriteLine( severity + " " + loggername + " " + message );
		}
	}
	
	/**
	 * WriteLine is the core output method - all other log methods are wrappers
	 * around this one.
	 */
	public static void WriteLine( string message ) {
		if( ms_initialized == true ) {
			logger.WriteLine( DateTime.Now.ToString() + " - " + message );
			if( ms_flush == true ) {
				logger.Flush();
			}
		}
		// Warning: console is very slow, nearly 50x slower than file with 
		// auto-flush disabled
		if( ms_useConsole == true ) {
				Console.WriteLine( DateTime.Now.ToString() + " - " + message );
		}
	}
}
} // namespace