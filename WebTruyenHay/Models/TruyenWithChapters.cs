using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTruyenHay.Models
{
    public class TruyenWithChapters
    {
        public Truyen Truyen { get; set; }
        public IEnumerable<Chuong> Chapters { get; set; }
    }
}