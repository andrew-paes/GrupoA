using System;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for GenerateResumeExtension
/// </summary>
public static class GenerateResumeExtension
{
	/// <summary>
	/// Generate a resume of strings
	/// </summary>
	/// <param name="str"></param>
	/// <param name="length">Max legth size of the resume</param>
	/// <returns></returns>
	public static string GenerateResume(this string str, int length)
	{
		//check if the string is not null, empty or whitespaces
		if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(str.Trim()))
		{
			str = HttpContext.Current.Server.HtmlDecode(str).StripHTML();
			//if the length of string is smaller than max length, return the same string
			if (str.Length <= length)
			{
				return str;
			}
			else
			{
				//split the string 
				string[] words = HttpContext.Current.Server.HtmlDecode(str).StripHTML().Split(" ".ToCharArray());

				if (words.Length > 0)
				{
					StringBuilder strResult = new StringBuilder();
					//iterates over each word in string
					foreach (string s in words)
					{
						//check the length of result string and concatenate the new word
						if (String.Concat(strResult.ToString(), " ", s).Length < length)
						{
							strResult.Append(String.Concat(" ", s));
						}
						else
						{
							if (!String.IsNullOrEmpty(strResult.ToString()))
							{
								//return the string result
								return String.Concat(strResult.ToString(), "...").Trim();
							}
							else
							{
								//if the word is greater than max length, returns the substring
								return String.Concat(s.Substring(0, length), "...").Trim();
							}
						}
					}
				}
			}
		}
		return string.Empty;
	}
}
