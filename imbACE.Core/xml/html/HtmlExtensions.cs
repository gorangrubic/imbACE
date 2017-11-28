namespace imbACE.Core.xml.html
{
    using HtmlAgilityPack;
    using imbSCI.Core.reporting.render;
    using imbSCI.Data.enums.reporting;
    using imbSCI.Core.extensions.text;
    #region imbVeles using

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// 2014 DEC> razna proširenja vezana za HtmlNode (HtmlAgilityPack)
    /// </summary>
    public static class HtmlExtensions
    {
        /*
        public static String getTextContent(this HtmlNode htmlNode)
        {
            XPathNodeIterator itr = htmlNode
                .SelectDescendants(XPathNodeType.Text, true);

            String inner = itr.Current.Value;
            if (!String.IsNullOrEmpty(inner))
            {
                var subNav = itr.Current.CreateNavigator();

                if (subNav.MoveToParent())
                {
                    if (subNav.checkNode(settings))
                        output.AppendLine(deploySpacing(inner, subNav, settings));
                }
                else
                {
                    if (subNav.checkNode(settings)) output.AppendLine(inner);
                }
            }
        }*/

        public static String getFullXPath(this HtmlNode node)
        {
            String output = node.XPath;

            HtmlNode parent = node.ParentNode;
            while (parent != null)
            {
                output = parent.XPath + output;
                parent = node.ParentNode;
            }
            return output;
        }


        /// <summary>
        /// Ubacuje opis HtmlNode-a u izvestaj prema ugradjenom sablonu
        /// </summary>
        /// <param name="node">HtmlNode objekat koji se opisuje</param>
        /// <param name="report">Izvestaj u kome se opisuje</param>
        /// <param name="isExpandedMode">Da li je rec o prosirenom ili skracenom izvestaju</param>
        public static void htmlNodeReport(this HtmlNode node, ITextRender report, Boolean isExpandedMode)
        {
            report.open(nameof(htmlTagName.div));
            Int32 childs = node.ChildNodes.Count;

            //report.AppendPairs(node, node.XPath + " " + node.Name + " (ch:" + childs + ")", "NodeType");
            report.AppendPair("Inner text", node.InnerText.toWidthMaximum(100));
            report.AppendPair("Inner HTML", node.InnerHtml.toWidthMaximum(100));
            report.close();
        }


        public static Boolean isAcceptableText(this HtmlNode htmlNode)
        {
            // htmlNode.CreateNavigator().retriveText(new textRetriveSetup());
            //  if (htmlNode.ChildNodes.Any()) return true;
            return !htmlNode.isEmptyText();
        }

        public static Boolean isEmptyText(this HtmlNode htmlNode)
        {
            String content = htmlNode.InnerText.ToString();
            String sl = content.Trim(new Char[] {'\n', '\r', '\t', ' '});
            return String.IsNullOrEmpty(sl);
        }

        public static Boolean isWithTextOnly(this HtmlNode htmlNode)
        {
            if (htmlNode.ChildNodes.Any())
            {
                if (htmlNode.FirstChild.NodeType == HtmlNodeType.Text)
                {
                    return true;
                }
            }
            return false;
        }
    }
}