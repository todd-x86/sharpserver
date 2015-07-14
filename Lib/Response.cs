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
		public int Code { get; set; }
		public string ContentType { get; set; }
		
		public Response ()
		{
			Code = 200;
			ContentType = "text/html";
		}
		
		public virtual void WriteTo(HttpListenerResponse resp)
		{
			resp.ContentType = ContentType;
			resp.StatusCode = Code;
		}
	}
}
