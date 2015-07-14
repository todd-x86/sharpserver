/*
 * Created by SharpDevelop.
 * User: Todd Suess
 * Date: 7/8/2015
 * Time: 5:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpServer
{
	/// <summary>
	/// Description of RouteAttribute.
	/// </summary>
	public class RouteAttribute: Attribute
	{
		public string Url { get; set; }
		
		public override string ToString()
		{
			return string.Format("[RouteAttribute Url={0}]", Url);
		}
	}
}
