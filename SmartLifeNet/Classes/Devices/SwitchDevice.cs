using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public class SwitchDevice : SingleChannelDevice
    {
        public async Task<bool> TurnOff() => await SetState(0);

        public async Task<bool> TurnOn() => await SetState(1);

        public async Task Toggle()
        { 
            if(await GetState() == "on") 
                await TurnOff() ;
            else 
                await TurnOn() ;
        }

        public override Task<string> GetState() => (bool)data.state ? Task.FromResult("on") : Task.FromResult("off");

        public async Task<bool> IsOn() => await GetState() == "on";
    }
}
