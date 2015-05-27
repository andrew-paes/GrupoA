using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for StripHtmlExtension
/// </summary>
public static class StripHtmlExtension
{
	public static string StripHTML(this string htmlString)
	{
		//This pattern Matches everything found inside html tags
		//(.|\n) - > Look for any character or a new line
		// *?  -> 0 or more occurences, and make a non-greedy search meaning
		//That the match will stop at the first available '>' it sees, and not at the last one
		//(if it stopped at the last one we could have overlooked
		//nested HTML tags inside a bigger HTML tag..)
		// Thanks to Oisin and Hugh Brown for helping on this one...

		string pattern = @"<(.|\n)*?>";

		if (!String.IsNullOrEmpty(htmlString))
		{
			return Regex.Replace(htmlString, pattern, string.Empty);
		}
		else
		{
			return string.Empty;
		}
	}
}
