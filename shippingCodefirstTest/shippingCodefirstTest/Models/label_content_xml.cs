using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace shippingCodefirstTest.Models {
    public class label_content_xml {
        public label_content_xml() {
            xml=new XElement("root");
            from_info=new Dictionary<string,string> { };
            to_info=new Dictionary<string,string> { };
            
        }
        public Dictionary<string,string> from_info { get; set; }
        public Dictionary<string,string> to_info { get; set; }
        public XElement xml { get; set; }
        public Label l { get; set; }

        public XElement generateXml() {

            
            XElement from_Node = new XElement("sender_info");
            XElement to_Node = new XElement("receiver_info");

            foreach(var node in from_info) {
                XElement tempNode = new XElement(node.Key);
                tempNode.Value=node.Value;
                from_Node.Add(tempNode);
            }
            foreach(var node in to_info) {
                XElement tempNode = new XElement(node.Key);
                tempNode.Value=node.Value;
                to_Node.Add(tempNode);
            }
            xml.Add(from_Node);
            xml.Add(to_Node);
            return xml;
        }

    }
}