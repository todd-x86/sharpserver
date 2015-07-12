/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/4/2015
 * Time: 11:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace SharpServer
{
	class Program
	{
		public static void Main(string[] args)
		{
			WebServer ws = new WebServer(IPAddress.Parse("0.0.0.0"), 8090);
			ws.Start();
			Console.WriteLine("Listening");
		}
		
		[Route(Url="/login")]
		public static void Login()
		{
			Console.WriteLine("LOGIN!");
		}
	}
}