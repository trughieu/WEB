using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Models
{
    public class HangVe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SelectList HangVeList { get; set; }
    }
}