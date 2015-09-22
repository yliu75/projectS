namespace shippingCodefirstTest.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Xml.Linq;
    using EasyPost;
    using shippingCodefirstTest.Controllers;
    using shippingCodefirstTest.ViewModels;

    public enum LabelState { Created, HasPrice, HasAll, Finished };

    public enum LabelVer { s_1, s_2 }
    public class Label {



        public Label() {
            lookupPrice=new Shipment();
            //l_xml=new label_content_xml();
        }

        [Key]
        [Display(Name = "Label ID")]
        public int LabelId { get; set; }

        [Required]
        [Display(Name = "Version")]
        public string ver { get; set; }

        [Column(TypeName = "xml")]
        public string lb_content { get; set; }

        [NotMapped]
        public XDocument lb_content_xml {
            get { return XDocument.Parse(lb_content); }
            set { lb_content=value.ToString(); }
        }

        [NotMapped]
        public XmlViewModel lb_obj {
            get {
                if (_lb_obj == null) {
                    return XmlHelper.Deserialize<XmlViewModel>(lb_content);
                }
                return _lb_obj;
            }
        }

        [NotMapped]
        private Shipment _lookupPrice;

        [NotMapped]
        private XmlViewModel _lb_obj;

        [NotMapped]
        public Rate chosenRate { get; set; }

        [NotMapped]
        [Display(Name = "Price")]
        public Shipment lookupPrice {
            get {
                if(_lookupPrice!=null) return _lookupPrice;
                EasyPost.Client.apiKey="a5we2wQDH0O8cuyafkDsNw";
                //XDocument xml = new XDocument();
                //xml=lb_content_xml;
                XmlViewModel ep = lb_obj;
                Address fromAddress=new Address() {
                    company=ep.sender_info.from_name,
                    street1=ep.sender_info.from_address_1,
                    street2=ep.sender_info.from_address_2,
                    city=ep.sender_info.from_city,
                    state=ep.sender_info.from_nation_state,
                    zip=ep.sender_info.from_zipcode,
                    phone=ep.sender_info.from_telephone
                };
                Address toAddress = new Address() {
                    name=ep.receiver_info.to_name,
                    company=ep.receiver_info.to_name,
                    street1=ep.receiver_info.to_address_1,
                    street2=ep.receiver_info.to_address_2,
                    city=ep.receiver_info.to_city,
                    state=ep.receiver_info.to_nation_state,
                    zip=ep.receiver_info.to_zipcode
                };
                try {
                    toAddress.Verify();
                } catch(Exception e) { Console.WriteLine(e.Message); }


                Parcel parcel = Parcel.Create(new Dictionary<string,object>() {
                    {"length", int.Parse( ep.parcel_info.pkg_length)},
                    { "width", int.Parse(ep.parcel_info.pkg_width)},
                    { "height",int.Parse( ep.parcel_info.pkg_height)},
                    { "weight",int.Parse( ep.parcel_info.pkg_weight)}
                });

                Shipment shipment = new Shipment() {
                    to_address=toAddress,
                    from_address=fromAddress,
                    parcel=parcel
                };

                shipment.Create();
                //List<string> rateList = new List<string> { };
                foreach(Rate rate in shipment.rates) {
                    //System.Console.WriteLine(rate.carrier);
                    //System.Console.WriteLine(rate.service);
                    //rateList.Add
                    //rateList.Add(rate.rate);
                    //System.Console.WriteLine(rate.id);
                }

                _lookupPrice=shipment;
                return _lookupPrice;
            }
            set { }
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

        [Display(Name = "Order ID")]
        public int order_id { get; set; }

        [ForeignKey("order_id")]
        public virtual OrderHistory OrderHistory { get; set; }

        [Display(Name = "User ID")]
        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual AspNetUser User { get; set; }

    }
}