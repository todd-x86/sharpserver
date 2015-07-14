/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/12/2015
 * Time: 1:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;

namespace SharpServer
{
	/// <summary>
	/// Description of StringResponse.
	/// </summary>
	public class StringResponse: Response
	{
		public string Text { get; set; }
		
		public StringResponse(string text)
		{
			Text = text;
		}
		
		public override void WriteTo (HttpListenerResponse r)
		{
			base.WriteTo(r);
			r.ContentType = "text/html";
			StreamWriter w = new StreamWriter(r.OutputStream);
			w.Write(Text);
			w.Close();
		}
	}
}
