using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shippingCodefirstTest.Models {
    public partial class AspNetUser {

        public ICollection<Label> Labels { get; set; }
        public ICollection<OrderHistory> OrderHistories { get; set; }

    }
}