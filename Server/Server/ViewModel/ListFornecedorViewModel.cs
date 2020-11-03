using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class DoubleListViewModel
    {
        public List<IdNameViewModel> Linked { get; set; }
        public List<IdNameViewModel> NotLinked { get; set; }
    }
}
