using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using SmartLifeNet.Classes;
using SmartLifeNet.Helpers.Extensions;
using SmartLifeNet.API.Responses;
using System.Linq;
using System.Collections.Generic;
using Mapster;
using System.IO;

namespace SmartLifeNet
{
    public class SmartLife
    {
        public string region { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string at { get; set; }
        public string apikey { get; set; }


        [JsonIgnore]
        public Credentials Credentials { get; private set; }

        [JsonIgnore]
        public Device[] Devices { get; private set; }


        public SmartLife(string email, string password, string region = "EU")
        {
            this.email = email;
            this.password = password;
            this.region = region;
        }

        public async Task<Credentials> ConnectAsync()
        {
            if (System.IO.File.Exists("credentials.json"))
            {
                await RestoreCredentialsFromFileAsync();
            }
            else
            {
                await GetCredentialsAsync();
                await StoreCredentialsToFileAsync();
            }

            if (Credentials.IsExpired)
            {
                await GetCredentialsAsync();
                await StoreCredentialsToFileAsync();
            }

            return Credentials;
        }


        public async Task InitDevicesAsync()
        {
            if (System.IO.File.Exists("devices.json"))
            {
                await RestoreDevicesFromFileAsync();
            }
            else
            {
                await GetDevicesAsync();
                await StoreDevicesToFileAsync();
            }
        }


        public async Task<Credentials> GetCredentialsAsync()
        {
            var response = await API.Rest.GetCredentials(email, password, region);
            Credentials = JsonConvert.DeserializeObject<Credentials>(response);
            return Credentials;
        }

        public async Task GetDevicesAsync()
        {
            var json = await API.Rest.GetDevices(region, Credentials.access_token);
            var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
            if (response.header.code != Constants.DiscoveryCode.SUCCESS) return;

            CreateDevices(response.payload.devices.Select(x => x.Adapt<Device>()).ToArray());
        }

        private void CreateDevices(Device[] devices)
        {
            var all = devices.Select(x => DeviceFactory.CreateDevice(this, x)).ToArray();

            var grouped = all.ToLookup(x => x.id.Split('_').First());
            Devices = grouped.Where(x => x.Count() == 1).Select(x => x.First())
                .Concat(
                    grouped.Where(x => x.Count() > 1).Select(x => DeviceFactory.CreateMultiDevice(x))
                    )
                .ToArray();

            deviceCache = all.ToDictionary(x => x.id);
        }

        private Dictionary<string, Device> deviceCache;

        public Task StoreCredentialsToFileAsync(string filename = "credentials.json")
            => System.IO.File.WriteAllTextAsync(filename, Credentials.AsJson());

        public async Task RestoreCredentialsFromFileAsync(string filename = "credentials.json")
        {
            var text = await File.ReadAllTextAsync(filename);
            Credentials = text.FromJson<Credentials>();
        }

        public Task StoreDevicesToFileAsync(string filename = "devices.json")
            => File.WriteAllTextAsync(filename, Devices.AsJson());

        public async Task RestoreDevicesFromFileAsync(string filename = "devices.json")
        {
            var text = await File.ReadAllTextAsync(filename);
            CreateDevices(JsonConvert.DeserializeObject<Device[]>(text));
        }
    }
}
