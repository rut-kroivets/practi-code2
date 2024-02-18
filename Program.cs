// See https://aka.ms/new-console-template for more information

using Html_Serializer;
using System;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

var html = await Load("https://learn.malkabruk.co.il/");
string[] htmlLines = new Regex("<(.*?)>").Split(html)
    .Select(line => Regex.Replace(line, @"\s+", " "))
    .Where(x => x.Length > 1)
    .ToArray();

var htmlElement = "<div id=\"my-id\" class=\"my-class-1 my-class-2\" width=\"100%\">text</div>";
HtmlSerializer serializer = new HtmlSerializer();

HtmlElement root = serializer.Serializer(htmlLines);
//var r=root.Descendants().ToList();
//"meta"
//
var result = root.FindElementsBySelector(Selector.ParseQuery("body div.home-hero"));
result.ToList().ForEach(e => Console.WriteLine(ToString(e)));
Console.ReadLine();

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}


string ToString(HtmlElement e)
{
    string res="<";
    if (e.Name != null)
        res += e.Name;
    if (e.Id != null)
        res += " id=" + e.Id;
    if (e.Classes.Count()>0)
    {
        res += " class=";
        e.Classes.ForEach(c => res += " " + c);
    }
    res += ">";
    return res;
}



