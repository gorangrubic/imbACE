namespace imbACE.Core.xml
{
    #region imbVeles using

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.XPath;
    using imbACE.Core.xml.html;
    using HtmlAgilityPack;
    using imbSCI.Core.extensions.text;
    using imbSCI.Core.extensions.data;

    #endregion

    /// <summary>
    /// 2013a> napredne operacije za XML-HTML divergenciju
    /// OVDE doradjivati kada postoje problemi sa XML importovanjem
    /// </summary>
    public static class imbXmlHtmlReparation
    {
        /// <summary>
        /// Univerzalna komanda za uklanjanje child nodea
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Boolean removeNode(this XPathNavigator target, XPathNavigator parent = null,
                                         Boolean disableParent = false)
        {
            if (!disableParent)
            {
                if (parent == null)
                {
                    XPathNavigator tmp = target.Clone();
                    if (target.MoveToParent())
                    {
                        parent = target.Clone();
                    }
                    target = tmp;
                }

                if (parent == null) return false;
            }


            String typeName = parent.GetType().Name;

            switch (typeName)
            {
                case "HtmlNodeNavigator":
                    HtmlNodeNavigator htmlNav = target as HtmlNodeNavigator;
                    htmlNav.CurrentNode.Remove();
                    return true;
                    /*
                    var htmlParent = htmlNav.CurrentNode; //parent.TypedValue as HtmlNode;
                   if (htmlParent != null)
                   {
                       htmlNav = target as HtmlNodeNavigator;
                       htmlNav.CurrentNode.Remove
                       var htmlTarget = target. as HtmlNode;

                       htmlParent.RemoveChild(htmlTarget);
                       return true;
                   }*/
                    break;
                case "XPathNavigator":
                case "XmlNodeNavigator":
                    // target.
                    // XmlNodeNavigator xmlNav = target as XmlNodeNavigator;
                    /// OVDE TREBA UBACITI PODRSKU DA SE BRISE TRENUTNI NODE
                    var xmlParent = parent.TypedValue as XmlNode;

                    if (xmlParent != null)
                    {
                        var xmlTarget = target.TypedValue as XmlNode;

                        xmlParent.RemoveChild(xmlTarget);
                        return true;
                    }
                    break;
                default:

                    break;
            }


            return false;
        }

        /// <summary>
        /// Klonira sadrzaj prema filteru
        /// </summary>
        /// <param name="sourceParent"></param>
        /// <param name="scopedTags">Tagovi koji mogu da se nadju u kolekciji</param>
        /// <param name="cleanAttribs">Malim slovima se radi poredjenje - </param>
        /// <param name="targetParent"></param>
        /// <param name="nsPrefix"></param>
        /// <returns></returns>
        public static IXPathNavigable filterCloning(this XPathNavigator sourceParent, preprocessCache query,
                                                    List<String> cleanAttribs, Int32 limit = -1,
                                                    Action<XPathNavigator> nodeAction = null)
        {
            // sourceParent.UnderlyingObject.GetType();

            IXPathNavigable output;
            // var outputNav = 
            //XmlDocument output = new XmlDocument(sourceParent.NameTable);
            //Type sourceType = sourceParent.GetType();
            //output = Activator.CreateInstance(sourceType) as IXPathNavigable;

            output = sourceParent.Clone();

            if (output == null)
            {
            }


            XPathNavigator targetParent = output.CreateNavigator();


            List<XPathNavigator> nextLoop = new List<XPathNavigator>();
            List<XPathNavigator> thisLoop = new List<XPathNavigator>();
            List<XPathNavigator> toDelete = new System.Collections.Generic.List<XPathNavigator>();
            thisLoop.Add(targetParent);

            Int32 c = 0;
            while (thisLoop.Count > 0)
            {
                nextLoop = new List<XPathNavigator>();
                foreach (XPathNavigator tp_xn in thisLoop)
                {
                    XPathNodeIterator xItr = tp_xn.SelectChildren(XPathNodeType.Element);
                    while (xItr.MoveNext())
                    {
                        var nxn = xItr.Current;

                        String tn_xn = nxn.Name.ToLower();

                        if (query.toRemove.Contains(tn_xn))
                        {
                            toDelete.Add(nxn);
                            //nxn.OuterXml = "";
                            //nxn.DeleteSelf();

                            // nxn.DeleteSelf();
                        }
                        else
                        {
                            if (cleanAttribs.Contains(tn_xn))
                            {
                                var aItr = nxn.SelectChildren(XPathNodeType.Attribute);
                                while (aItr.MoveNext())
                                {
                                    aItr.Current.DeleteSelf();
                                }
                            }
                            nextLoop.Add(nxn.Clone());
                            if (nodeAction != null) nodeAction(nxn);
                        }
                        //c++;
                    }

                    foreach (var nd in toDelete)
                    {
                        nd.removeNode(tp_xn, true);
                    }
                    toDelete.Clear();
                }
                c += nextLoop.Count;
                if (limit > 0)
                {
                    if (c < limit) thisLoop = nextLoop;
                }
                else
                {
                    thisLoop = nextLoop;
                }
            }
            return output;
        }

        /// <summary>
        /// 2013a> sredjuje XML koji je ucitan
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public static String repairXmlCode(String sourceCode, String rootElement = "span")
        {
            List<String> lines = sourceCode.getStringLineList(StringSplitOptions.RemoveEmptyEntries);
            String xmlCode = "";

            String prvaLinija = "";

            if (lines.Count == 0)
            {
                prvaLinija = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            }
            else
            {
                prvaLinija = lines[0];
            }


            /*
            lines.Insert(1, "<" + xmlRootElement + ">");

            lines.Add("</" + xmlRootElement + ">");
            */

            xmlCode = lines.toCsvInLine(Environment.NewLine);
            return xmlCode;
        }

        /// <summary>
        /// 2013a> konvertuje HTML dokument u XML
        /// </summary>
        /// <param name="input"></para>
        /// <returns></returns>
        public static XmlDocument HtmlDocumentToXml(HtmlDocument input)
        {
            repairHtmlDocument(input);

            XmlDocument output = new XmlDocument();
            String xmlCode = "";
            StringWriter sw = new StringWriter();
            input.Save(sw);

            xmlCode = repairXmlCode(sw.ToString());

            output.LoadXml(xmlCode);
            return output;
        }


        /// <summary>
        /// Proverava sve nodove u dokumentu
        /// </summary>
        /// <param name="input"></param>
        public static void repairHtmlDocument(HtmlDocument input)
        {
            List<HtmlNode> htmlNodes = new System.Collections.Generic.List<HtmlNode>();
            htmlNodes.AddRange(input.DocumentNode.DescendantNodesAndSelf());

            foreach (HtmlNode node in htmlNodes)
            {
                cleanNode(node);
            }
        }

        /// <summary>
        /// Cisti probleme u pojedinacnom nodeu
        /// </summary>
        /// <param name="node"></param>
        public static void cleanNode(HtmlNode node)
        {
            List<HtmlAttribute> htmlAttributes = new System.Collections.Generic.List<HtmlAttribute>();
            htmlAttributes.AddRange(node.Attributes);

            foreach (HtmlAttribute att in htmlAttributes)
            {
                Char nameStart = att.Name[0];
                if (!Char.IsLetter(nameStart))
                {
                    node.Attributes[att.Name].Name = "fix" + att.Name;
                }
            }
        }
    }
}