using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RemoveAccentsExtension
/// </summary>
public static class RemoveAccentsExtension
{
	public static string RemoveAccents(this string txt)
	{
		byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
		return System.Text.Encoding.ASCII.GetString(bytes);
	}
}
