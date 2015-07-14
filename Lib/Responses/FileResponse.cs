/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/13/2015
 * Time: 5:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;

namespace SharpServer
{
	/// <summary>
	/// Description of FileResponse.
	/// </summary>
	public class FileResponse: Response
	{
		private string _file;
		
		public FileResponse(string file)
		{
			_file = file;
		}
		
		public override void WriteTo(HttpListenerResponse resp)
		{
			base.WriteTo(resp);
			FileStream fs = new FileStream(_file, FileMode.Open);
			fs.CopyTo(resp.OutputStream);
		}
	}
}
