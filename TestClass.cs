/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/9/2015
 * Time: 11:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;

namespace SharpServer
{
	/// <summary>
	/// Description of TestClass.
	/// </summary>
	public class TestClass: Controller
	{
		[Route(Url="/cooldude")]
		public static Response Cool(HttpListenerRequest req)
		{
			return AsString("I'm 2cool4u!");
		}
		
		[Route(Url="/")]
		public static Response Home(HttpListenerRequest req)
		{
			return AsString("Welcome <a href=\"/login\">Login</a>!");
		}
		
		[Route(Url="/login")]
		public static Response Login(HttpListenerRequest req)
		{
			return AsString("CANNOT LOGIN!!!!!!1!");
		}
		
		[Route(Url="/test")]
		public static Response SomeFile(HttpListenerRequest req)
		{
			return AsFile("testfile.html");
		}
	}
}
