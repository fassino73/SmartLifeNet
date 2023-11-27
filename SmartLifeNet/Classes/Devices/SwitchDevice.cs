using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public class SwitchDevice : SingleChannelDevice
    {
        public async Task TurnOff() => await SetState(0);

        public async Task TurnOn() => await SetState(1);

#if NOTWORKING
        public async Task<bool> IsOn() => await GetState() == "on";
#endif
    }
}
