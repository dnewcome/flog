using System;
using Djn;

public class Program 
{
	public static void Main() {
		Flog.WriteLine( "trace enabled" );
		Flog.Assert( true, "assertion passed" );
		Flog.Assert( false, "assertion failed" );
		
		Flog.Log( "WARN", "Program.Main()", "this is a warning message" );
		Console.ReadLine();
	}
}
