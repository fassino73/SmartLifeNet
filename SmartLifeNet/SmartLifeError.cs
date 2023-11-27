using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet
{
    public class SmartLifeError : Exception
    {
        public SmartLifeError(string message) : base(message)
        {

        }
    }
}
