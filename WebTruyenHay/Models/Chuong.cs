//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTruyenHay.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Chuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chuong()
        {
            this.imagechuongs = new HashSet<imagechuong>();
        }
        public HttpPostedFileBase UploadImagechuong { get; set; }
        public string IDchuong { get; set; }
        public string TruyenID { get; set; }
        public Nullable<int> SoThuTu { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
    
        public virtual Truyen Truyen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<imagechuong> imagechuongs { get; set; }
    }
}
