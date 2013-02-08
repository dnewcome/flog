using System;
using System.Threading;
using log4net;
using log4net.Config;
using log4net.Appender;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension="log4net",Watch=true)]
public class Program {
	private static readonly ILog logger = LogManager.GetLogger("speedtest");
	public static void Main() {
		logger.Info("working");
		// Perf( "x" );
		ThreadTest();
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
			logger.Info(msg);
		}
		sw.Stop();
		logger.Info( "Elapsed time " + sw.ElapsedMilliseconds );
	}
}
