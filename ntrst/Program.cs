using io.harness.cfsdk.client.api;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Generic;
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
            string API_KEY = "YOUR_API_KEY_GOES_HERE";

            // Change to your flag id's
            string flag1 = "flag1";
            string flag2 = "flag2";
            string flag3 = "flag3";
            string flag4 = "flag4";


            // If you want you can uncoment this configure serilog sink you want and see internal SDK information messages:
            //
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.File("c:\\Temp\\TestLog.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();


            config = Config.Builder()
                .SetAnalyticsEnabled()
                .SetStreamEnabled(true)
                // If you want to use the custom server environment:
                // .ConfigUrl("https://config.feature-flags.uat.harness.io/api/1.0")
                // .EventUrl("https://event.feature-flags.uat.harness.io/api/1.0")
                .Build();

            Console.WriteLine("Config URL: " + config.ConfigUrl);
            Console.WriteLine("Event URL: " + config.EventUrl);

            await CfClient.Instance.Initialize(API_KEY, config);

            Dictionary<string, string> attrs =
                        new Dictionary<string, string>();

            attrs.Add("email", "hello@example.com");
            attrs.Add("one", "one");
            attrs.Add("two", "two");
            attrs.Add("three", "three");

            io.harness.cfsdk.client.dto.Target target =
                io.harness.cfsdk.client.dto.Target.builder()
                .Name("My_Target") // Can change with your target name
                .Identifier("hello@example.com") // Can change with your target identifier
                .Attributes(attrs)
                .build();

            while (true) {

                bool resultBool = CfClient.Instance.boolVariation(flag1, target, false);
                double resultNumber = CfClient.Instance.numberVariation(flag2, target, -1.0);
                string resultString = CfClient.Instance.stringVariation(flag3, target, "NO VALUE !!!");
                JObject resultJson = CfClient.Instance.jsonVariation(flag4, target, null);

                Console.WriteLine("Bool value ---->" + resultBool);
                Console.WriteLine("Number value ---->" + resultNumber);
                Console.WriteLine("String value ---->" + resultString);
                Console.WriteLine("JSON value ---->" + resultJson);

                Console.WriteLine("---------------");

                Thread.Sleep(10 * 1000);
            }
        }

    }
}
