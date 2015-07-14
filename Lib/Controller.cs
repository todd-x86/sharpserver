/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/13/2015
 * Time: 12:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpServer
{
	/// <summary>
	/// Description of Controller.
	/// </summary>
	public class Controller
	{
		protected static StringResponse AsString(string text)
		{
			return new StringResponse(text);
		}
		
		protected static FileResponse AsFile(string filename)
		{
			return new FileResponse(filename);
		}
	}
}
