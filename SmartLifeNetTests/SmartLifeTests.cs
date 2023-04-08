using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLifeNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Tests
{
    [TestClass()]
    public class SmartLifeTests
    {

        private const string email = "";
        private const string password = "";

        [TestMethod()]
        public async Task GetCredentialsTest()
        {
            var smart = new SmartLife(email, password);
            var cred = await smart.GetCredentialsAsync();
            Assert.IsNotNull(cred);
            Console.WriteLine(cred);
        }

        [TestMethod()]
        public async Task StoreCredenditalsToFileTest()
        {
            var smart = new SmartLife(email, password);
            var cred = await smart.GetCredentialsAsync();
            await smart.StoreCredentialsToFileAsync();
            Console.WriteLine(cred);
        }

        [TestMethod()]
        public async Task RestoreCredenditalsFromFileTest()
        {
            var smart = new SmartLife(email, password);
            await smart.RestoreCredentialsFromFileAsync();
            Assert.IsNotNull(smart.Credentials);
            Console.WriteLine(smart.Credentials);
        }

        [TestMethod()]
        public async Task ConnectTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            Assert.IsNotNull(smart.Credentials);
            Console.WriteLine(smart.Credentials);
        }

        [TestMethod()]
        public async Task GetDevicesTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.GetDevicesAsync();
            Assert.IsNotNull(smart.Devices);
        }

        [TestMethod()]
        public async Task InitDevicesTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.InitDevicesAsync();
            Assert.IsNotNull(smart.Devices);
        }

        [TestMethod()]
        public async Task StoreDevicesToFileTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.GetDevicesAsync();
            await smart.StoreDevicesToFileAsync();
        }

        [TestMethod()]
        public async Task RestoreDevicesFromFileTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.GetDevicesAsync();
            await smart.RestoreDevicesFromFileAsync();
        }


        [TestMethod()]
        public async Task SwitchTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.InitDevicesAsync();
            var device = smart.Devices.FirstOrDefault(x => x is SmartLifeNet.Classes.SwitchDevice) as SmartLifeNet.Classes.SwitchDevice;
            await device?.SetState(1);
        }

        [TestMethod()]
        public async Task MultiSwitchTest()
        {
            var smart = new SmartLife(email, password);
            await smart.ConnectAsync();
            await smart.InitDevicesAsync();
            var device = smart.Devices.FirstOrDefault(x => x is SmartLifeNet.Classes.MultiSwitchDevice) as SmartLifeNet.Classes.MultiSwitchDevice;
            await device?.SetAllChannels(1);

            await Task.Delay(500);

            await device?.SetAllChannels(0);
        }

    }
}