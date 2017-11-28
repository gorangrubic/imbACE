namespace imbACE.Core.xml.html
{
    using imbACE.Core.core;
    using imbSCI.Data.data;
    #region imbVeles using

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Xml.XPath;

    // using imbReportingCore.reporting.process;

    #endregion

    public class preprocessSettings : imbBindable
    {
        public static Int32 developingLimit = 20000;

        public preprocessSettings()
        {
        }

        #region KESIRANI UPIT

        private preprocessCache _cache;
        private PropertyChangedEventHandler _cache_handler;

        public preprocessCache get_cache(IXPathNavigable __source)
        {
            if (_cache_handler == null)
            {
                _cache_handler = new PropertyChangedEventHandler(_cache_updateCache);
                PropertyChanged += _cache_handler;
            }

            if (_cache == null) _cache = new preprocessCache(this, __source);

            return _cache;
        }

        private void _cache_updateCache(object sender, PropertyChangedEventArgs e)
        {
            _cache = null;
        }

        #endregion

        #region CONTENT TEXT PROCESSING

        private Boolean _doRemoveHtmlEntities = true;

        private Boolean _doTransliterateToLat = true;

        #region imbObject Property <Boolean> doTransliterateToLat

        /// <summary>
        /// Da izvrsi transliteraciju na latinicu
        /// </summary>
        [Category("HTML Postprocessing")]
        [DisplayName("Transliterate to Latin")]
        [Description("If True phaser will make transliteration of all Cyrilic characherts into Latin")]
        public Boolean doTransliterateToLat
        {
            get { return _doTransliterateToLat; }
            set
            {
                _doTransliterateToLat = value;
                OnPropertyChanged("doTransliterateToLat");
            }
        }

        #endregion

        #region imbObject Property <Boolean> doRemoveHtmlEntities

        /// <summary>
        /// Da skloni HTML entitie
        /// </summary>
        [Category("HTML Postprocessing")]
        [DisplayName("Remove HTML Entities")]
        [Description("If True phaser will remove all HTML entities after loading HTML")]
        public Boolean doRemoveHtmlEntities
        {
            get { return _doRemoveHtmlEntities; }
            set
            {
                _doRemoveHtmlEntities = value;
                OnPropertyChanged("doRemoveHtmlEntities");
            }
        }

        #endregion

        #endregion

        #region --- nodesToStrip ------- xmlNodes to strip

        private List<String> _nodesToStrip = new List<string>();

        /// <summary>
        /// xmlNodes to strip
        /// </summary>
        public List<String> nodesToStrip
        {
            get { return _nodesToStrip; }
            set
            {
                _nodesToStrip = value;
                OnPropertyChanged("nodesToStrip");
            }
        }

        #endregion

        #region --- nodesToCleanAttributes ------- Nodovi kojima treba potpuno skinuti atribute

        private List<String> _nodesToCleanAttributes = new List<string>();

        /// <summary>
        /// Nodovi kojima treba potpuno skinuti atribute
        /// </summary>
        public List<String> nodesToCleanAttributes
        {
            get { return _nodesToCleanAttributes; }
            set
            {
                _nodesToCleanAttributes = value;
                OnPropertyChanged("nodesToCleanAttributes");
            }
        }

        #endregion

        #region TAG STRIPPING

        #region ----------- Boolean [ doStripScriptTags ] -------  [Da li skloni sve script tagove?]

        private Boolean _doStripScriptTags = true;

        /// <summary>
        /// Da li skloni sve script tagove?
        /// </summary>
        [Category("Switches")]
        [DisplayName("doStripScriptTags")]
        [Description("Da li skloni sve script tagove?")]
        public Boolean doStripScriptTags
        {
            get { return _doStripScriptTags; }
            set
            {
                _doStripScriptTags = value;
                OnPropertyChanged("doStripScriptTags");
            }
        }

        #endregion

        #region ----------- Boolean [ doStripStyleTags ] -------  [Da li da skloni sve Style tagove]

        private Boolean _doStripStyleTags = true;

        /// <summary>
        /// Da li da skloni sve Style tagove
        /// </summary>
        [Category("Switches")]
        [DisplayName("doStripStyleTags")]
        [Description("Da li da skloni sve Style tagove")]
        public Boolean doStripStyleTags
        {
            get { return _doStripStyleTags; }
            set
            {
                _doStripStyleTags = value;
                OnPropertyChanged("doStripStyleTags");
            }
        }

        #endregion

        #region --- doCollapseInlineSemanticTags ------- Da li da ukloni sve inline semantic tagove

        private Boolean _doCollapseInlineSemanticTags;

        /// <summary>
        /// Da li da ukloni sve inline semantic tagove
        /// </summary>
        public Boolean doCollapseInlineSemanticTags
        {
            get { return _doCollapseInlineSemanticTags; }
            set
            {
                _doCollapseInlineSemanticTags = value;
                OnPropertyChanged("doCollapseInlineSemanticTags");
            }
        }

        #endregion

        #region ----------- Boolean [ doFocusToBody ] -------  [Da li da se fokusira u Body tag?]

        private Boolean _doFocusToBody = true;

        /// <summary>
        /// Da li da se fokusira u Body tag?
        /// </summary>
        [Category("Switches")]
        [DisplayName("doFocusToBody")]
        [Description("Da li da se fokusira u Body tag?")]
        public Boolean doFocusToBody
        {
            get { return _doFocusToBody; }
            set
            {
                _doFocusToBody = value;
                OnPropertyChanged("doFocusToBody");
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 2014c> vraca optimizovan Xml sadrzaj
        /// </summary>
        /// <param name="input"></param>
        /// <param name="settings"></param>
        /// <param name="cached"></param>
        /// <returns></returns>
        public XPathNavigator preprocessNodes(XPathNavigator input)
        {
            preprocessCache cached = get_cache(input);


            if (doFocusToBody) input.MoveToChild("body", input.NamespaceURI);

            //XmlNode start = input;
            //
            //{
            //    start = input.SelectSingleNode("body", cached.xNm);
            //    if (start == null) start = input;
            //}

            IXPathNavigable output = input.filterCloning(cached, cached.toRemoveAttributes, developingLimit,
                                                         preprocessNodeText);


            return output.CreateNavigator();
        }

        public void preprocessNodeText(XPathNavigator node)
        {
            if (node != null)
            {
                if (node.NodeType == XPathNodeType.Text)
                {
                    node.SetValue(preprocessText(node.Value));
                    //node.Value = preprocessText(node.InnerText);
                }
            }
        }

        public String preprocessText(String input)
        {
            if (doRemoveHtmlEntities)
            {
                try
                {
                    input = WebUtility.HtmlDecode(input);
                    //input = HtmlEntity.DeEntitize(input);
                }
                catch (Exception ex)
                {
                    aceLog.log(ex.Message);
                    throw;
                }
            }

            if (doTransliterateToLat)
            {
                throw new NotImplementedException("Transliteration");
               // input = transliteration.transliterate(input, true);
            }
            return input;
        }
    }
}