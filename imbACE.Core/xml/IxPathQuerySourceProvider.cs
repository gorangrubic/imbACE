namespace imbACE.Core.xml
{
    #region imbVeles using

    using System.Xml.XPath;

    #endregion

    /// <summary>
    /// xPathQuery koji moze da razmenjuje izvor podataka
    /// </summary>
    public interface IxPathQuerySourceProvider : IxPathQueryCache
    {
        XPathNavigator navigator { get; set; }
    }
}