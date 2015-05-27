using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GrupoA.BusinessLogicalLayer.Helper
{
    public class RssHelper
    {
        /// <summary>
        /// Get a generic MRSS document.
        /// </summary>
        /// <param name="titleOfDocument">The title of MRSS document.</param>
        /// <param name="mrssItems">A collection of MRRS items.</param>
        /// <param name="url">The application URL.</param>
        /// <returns>XML document in MRSS format.</returns>
        public XDocument GetMrss(string titleOfDocument, string url, IEnumerable<MrssItem> mrssItems)
        {
            XNamespace namespaceMrss = "http://search.yahoo.com/mrss";
            XNamespace namespaceAtom = "http://www.w3.org/2005/Atom";

            XDocument rss =
                new XDocument(
                    new XElement("rss", new XAttribute("version", "2.0"), new XAttribute(XNamespace.Xmlns + "media", namespaceMrss), new XAttribute(XNamespace.Xmlns + "atom", namespaceAtom),
                        new XElement("channel",
                            new XElement("title", titleOfDocument),
                            new XElement("description", "aaaaaa"),
                            new XElement("link", url),
                            new XElement("pubDate", DateTime.Now.ToString("r")),
                            new XElement("generator", "XLinq"),
                        from mrssItem in mrssItems
                        select new XElement("item",
                                   new XElement("title", mrssItem.Title),
                                   new XElement(XName.Get("description", namespaceMrss.NamespaceName),
                                   new XCData(String.Format("<a href='/Details.aspx?vid={1}'> <img src='/Images/ThumbnailHandler.ashx?vid={1}' align='left' width='120' height='90' style='border: 2px solid #B9D3FE;'/></a><p>{0}</p>", "", mrssItem.Description))),
                                   new XElement("link",
                                       new XAttribute("rel", "enclosure"),
                                       new XAttribute("href", String.Format("/Details.aspx?vid={0}", mrssItem.ItemId))),
                                   new XElement(XName.Get("thumbnail", namespaceMrss.NamespaceName), "",
                                        new XAttribute("url", String.Format("{0}", mrssItem.ThumbnailUrl))),
                                   new XElement(XName.Get("content", namespaceMrss.NamespaceName), "a",
                                        new XAttribute("isDefault", String.Format("{0}", false)),
                                        new XAttribute("width", String.Format("{0}", 690)),
                                        new XAttribute("height", String.Format("{0}", 430)),
                                        new XAttribute("url", String.Format("{0}", mrssItem.ContentUrl)),
                                        new XAttribute("type", String.Format("{0}", "text/html"))
                                    )
                                )
                            ))
                    );

            return rss;
        }
    }

    /// <summary>
    /// Abstraction of MRSS items.
    /// </summary>
    [Serializable]
    public class MrssItem
    {
        private string _title;
        private string _description;
        private string _link;
        private string _thumbnailUrl;
        private string _contentUrl;
        private string _itemId;

        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public string Title
        {

            get { return _title; }

            set { _title = value; }
        }

        public string Description
        {

            get { return _description; }

            set { _description = value; }

        }

        public string Link
        {

            get { return _link; }

            set { _link = value; }

        }

        public string ThumbnailUrl
        {

            get { return _thumbnailUrl; }

            set { _thumbnailUrl = value; }

        }

        public string ContentUrl
        {

            get { return _contentUrl; }

            set { _contentUrl = value; }

        }

    }





}
