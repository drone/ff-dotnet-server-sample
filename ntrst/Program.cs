using io.harness.cfsdk.client.api;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HarnesSDKSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Config config;

            // Change this to your API_KEY :
            string API_KEY = "YOUR_API_KEY";

            //change to your flag id's
            string boolflagname = "flag1";
            string numberflagname = "flag2";
            string stringflagname = "flag3";
            string jsonflagname = "flag4";

            // If you want you can uncoment this 
            // configure serilog sink you want
            // and see internal SDK information messages:
            // Log.Logger = new LoggerConfiguration()
               // .MinimumLevel.Debug()
               // .WriteTo.File("c:\\harness\\logs\\TestLog.txt", rollingInterval: RollingInterval.Day)
               // .CreateLogger();


            config = Config.Builder()
                .SetAnalyticsEnabled()
                .SetStreamEnabled(true)
                // For UAT environment:
                // .ConfigUrl("https://config.feature-flags.uat.harness.io/api/1.0")
                // .EventUrl("https://event.feature-flags.uat.harness.io/api/1.0")
                .Build();

            Console.WriteLine("Config URL: " + config.ConfigUrl);
            Console.WriteLine("Event URL: " + config.EventUrl);

            CfClient cfClient = await CfClient.getInstance(API_KEY, config);

            io.harness.cfsdk.client.dto.Target target =
                io.harness.cfsdk.client.dto.Target.builder()
                .Name("Harness") //can change with your target name
                .Identifier("Harness") //can change with your target identifier
                .build();

            while (true) {

                cfClient = await CfClient.getInstance();

                Console.WriteLine("Bool Variation Calculation Comamnd Start ============== " + boolflagname);
                bool result = await cfClient.boolVariation(boolflagname, target, false);
                Console.WriteLine("Bool Variation value ---->" + result);
                Console.WriteLine("Bool Variation Calculation Comamnd Stop ---------------\n\n\n");

                Console.WriteLine("String Variation Calculation Comamnd Start ============== " + stringflagname);
                string sresult = await cfClient.stringVariation(stringflagname, target, "def value");
                Console.WriteLine("String Variation value ---->" + sresult);
                Console.WriteLine("String Variation Calculation Comamnd Stop ---------------\n\n\n");

                Console.WriteLine("Number Variation Calculation Comamnd Start ============== " + numberflagname);
                double dresult = await cfClient.numberVariation(numberflagname, target, 999999);
                Console.WriteLine("Number Variation value ---->" + dresult);
                Console.WriteLine("Number Variation Calculation Comamnd Stop ---------------\n\n\n");

                Console.WriteLine("JSON Variation Calculation Comamnd Start ============== " + jsonflagname);
                JObject Jresult = await cfClient.jsonVariation(jsonflagname, target, JObject.Parse(@"{
  DefVal: 'JSON def value',
}"));
                Console.WriteLine("JSON Variation value ---->" + Jresult.ToString());
                Console.WriteLine("JSON Variation Calculation Comamnd Stop ---------------\n\n\n");

                Thread.Sleep(2000);
            }
        }

    }
}
