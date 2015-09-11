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
        public int LabelId { get; set; }

        [Required]
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

        [Required]
        //[DatabaseGenerated(DatabaseGenerationOption.Computed)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_time { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime last_updated_time { get; set; }

        public int order_id { get; set; }

        [ForeignKey("order_id")]
        public OrderHistory OrderHistory { get; set; }

        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public AspNetUser User { get; set; }

    }
}