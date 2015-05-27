using System.Text.RegularExpressions;
using System;
using System.Globalization;

public class FTPLineParser
{
    private Regex unixStyle = new Regex(@"^(?<dir>[-dl])(?<ownerSec>[-r][-w][-x])(?<groupSec>[-r][-w][-x])(?<everyoneSec>[-r][-w][-x])\s+(?:\d)\s+(?<owner>\w+)\s+(?<group>\w+)\s+(?<size>\d+)\s+(?<month>\w+)\s+(?<day>\d{1,2})\s+(?<hour>\d{1,2}):(?<minutes>\d{1,2})\s+(?<name>.*)$");
    private Regex winStyle = new Regex(@"^(?<month>\d{1,2})-(?<day>\d{1,2})-(?<year>\d{1,2})\s+(?<hour>\d{1,2}):(?<minutes>\d{1,2})(?<ampm>am|pm)\s+(?<dir>[<]dir[>])?\s+(?<size>\d+)?\s+(?<name>.*)$");

    public FTPLineResult Parse(string line)
    {
        Match match = unixStyle.Match(line);

        if (match.Success)
        {
            return ParseMatch(match.Groups, ListStyle.Unix);
        }

        match = winStyle.Match(line);

        if (match.Success)
        {
            return ParseMatch(match.Groups, ListStyle.Unix);
        }

        throw new Exception("Invalid line format");
    }

    private FTPLineResult ParseMatch(GroupCollection matchGroups, ListStyle style)
    {

        string dirMatch = (style == ListStyle.Unix ? "d" : "<dir>");
        FTPLineResult result = new FTPLineResult();
        result.Style = style;
        result.IsDirectory = matchGroups["dir"].Value.Equals(dirMatch, StringComparison.InvariantCultureIgnoreCase);
        result.Name = matchGroups["name"].Value;
        string day = matchGroups["day"].Value;
        string month = matchGroups["month"].Value;
        string year = !String.IsNullOrEmpty(matchGroups["year"].Value) ? matchGroups["year"].Value : DateTime.Now.Year.ToString();
        string hour = matchGroups["hour"].Value;
        string minutes = matchGroups["minutes"].Value;
        string date = String.Concat(year, "-", month, "-", day, " ", hour, ":", minutes);
        CultureInfo ci = new CultureInfo("en-US");

        result.DateTime = Convert.ToDateTime(date, ci);

        if (!result.IsDirectory)
        {
            result.Size = long.Parse(matchGroups["size"].Value);
        }
        return result;
    }
}

public enum ListStyle
{
    Unix,
    Windows
}

public class FTPLineResult
{
    public ListStyle Style;
    public string Name;
    public DateTime DateTime;
    public bool IsDirectory;
    public long Size;
}