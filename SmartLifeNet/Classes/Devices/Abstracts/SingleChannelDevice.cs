using Newtonsoft.Json;
using SmartLifeNet.API.Responses;
using SmartLifeNet.Helpers;
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
#if NOTWORKING
     public async Task<string> GetState()
        {
            var json = await API.Rest.QueryDevice(context.region, id, context.Credentials.access_token);
            var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
            if(response?.header.code == "FrequentlyInvoke")
            {
                throw new SmartLifeError(response?.header.msg);
            }
            if (response?.header.code == Constants.DiscoveryCode.SUCCESS)
            {
                return "on";
            }
            return null;
        }
#endif

        public async Task SetState(int state)
        {
            var json = await API.Rest.SetDeviceSkill(context.region, id, context.Credentials.access_token, state);
            var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
            if (response?.header.code != Constants.DiscoveryCode.SUCCESS)
            {
                throw new SmartLifeError(response?.header.msg);
            }
        }
    }
}
