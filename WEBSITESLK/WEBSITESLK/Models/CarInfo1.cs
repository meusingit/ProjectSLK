namespace WEBSITESLK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    

    public partial class CarInfo1
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string VIN { get; set; }
        [Required]
        [StringLength(20)]
        public string Brand { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        [StringLength(50)]
        public string StoreLoc { get; set; }
        [Required]
        [StringLength(50)]
        public string Owner { get; set; }
        [Required]
        public bool isimgup { get; set; }
    }
}
