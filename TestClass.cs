/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/9/2015
 * Time: 11:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

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
		public static void Cool()
		{
			Console.WriteLine("I'M COOL");
		}
	}
}
