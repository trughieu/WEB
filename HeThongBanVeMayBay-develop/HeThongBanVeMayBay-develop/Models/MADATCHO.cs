using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongBanVeMayBay.Models
{
    public class MADATCHO
    {
        [Display(Name = "Sân bay đi")]
        public string IDSanBayDi { get; set; }

        [Display(Name = "Sân bay đến")]
        public string IDSanBayDen { get; set; }

        [Display(Name = "Hãng bay")]
        public string HangBay { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Ngày bay: ")]
        public DateTime NgayBay { get; set; }

        [Display(Name = "Giờ bay")]
        public System.TimeSpan GioBay { get; set; }

        [Display(Name = "Giá tiền")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int GiaTien { get; set; }
    }
}