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
	public delegate Response RouteDelegate(HttpListenerRequest req);
	
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
			_listener.Prefixes.Add(string.Format("http://*:{0}/", port));
			_host = host;
			_port = port;
		}
		
		public void Register(Type t)
		{
			// Add routes
			object inst = t.GetMembers()[0];
			foreach (MethodInfo m in t.GetMethods())
			{
				if (!m.IsStatic) continue;
				foreach (object obj in m.GetCustomAttributes(true))
					if (obj is RouteAttribute)
						AddRoute(new Route(obj as RouteAttribute, inst, m));
			}
		}
		
		public void AddFile(string path, string uri = null)
		{
			AddRoute(uri ?? path, FileResponse.BuildHandler(path));
		}
		
		private void AddRoute(Route route)
		{
			AddRoute(route.Uri, route.Handler);
		}
		
		public void AddRoute(string url, RouteDelegate handler)
		{
			_routeMap[NormalizeUri(url)] = handler;
		}
		
		public void Start(bool threadBlocking = true)
		{
			// TODO: Multithreading capabilities
			_listener.Start();
			if (threadBlocking) {
				while (_listener.IsListening){
					HandleContext(_listener.GetContext());
				}
			} else {
				_listener.BeginGetContext(ReceiveContext, null);
			}
		}
		
		private void ReceiveContext(IAsyncResult result)
		{
			HttpListenerContext ctx = _listener.EndGetContext(result);
			HandleContext(ctx);
		}
		
		private Response GetResponse (int httpCode, string msg)
		{
			Response r = new StringResponse(string.Format("{0} {1}", httpCode, msg));
			r.Code = httpCode;
			return r;
		}
		
		private string NormalizeUri (string uri)
		{
			uri = uri.ToLower();
			if (!uri.StartsWith("/")) uri = '/'+uri;
			if (!uri.EndsWith("/")) uri += '/';
			return uri;
		}
		
		private void HandleContext(HttpListenerContext ctx)
		{
			RouteDelegate handler;
			Response rsp;
			if(_routeMap.TryGetValue(NormalizeUri(ctx.Request.Url.LocalPath), out handler))
			{
				rsp = handler.Invoke(ctx.Request);
			} else {
				rsp = GetResponse(404, "Not Found");
			}
			rsp.WriteTo(ctx.Response);
			try {
				ctx.Response.Close();
			} catch (HttpListenerException e) {}
		}
		
		public void Stop()
		{
			_listener.Stop();
		}
	}
}
