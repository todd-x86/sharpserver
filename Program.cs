/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/4/2015
 * Time: 11:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;

namespace SharpServer
{
	class Program
	{
		public static void Main(string[] args)
		{
			WebServer ws = new WebServer(IPAddress.Parse("0.0.0.0"), 8090);
			Console.WriteLine("Listening");
			ws.Start();
		}
		
		[Route(Url="/login")]
		public static Response Login(HttpListenerRequest req)
		{
			Console.WriteLine("LOGIN!");
			return new StringResponse("Hello THERE!");
		}
	}
}