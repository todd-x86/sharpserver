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
	public class TestClass
	{
		public TestClass()
		{
		}
		
		[Route(Url="/cooldude")]
		public static void Cool(HttpListenerResponse resp)
		{
			Console.WriteLine("I'M COOL");
			StreamWriter w = new StreamWriter(resp.OutputStream);
			w.WriteLine("I'm 2cool4u!");
			w.Close();
		}
	}
}
