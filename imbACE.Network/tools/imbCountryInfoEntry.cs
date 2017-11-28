#region USING



#endregion

namespace imbACE.Network.tools
{
    using imbSCI.Data.data;
    #region imbVELES USING

    using System;
    using System.Collections.Generic;

    #endregion

   // [imbSql(shemaFromTypeMode.onlyDirect)]
    public class imbCountryInfoEntry : imbBindable
    {
        #region --- tldList ------- Lista svih top level domena

        private String _tldList;

        /// <summary>
        /// Lista svih top level domena
        /// </summary>
        public String tldList
        {
            get { return _tldList; }
            set
            {
                _tldList = value;
                OnPropertyChanged("tldList");
            }
        }

        #endregion

        private String _countryCode;
        private String _countryName;

        /// <summary>
        /// Top Level domains by TLD sufix
        /// </summary>
        private Dictionary<String, imbTopLevelDomain> _countryTLDindex;

        #region imbObject Property <Dictionary<String, imbTopLevelDomain>> countryTLDindex

        /// <summary>
        /// imbControl property countryTLDindex tipa Dictionary<String, imbTopLevelDomain>
        /// </summary>
      //  [imbSql(sqlEntityPropMode.skip)]
        public Dictionary<String, imbTopLevelDomain> countryTLDindex
        {
            get
            {
                if (_countryTLDindex == null) _countryTLDindex = new Dictionary<string, imbTopLevelDomain>();
                return _countryTLDindex;
            }
            set { _countryTLDindex = value; }
        }

        #endregion

        #region imbObject Property <String> countryCode

        /// <summary>
        /// imbControl property countryCode tipa String
        /// </summary>
        //[imbSql(sqlEntityPropMode.unique)]
        public String countryCode
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                OnPropertyChanged("tldList");
            }
        }

        #endregion

        #region imbObject Property <String> countryName

        /// <summary>
        /// imbControl property countryName tipa String
        /// </summary>
        public String countryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;
                OnPropertyChanged("tldList");
            }
        }

        #endregion

        public void learnFromTLD(imbTopLevelDomain input)
        {
            countryCode = input.countryCode.ToUpper().Trim();


            countryName = input.countryName;
            tldList = "";

            if (!(countryTLDindex.ContainsKey(input.countryCode)))
            {
                countryTLDindex.Add(input.countryCode, input);
                tldList += input.countryCode + "; ";
            }
        }
    }
}