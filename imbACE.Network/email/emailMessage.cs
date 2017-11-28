// --------------------------------------------------------------------------------------------------------------------
// <copyright file="emailMessage.cs" company="imbVeles" >
//
// Copyright (C) 2017 imbVeles
//
// This program is free software: you can redistribute it and/or modify
// it under the +terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
// <summary>
// Project: imbACE.Network
// Author: Goran Grubic
// ------------------------------------------------------------------------------------------------------------------
// Project web site: http://blog.veles.rs
// GitHub: http://github.com/gorangrubic
// Mendeley profile: http://www.mendeley.com/profiles/goran-grubi2/
// ORCID ID: http://orcid.org/0000-0003-2673-9471
// Email: hardy@veles.rs
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace imbACE.Network.email
{
    using imbACE.Core;
    using imbACE.Core.core.diagnostic;
    using imbSCI.Core;
    using imbSCI.Core.attributes;
    using imbSCI.Core.enums;
    using imbSCI.Core.extensions.text;
    using imbSCI.Core.extensions.typeworks;
    using imbSCI.Core.interfaces;
    using imbSCI.Data;
    using imbSCI.Data.collection;
    using imbSCI.Data.data;
    using imbSCI.Data.interfaces;
    using imbSCI.DataComplex;
    using imbSCI.Reporting;
    using imbSCI.Reporting.enums;
    using imbSCI.Reporting.includes;
    using imbSCI.Reporting.interfaces;

    /// <summary>
    /// E-Mail message pre-compiled instance
    /// </summary>
    /// <seealso cref="imbSCI.Data.data.imbBindable" />
    /// <seealso cref="imbSCI.Data.interfaces.IObjectWithName" />
    [imb(imbAttributeName.collectionPrimaryKey, "name")]
        public class emailMessage : imbBindable, IObjectWithName
        {
            /// <summary>
            /// vraca jedinstveni emailSendTask id
            /// </summary>
            /// <returns></returns>
            public string makeUID()
            {
                return
                    (address.toStringSafe("addunknown") + "-" + subject.toStringSafe("subjectunknown") + "-" +
                     timeStamp.GenerateTimeStamp(imbTimeStampFormat.imbDatabaseTableName)).getValidFileName();
            }

            

            private string _name; // = new String();

            /// <summary>
            /// UID of this email message
            /// </summary>
            public string name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    OnPropertyChanged("name");
                }
            }

            

            #region --- subject ------- naslov poruke

            private string _subject;

            /// <summary>
            /// Email subject
            /// </summary>
            public string subject
            {
                get { return _subject; }
                set
                {
                    _subject = value;
                    OnPropertyChanged("subject");
                }
            }

            #endregion

            #region --- content ------- sadrzaj poruke

            private string _content;

            /// <summary>
            /// Content of the message
            /// </summary>
            public string content
            {
                get { return _content; }
                set
                {
                    _content = value;
                    OnPropertyChanged("content");
                }
            }

            #endregion

            #region --- address ------- adresa na koju salje poruku

            private string _address;

            /// <summary>
            /// Address To
            /// </summary>
            public string address
            {
                get { return _address; }
                set
                {
                    _address = value;
                    OnPropertyChanged("address");
                }
            }

            #endregion

            #region --- from ------- Adresa sa koje ide poruka

            private string _from;

            /// <summary>
            /// Address From
            /// </summary>
            public string from
            {
                get { return _from; }
                set
                {
                    _from = value;
                    OnPropertyChanged("from");
                }
            }

            #endregion

            #region --- attachments ------- kolekcija sa fajlovima za attachovanje

            private reportIncludeFileCollection _attachments = new reportIncludeFileCollection();

            /// <summary>
            /// Collection of included attachment files - files to send
            /// </summary>
            public reportIncludeFileCollection attachments
            {
                get { return _attachments; }
                set
                {
                    _attachments = value;
                    OnPropertyChanged("attachments");
                }
            }

            #endregion
        }
    
}
