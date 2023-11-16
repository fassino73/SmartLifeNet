using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLifeNet.Constants
{
    internal static class URLs
    {

        public static string GetHost(string region)
        {
            return $"px1.tuya{region.ToLower()}.com";
        }

        public static string GetAuthUrl()
        {
            return $"homeassistant/auth.do";
        }


        public static string GetSkillUrl()
        {
            return $"homeassistant/skill";
        }


       
    }
}
