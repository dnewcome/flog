using System;
using Djn;

public class Program 
{
	public static void Main() {
		Flog.WriteLine( "trace enabled" );
		Flog.Assert( true, "assertion passed" );
		Flog.Assert( false, "assertion failed" );
		
		Flog.Log( "WARN", "Program.Main()", "this is a warning message" );

		// Perf( "Testing logging performance" ); // 15ms log to file, flush enabled
		Perf( "x" ); // 11ms logging to file flush enabled
		Flog.Flush = false;

		Flog.WriteLine( "disabling flush" );
		Perf( "x" ); // 6ms logging to file flush disabled

		Console.ReadLine();
	}

	public static void Perf( string msg ) {
		System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		for( int i = 0; i < 1000; i++ ) {
			Flog.WriteLine( msg );
		}
		sw.Stop();
		Flog.WriteLine( "Elapsed time " + sw.ElapsedMilliseconds );
	}
}
