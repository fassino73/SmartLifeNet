using System;
using SmartLifeNet;
using SmartLifeNet.Classes;

namespace SmartLifeRunner
{
    public class DeviceManager
    {
        private SmartLife smartLife;

        public DeviceManager(string smartLifeEmail, string smartLifePassword)
        {
            smartLife = new SmartLife(smartLifeEmail, smartLifePassword, region: "US");
        }

        public async Task InitializeDevicesAsync()
        {
            await smartLife.ConnectAsync();
            await smartLife.InitDevicesAsync();
        }

        public Device? GetDevice(string deviceId)
        {
            return smartLife?.Devices.FirstOrDefault(x => x.id == deviceId);
        }

        public SwitchDevice? GetSwitch(string deviceId)
        {
            return GetDevice(deviceId) as SmartLifeNet.Classes.SwitchDevice;
        }
    }
}

