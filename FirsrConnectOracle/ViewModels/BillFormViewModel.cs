using FirsrConnectOracle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirsrConnectOracle.ViewModels
{
    public class BillFormViewModel
    {
        public BILL BILL { get; set; }
        public IEnumerable<CUSTOMER> CUSTOMERs { get; set; }
    }
}