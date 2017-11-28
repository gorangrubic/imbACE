namespace imbACE.Core.xml.html
{
    #region imbVeles using

    using System.Collections.Generic;
    using System.Xml.XPath;
    using imbACE.Core.xml.query;
    using imbSCI.Core.extensions.data;
    using imbSCI.Core.reporting;

    #endregion

    /// <summary>
    /// Klasa sa kesiranim podesavanjima pred-processinga sadrzaja
    /// </summary>
    public class preprocessCache : xPathQueryCache
    {
        public List<string> toCollapse = new List<string>();
        public List<string> toRemove = new List<string>();
        public List<string> toRemoveAttributes = new List<string>();


        public preprocessCache(preprocessSettings settings, IXPathNavigable sourceParent)
            : base("", sourceParent)
        {
            if (settings.doStripScriptTags) toRemove.AddUnique(htmlDefinitions.HTMLTag_Script);
            if (settings.doStripStyleTags) toRemove.AddUnique(htmlDefinitions.HTMLTag_Style);

            if (settings.doCollapseInlineSemanticTags)
                toRemove.AddRange(htmlDefinitions.HTMLTags_textSemanticTags);

            toRemove.AddRange(settings.nodesToStrip);
            toRemoveAttributes.AddRange(settings.nodesToCleanAttributes);

            XPath = toRemove.makeXPathForAllNodes(false, nsPrefix, true, true);
        }
    }
}