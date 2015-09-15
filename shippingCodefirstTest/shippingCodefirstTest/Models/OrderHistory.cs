namespace shippingCodefirstTest.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    enum OrderState {Created,Paid,Cancelled }


    public partial class OrderHistory {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get ; set; }


        //public decimal price { get; set; }
        [Column(TypeName = "Money")]
        [Display(Name ="Total Sell Price")]
        public decimal total_sell_price { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name ="Total Cost Price")]
        public decimal total_cost_price { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Total Sell Tax")]
        public decimal total_sell_tax { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Total Sell Balance")]
        public decimal total_sell_balance { get; set; }

        public string note { get; set; }

        [Index]
        public int state { get; set; }

        [NotMapped]
        public string status { get { return Enum.GetName(typeof(OrderState),state); } }

        [Display(Name = "Total Payment ID")]
        public string payment_id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name ="Created Time")]
        public DateTime created_time { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name ="Last Updated Time")]
        public DateTime last_updated_time { get; set; }

        [Display(Name ="Paid On Time")]
        public DateTime? paid_on_time { get; set; }

        [Display(Name ="User ID")]
        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual AspNetUser User { get; set; }

        public virtual ICollection<Label> Labels { get; set; }


    }
} 