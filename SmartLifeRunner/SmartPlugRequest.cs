using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SmartLifeRunner
{
    public class SmartPlugRequest
    {
        public SmartPlugState State { get; set; }

        public SmartPlugRequest()
        {
        }
    }

    public enum SmartPlugState
    {
        On,
        Off
    }
}

