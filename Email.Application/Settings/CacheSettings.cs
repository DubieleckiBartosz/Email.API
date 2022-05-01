using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Application.Settings
{
    public class CacheSettings
    {
        public bool Enabled { get; set; }

        public int DefaultTime { get; set; }

        public string RedisConnection { get; set; }
    }
}
