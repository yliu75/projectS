namespace shippingCodefirstTest.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    enum OrderState {Created,Paid,Cancelled }


    public partial class OrderHistory {

        [Key]
        public int OrderId { get; set; }

        [Column(TypeName = "Money")]
        public decimal total_sell_price { get; set; }

        [Column(TypeName = "Money")]
        public decimal total_cost_price { get; set; }

        [Column(TypeName = "Money")]
        public decimal total_sell_tax { get; set; }

        [Column(TypeName = "Money")]
        public decimal total_sell_balance { get; set; }

        public string note { get; set; }

        [Index]
        public int state { get; set; }

        public string payment_id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_time { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime last_updated_time { get; set; }

        public DateTime paid_on_time { get; set; }

        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public AspNetUser User { get; set; }

        public ICollection<Label> Labels { get; set; }


    }
} 