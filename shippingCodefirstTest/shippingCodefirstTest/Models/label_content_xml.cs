using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

//namespace shippingCodefirstTest.Models {
//    [Obsolete]
//    public class label_content_xml {
//        public label_content_xml() {
//            xml=new XElement("root");
//            //from_info=new Dictionary<string,string> { };
//            to_info=new Dictionary<string,string> { };
//            parcel_info=new Dictionary<string,string> { };
            
//        }
//        //public Dictionary<string,string> from_info { get; set; }
//        public Dictionary<string,string> to_info { get; set; }
//        public Dictionary<string,string> parcel_info { get; set; }
//        public XElement xml { get; set; }
//        public Label l { get; set; }

//        public XElement generateXml() {

            
//            XElement from_Node = new XElement("sender_info");
//            XElement to_Node = new XElement("receiver_info");
//            XElement parcel_Node = new XElement("parcel_info");
//            XElement shipping_Node = new XElement("shipping_info");
//            //foreach(var node in from_info) {
//            //    XElement tempNode = new XElement(node.Key);
//            //    tempNode.Value=node.Value;
//            //    from_Node.Add(tempNode);
//            //}
//            foreach(var node in to_info) {
//                XElement tempNode = new XElement(node.Key);
//                tempNode.Value=node.Value;
//                to_Node.Add(tempNode);
//            }
//            foreach(var node in parcel_info) {
//                XElement tempNode = new XElement(node.Key);
//                tempNode.Value=node.Value;
//                parcel_Node.Add(tempNode);
//            }
//            shipping_Node.Add(new XElement("tracking_code") {Value="0" } );
//            shipping_Node.Add(new XElement("shipping_label_address") { Value="0"});
//            xml.Add(from_Node);
//            xml.Add(to_Node);
//            xml.Add(parcel_Node);
//            xml.Add(shipping_Node);
//            return xml;
//        }

//    }
//}