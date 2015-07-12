/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/4/2015
 * Time: 11:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

namespace SharpServer
{
	public delegate void RouteDelegate();
	
	/// <summary>
	/// Description of WebServer.
	/// </summary>
	public class WebServer
	{
		private HttpListener _listener;
		private IPAddress _host;
		private int _port;
		private Dictionary<string, RouteDelegate> _routeMap;
		
		public WebServer(IPAddress host, int port)
		{
			_routeMap = new Dictionary<string, RouteDelegate>();
			_listener = new HttpListener();
			_host = host;
			_port = port;
			foreach (Type t in Assembly.GetCallingAssembly().ManifestModule.GetTypes())
			{
				MemberInfo[] members = t.GetMembers();
				object inst = members[0];
				CheckMethods(inst, t.GetMethods());
			}
		}
		
		private void AddPrefix (string uri)
		{
			if (!uri.StartsWith("/")) uri = '/'+uri;
			if (!uri.EndsWith("/")) uri += '/';
			_listener.Prefixes.Add(string.Format("http://127.0.0.1:{1}{2}", _host, _port, uri));
		}
		
		private void CheckMethods(object inst, MethodInfo[] mthds)
		{
			foreach (MethodInfo m in mthds)
			{
				if (m.IsStatic)
					foreach (object obj in m.GetCustomAttributes(true))
						if (obj is RouteAttribute)
							AddRoute(obj as RouteAttribute, inst, m);
			}
		}
		
		public void AddRoute(RouteAttribute attr, object inst, MethodInfo m)
		{
			AddRoute(attr.Url, () => m.Invoke(inst, new object[]{}));
		}
		
		public void AddRoute(string url, RouteDelegate handler)
		{
			url = url.ToLower();
			AddPrefix(url);
			_routeMap[url] = handler;
		}
		
		public void Start()
		{
			_listener.Start();
			while (_listener.IsListening){
				HttpListenerContext ctx = _listener.GetContext();
				ctx.Response.ContentType = "text/html";
				StreamWriter w = new StreamWriter(ctx.Response.OutputStream);
				RouteDelegate handler;
				if(_routeMap.TryGetValue(ctx.Request.Url.LocalPath.ToLower(), out handler))
				{
					handler.Invoke();
				}
				w.WriteLine(string.Format("<marquee>Hello {0}</marquee>", ctx.Request.Url.LocalPath));
				w.Close();
				ctx.Response.Close();
			}
		}
		
		public void Stop()
		{
			_listener.Stop();
		}
	}
}
