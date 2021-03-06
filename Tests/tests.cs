using System;
using System.Threading;
using Djn;

public class Program 
{
	public static void Main() {
		PerfTest();
		ThreadTest();
	}

	private static void PerfTest() {
		Flog.WriteLine( "trace enabled" );
		Flog.Assert( true, "assertion passed" );
		Flog.Assert( false, "assertion failed" );
		
		Flog.Log( "WARN", "Program.Main()", "this is a warning message" );

		// Perf( "Testing logging performance" ); // 15ms log to file, flush enabled
		Perf( "x" ); // 11ms logging to file flush enabled
		Flog.Flush = false;

		Flog.WriteLine( "disabling flush" );
		Perf( "x" ); // 6ms logging to file flush disabled
	}

	private static void ThreadTest() {
		Thread thread1 = new Thread(new ThreadStart(Worker) );
		thread1.Start();

		Thread thread2 = new Thread(new ThreadStart(Worker) );
		thread2.Start();

		Thread thread3 = new Thread(new ThreadStart(Worker) );
		thread3.Start();


		thread1.Join();
		thread2.Join();
		thread3.Join();
	}
	
	private static void Worker() {
		Perf( "INFO - ThreadTest - " + Thread.CurrentThread.ManagedThreadId );
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
