namespace shippingCodefirstTest.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Linq;



    public enum LabelState { Created, HasPrice, HasAll, Finished };

    public enum LabelVer { s_1, s_2 }
    public class Label {

        [Key]
        [Display(Name ="Label ID")]
        public int LabelId { get; set; }

        [Required]
        [Display(Name ="Version")]
        public string ver { get; set; }

        [Column(TypeName = "xml")]
        public string lb_content { get; set; }

        [NotMapped]
        public XElement lb_content_xml {
            get { return XElement.Parse(lb_content); }
            set { lb_content = value.ToString(); }
        }



        [Index]
        public int state { get; set; }

        //[DatabaseGenerated(DatabaseGenerationOption.Computed)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Created Time")]
        public DateTime created_time { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Last Updated Time")]
        public DateTime last_updated_time { get; set; }

        [Display(Name ="Order ID")]
        public int order_id { get; set; }

        [ForeignKey("order_id")]
        public virtual OrderHistory OrderHistory { get; set; }

        [Display(Name ="User ID")]
        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual AspNetUser User { get; set; }

    }
}