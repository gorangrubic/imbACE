namespace imbACE.Network.tools
{
	#region imbVELES USING

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Linq;
    using imbSCI.DataComplex.tables;
    using imbACE.Core.application;
    using System.IO;
    using imbACE.Core.data.mysql;
    using imbSCI.Core.reporting;

    #endregion

    /// <summary>
    /// Klasa za upravljanje znanjem sistema 
    /// </summary>
    public static class systemKnowledge
	{
		/// <summary>
		/// 2013a: Poziv za priremu
		/// Nije bitan sadržaj već samo postojanje poziva ka ovom metodu jer to osigurava da se instanciraju sve date statičke kolekcije
		/// </summary>
		public static void prepare(dataBaseTarget dbSource = null, ILogBuilder logger=null)
		{
            //Int32 entries = topLevelDomains.loadItems();


            if (dbSource != null) {

                countries.Load(dbSource.GetTable(nameof(countries)), logger,objectTableUpdatePolicy.overwrite);

                topLevelDomains.Load(dbSource.GetTable(nameof(topLevelDomains).ToLower()), logger, objectTableUpdatePolicy.overwrite);

                whoIsServers.Load(dbSource.GetTable(nameof(whoIsServers).ToLower()), logger, objectTableUpdatePolicy.overwrite);

            }


            imbDomainManager.prepare();

            

            List<imbTopLevelDomain> tlds = topLevelDomains.GetList(); //.selectItems<imbTopLevelDomain>("doPreload=1");
            foreach (imbTopLevelDomain t in tlds) imbDomainManager.AllDomains.Add(t);

			
			
			imbDomainManager.afterLoad();

		}


		/// <summary>
		/// 2013a: prvenstveno je napravljeno za jednokratnu upotrebu, možda se upotrebi za eksportovanje različitog znanja sa veba
		/// </summary>
		public static void rebuildKnowledge()
		{
			imbDomainManager.afterLoad();


			foreach (KeyValuePair<string, imbTopLevelDomain> item in imbDomainManager.DomainDictionary)
			{
				systemKnowledge.topLevelDomains.Add(item.Value);
			}

			foreach (imbWhoIsServer item in imbDomainManager.servers)
			{
				systemKnowledge.whoIsServers.Add(item);
			}


			foreach (imbTopLevelDomain tmpDomain in imbDomainManager.AllDomainsUnique)
			{
				imbCountryInfoEntry tmpEntry = new imbCountryInfoEntry();

				tmpEntry.countryName = tmpDomain.countryName;
				tmpEntry.countryCode = tmpDomain.countryCode;


				systemKnowledge.countries.Add(tmpEntry);
			}

			foreach (KeyValuePair<string, imbCountryInfoEntry> pair in imbCountryInfoEngine.countryBase)
			{
				// systemKnowledge.countries.Add((imbCountryInfoEntry)pair.Value, pair.Key);
			}
		}

        #region -----------  topLevelDomains  -------  [kolekcija TLDa]

        private static objectTable<imbTopLevelDomain> _topLevelDomains = null;

		/// <summary>
		/// kolekcija TLDa
		/// </summary>
		[XmlIgnore]
		[Category("systemKnowledge")]
		[DisplayName("topLevelDomains")]
		[Description("kolekcija TLDa")]
		public static objectTable<imbTopLevelDomain> topLevelDomains
		{
			get {

                if (_topLevelDomains == null)
                {

                    _topLevelDomains = new objectTable<imbTopLevelDomain>(aceApplicationInfo.FOLDERNAME_REPORTS + Path.PathSeparator + "whoIsServerTable.xml", true, nameof(imbTopLevelDomain.domainName));
                }
                return _topLevelDomains;

            }
			set { _topLevelDomains = value; }
		}

        #endregion

        #region -----------  whoIsServers  -------  [kolekcija who is servera]

        private static objectTable<imbWhoIsServer> _whoIsServers = null;
			//new objectTable<imbWhoIsServer>("whoisservers", mainDBs.system, sqlCollectionOperationMode.all);

		/// <summary>
		/// kolekcija who is servera
		/// </summary>
		[XmlIgnore]
		[Category("systemKnowledge")]
		[DisplayName("whoIsServers")]
		[Description("kolekcija who is servera")]
		public static objectTable<imbWhoIsServer> whoIsServers
		{
			get {

                if (_whoIsServers == null)
                {
                    _whoIsServers = new objectTable<imbWhoIsServer>(aceApplicationInfo.FOLDERNAME_REPORTS + Path.PathSeparator + "whoIsServerTable.xml", true, nameof(imbWhoIsServer.url));
                }

                return _whoIsServers;

            }
			set { _whoIsServers = value; }
		}

        #endregion

        #region -----------  countries  -------  [Podaci o drzavama]

        private static objectTable<imbCountryInfoEntry> _countries = null;
			

		/// <summary>
		/// Podaci o drzavama
		/// </summary>
		[XmlIgnore]
		[Category("systemKnowledge")]
		[DisplayName("countries")]
		[Description("Podaci o drzavama")]
		public static objectTable<imbCountryInfoEntry> countries
		{
			get {

                if (_countries == null)
                {
                    _countries = new objectTable<imbCountryInfoEntry>(aceApplicationInfo.FOLDERNAME_REPORTS + Path.PathSeparator + "countryTable.xml", true, nameof(imbCountryInfoEntry.countryCode));
                }

                return _countries;
            }
			set { _countries = value; }
		}

		#endregion

		/*
		#region -----------  dictionaries  -------  [Skup informacija o jezicima sa recnicima]
		private static imbSqlEntityCollection<imbLanguageInfo> _dictionaries = new imbSqlEntityCollection<imbLanguageInfo>("dictionaries");
		/// <summary>
		/// Skup informacija o jezicima sa recnicima
		/// </summary>
		[XmlIgnore]
		[Category("systemKnowledge")]
		[DisplayName("dictionaries")]
		[Description("Skup informacija o jezicima sa recnicima")]
		public static imbSqlEntityCollection<imbLanguageInfo> dictionaries
		{
			get
			{
				return _dictionaries;
			}
			set
			{
				_dictionaries = value;
				
			}
		}
		#endregion
	
	

		#region -----------  serviceTypes  -------  [Vrste online servisa]
		private static imbSqlEntityCollection<imbServiceType> _serviceTypes = new imbSqlEntityCollection<imbServiceType>("serviceTypes");
		/// <summary>
		/// Vrste online servisa
		/// </summary>
		[XmlIgnore]
		[Category("systemKnowledge")]
		[DisplayName("serviceTypes")]
		[Description("Vrste online servisa")]
		public static imbSqlEntityCollection<imbServiceType> serviceTypes
		{
			get
			{
				return _serviceTypes;
			}
			set
			{
				_serviceTypes = value;
				
			}
		}
		#endregion
		
		*/
	}
}