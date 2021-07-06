using io.harness.cfsdk.client.api;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Threading.Tasks;

namespace HarnesSDKSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Config config;
            //change this to your API_KEY 
            string API_KEY = "e9305965-4db9-483b-8277-5d400377e948";

            //change to your flag id's
            string boolflagname = "t2";
            string numberflagname = "Tnumber";
            string stringflagname = "Tstring";
            string jsonflagname = "Tjson";


            //If you want you can uncoment this 
            //configure serilog sink you want
            //and see internal SDK information messages
            //Log.Logger = new LoggerConfiguration()
            //   .MinimumLevel.Debug()
            //   .WriteTo.File("c:\\harness\\logs\\TestLog.txt", rollingInterval: RollingInterval.Day)
            //   .CreateLogger();


            config = Config.Builder()
                .SetPollingInterval(10000)
                .SetAnalyticsEnabled()
                .SetStreamEnabled(true)
                .Build();

            CfClient cfClient = await CfClient.getInstance(API_KEY, config);

            Console.Write("Pres any key > ");
            ConsoleKeyInfo commandline = Console.ReadKey();

            cfClient = await CfClient.getInstance();
            io.harness.cfsdk.client.dto.Target target =
                io.harness.cfsdk.client.dto.Target.builder()
                .Name("Aptiv") //can change with your target name
                .Identifier("1") //can change with your target identifier
                .build();

            Console.WriteLine("Bool Variation Calculation Comamnd Start ==============");
            bool result = await cfClient.boolVariation(boolflagname, target, false);
            Console.WriteLine("Bool Variation value ---->" + result);
            Console.WriteLine("Bool Variation Calculation Comamnd Stop ---------------\n\n\n");

            Console.Write("Pres any key > ");
             commandline = Console.ReadKey();

            Console.WriteLine("String Variation Calculation Comamnd Start ==============");
            string sresult = await cfClient.stringVariation(stringflagname, target, "def value");
            Console.WriteLine("String Variation value ---->" + sresult);
            Console.WriteLine("String Variation Calculation Comamnd Stop ---------------\n\n\n");

            Console.Write("Pres any key > ");
             commandline = Console.ReadKey();

            Console.WriteLine("Number Variation Calculation Comamnd Start ==============");
            double dresult = await cfClient.numberVariation(numberflagname, target, 999999);
            Console.WriteLine("Number Variation value ---->" + dresult);
            Console.WriteLine("Number Variation Calculation Comamnd Stop ---------------\n\n\n");

            Console.Write("Pres any key > ");
             commandline = Console.ReadKey();

            Console.WriteLine("JSON Variation Calculation Comamnd Start ==============");
            JObject Jresult = await cfClient.jsonVariation(jsonflagname, target, JObject.Parse(@"{
  DefVal: 'JSON def value',
}"));
            Console.WriteLine("JSON Variation value ---->" + Jresult.ToString());
            Console.WriteLine("JSON Variation Calculation Comamnd Stop ---------------\n\n\n");
            Console.Write("Pres any key > ");
             commandline = Console.ReadKey();
        }

    }
}
