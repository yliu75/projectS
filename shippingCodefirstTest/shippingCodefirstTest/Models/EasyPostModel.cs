using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shippingCodefirstTest.Models {
    public class EasyPostModel {

    }

    public class root {
        public sender_info sender_info { get; set; }
        public receiver_info receiver_info { get; set; }
        public parcel_info parcel_info { get; set; }
        public shipping_info shipping_info { get; set; }

    }

    public class sender_info {
        public string from_name { get; set; }
        public string from_address_1 { get; set; }
        public string from_address_2 { get; set; }
        public string from_city { get; set; }
        public string from_nation_state { get; set; }
        public string from_zipcode { get; set; }
        public string from_telephone { get; set; }
    }
    public class receiver_info {
        public string to_name { get; set; }
        //public string to_name { get; set; }
        public string to_address_1 { get; set; }
        public string to_address_2 { get; set; }
        public string to_city { get; set; }
        public string to_nation_state { get; set; }
        public string to_zipcode { get; set; }
        public string to_telephone { get; set; }
    }
    public class parcel_info {
        public string pkg_length { get; set; }
        public string pkg_width { get; set; }
        public string pkg_height { get; set; }
        public string pkg_weight { get; set; }
    }
    public class shipping_info {
        public string tracking_code { get; set; } = "0";
        public string shipping_label_address { get; set; } = "0";
    }



}