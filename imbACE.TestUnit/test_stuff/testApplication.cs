using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using imbACE.Core.application;
using imbACE.Services.application;
namespace imbACE.TestUnit.test_stuff
{
    public class testApplication : aceConsoleApplication<testConsoleConsole>
    {
        public override void setAboutInformation()
        {
            appAboutInfo = new aceApplicationInfo()
            {
                author = "Test",
                software = "testing softvare",
                applicationVersion = "test version",
                organization = "imb veles",
                welcomeMessage = "msg"
            };
        }
    }
}
