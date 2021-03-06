#region USING



#endregion

namespace imbACE.Network.web.core
{
    #region imbVeles using

    using System;
    using imbACE.Network.tools;
    using imbSCI.Data.data;

    #endregion

    /// <summary>
    /// Request parametar koji ucestvuje u formiranju URL-a, ali i u izvrsavanju requesta
    /// </summary>
    public class requestParameter : imbBindable
    {
        private String _Name;

        private String _Value;
        private Boolean _useIt = true;

        #region imbObject Property <String> Value

        /// <summary>
        /// imbControl property Value tipa String
        /// </summary>
        public String Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }

        #endregion

        #region imbObject Property <String> Name

        /// <summary>
        /// imbControl property Name tipa String
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region imbObject Property <Boolean> useIt

        /// <summary>
        /// imbControl property useIt tipa Boolean
        /// </summary>
        public Boolean useIt
        {
            get { return _useIt; }
            set
            {
                _useIt = value;
                OnPropertyChanged("useIt");
            }
        }

        #endregion

        public requestParameter()
        {
        }

        public requestParameter(String paramLine)
        {
            try
            {
                String[] el = paramLine.Split(imbNetworkTools.URL_PARAMOPERATOR.ToCharArray(),
                                              StringSplitOptions.RemoveEmptyEntries);
                Name = el[0];

                Value = Uri.UnescapeDataString(el[1]);
                if (Value.Contains("$"))
                {
                    Value = "";
                }
                useIt = true;
            }
            catch
            {
                useIt = false;
            }
        }
    }
}