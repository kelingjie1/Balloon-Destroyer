using UnityEngine;
using System.Collections;
using System.Xml;
public static class XmlElementExtension
{
    public static XmlElement GetFirstElementByTag(this XmlElement ele, string tagname)
    {
        return (XmlElement)ele.GetElementsByTagName(tagname).Item(0);
    }
    public static string GetFirstTextByTag(this XmlElement ele, string tagname)
    {
        return ele.GetFirstElementByTag(tagname).InnerText;
    }
}
