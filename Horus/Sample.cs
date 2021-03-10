using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Waves.Horus
{
    public class Sample
    {
        public string Id { get; set; }
        public string Path { get; set; }

        public Sample()
        {

        }

        public Sample(string id, string path)
        {
            this.Id = id;
            this.Path = path;
        }

    }
}
