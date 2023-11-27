using Newtonsoft.Json;
using RestSharp;
using SmartLifeNet.Classes;
using SmartLifeNet.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLifeNet.API
{

    public static class Rest
    {
        public static async Task<string> GetCredentials(string email, string password, string region, int country)
        {
            var host = Constants.URLs.GetHost(region);
            var client = new RestClient("https://" + host);

            var request = new RestRequest(Constants.URLs.GetAuthUrl(), Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Host", host);
            request.AddParameter("userName", email);
            request.AddParameter("password", password);
            request.AddParameter("countryCode", country);
            request.AddParameter("bizType", Constants.AppData.BIZ_TYPE);
            request.AddParameter("from", Constants.AppData.FROM);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

        public static async Task<string> GetDevices(string region, string accessToken)
        {
            var host = Constants.URLs.GetHost(region);
            var client = new RestClient("https://" + host);

            var request = new RestRequest(Constants.URLs.GetSkillUrl(), Method.Post);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var body = new {
                header = new {
                    name = "Discovery",
                    @namespace = "discovery",
                    payloadVersion = 1,
                },
                payload = new { accessToken }
            };

            request.AddParameter("application/json", body.AsJson(), ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

        public static async Task<string> SetDeviceSkill(string region, string deviceId, string accessToken, int state)
        {
            var host = Constants.URLs.GetHost(region);
            var client = new RestClient("https://" + host);

            var request = new RestRequest(Constants.URLs.GetSkillUrl(), Method.Post);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
    
            var body = new {
                header = new {
                    name = "turnOnOff",
                    @namespace = "control",
                    payloadVersion = 1,
                },
                payload = new { accessToken, devId = deviceId, value = state  }
            };

            request.AddParameter("application/json", body.AsJson(), ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

#if NOTWORKING
        public static async Task<string> QueryDevice(string region, string deviceId, string accessToken)
        {
            var host = Constants.URLs.GetHost(region);
            var client = new RestClient("https://" + host);

            var request = new RestRequest(Constants.URLs.GetSkillUrl(), Method.Get);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                header = new
                {
                    name = "QueryDevice",
                    @namespace = "query",
                    payloadVersion = 1,
                },
                payload = new { accessToken, devId = deviceId, value = 1 }
            };

            request.AddParameter("application/json", body.AsJson(), ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }
#endif

    }
}
