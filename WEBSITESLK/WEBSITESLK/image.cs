namespace WEBSITESLK
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class image
    {
        public int id { get; set; }

        [Column("image")]
        [Display(Name = "Upload File")]
        public string image1 { get; set; }
        public HttpPostedFileBase imgf { get; set; }
    }
}
