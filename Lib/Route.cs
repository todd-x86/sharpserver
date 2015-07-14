/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/13/2015
 * Time: 6:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;

namespace SharpServer
{
	/// <summary>
	/// Description of Route.
	/// </summary>
	public class Route
	{
		public string Uri { get; private set; }
		public RouteDelegate Handler { get; private set; }
		
		public Route(string uri, RouteDelegate handler)
		{
			Uri = uri;
			Handler = handler;
		}
		
		public Route(RouteAttribute attr, object instance, MethodInfo method)
		{
			Uri = attr.Url;
			Handler = (req) => (Response)method.Invoke(instance, new object[]{req});
		}
	}
}
