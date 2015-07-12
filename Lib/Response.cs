/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/12/2015
 * Time: 9:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace SharpServer
{
	/// <summary>
	/// Description of Response.
	/// </summary>
	public abstract class Response
	{
		public Response()
		{
		}
		
		public abstract void WriteTo(HttpListenerResponse resp);
	}
}
