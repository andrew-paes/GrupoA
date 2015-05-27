using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ReplaceHtmlExtension
/// </summary>
public static class ReplaceHtmlExtension
{
	public static string ReplaceHtml(this string html)
	{
		if (!String.IsNullOrEmpty(html))
		{
			return HttpContext.Current.Server.HtmlDecode(html)
				.Replace("<ol>", "<ol class=\"fckList\">")
				.Replace("<ul>", "<ul class=\"fckList\">")
				.Replace("<em>", "<em class=\"fckInline\">")
				.Replace("<strong>", "<strong class=\"fckInline\">");
		}
		return html;
	}
}
