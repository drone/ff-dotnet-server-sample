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
            string API_KEY = "110f158a-3f54-4aea-9de4-03af85a38b63";

            //change to your flag id's
            string boolflagname = "TestBool";
           

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

            CfClient cfClient = CfClient.getInstance();
            cfClient.Initialize(API_KEY, config);

            io.harness.cfsdk.client.dto.Target target =
                io.harness.cfsdk.client.dto.Target.builder()
                .Name("Milos Vasic") //can change with your target name
                .Identifier("milos") //can change with your target identifier
                .build();

            while (true) {

                cfClient = CfClient.getInstance();

                Console.WriteLine("Bool Variation Calculation Comamnd Start ============== " + boolflagname);
                bool result = cfClient.boolVariation(boolflagname, target, false);
                Console.WriteLine("Bool Variation value ---->" + result);
                Console.WriteLine("Bool Variation Calculation Comamnd Stop ---------------\n\n\n");

                Thread.Sleep(2000);
            }
        }

    }
}
