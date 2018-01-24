using System;
using System.Threading;
using imbACE.TestUnit.test_stuff;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using imbACE.Core.data.mysql;
using imbSCI.Core.reporting;
using imbSCI.Core.reporting.render.builders;
using imbACE.Network.tools;
using System.Data;
using imbACE.Network.web;

namespace imbACE.TestUnit
{
    [TestClass]
    public class UnitTest1
    {
        public static testApplication app;

        [TestMethod]
        public void TestWebLoad()
        {

            


        }


        [TestMethod]
        public void TestDatabase()
        {

            dataBaseTarget dBT = new dataBaseTarget();

            DataTable dt = dBT.GetTable("topleveldomains");

            Assert.IsTrue(dt.Rows.Count > 0);


        }



        [TestMethod]
        public void StartApp()
        {
            ILogBuilder logger = new  builderForLogBase();

            app = new testApplication();

                      
            Thread t = new Thread(newThread);
            t.Start();


            Thread.Sleep(2000);


            dataBaseTarget dBT = new dataBaseTarget();

            imbACE.Network.tools.systemKnowledge.prepare(dBT, logger);


            domainAnalysis da = new domainAnalysis("http://www.koplas.co.rs");

            Assert.IsNotNull(da.tldDefinition);
           

        }


        public void newThread()
        {
            app.StartApplication(null);
        }
    }
}
