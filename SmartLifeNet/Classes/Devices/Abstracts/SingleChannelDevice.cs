using Newtonsoft.Json;
using SmartLifeNet.API.Responses;
using SmartLifeNet.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public abstract class SingleChannelDevice : Device
    {
        public abstract Task<string> GetState();
        

        public async Task<bool> SetState(int state)
        {
            var json = await API.Rest.SetDeviceSkill(context.region, id, context.Credentials.access_token, state);
            var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
            return (response?.header.code == Constants.DiscoveryCode.SUCCESS);
        }
    }
}
