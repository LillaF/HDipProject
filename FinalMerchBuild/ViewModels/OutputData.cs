using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalMerchBuild.Models;

namespace FinalMerchBuild.ViewModels
{
    public class OutputData
    {
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Bay> Bays { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}