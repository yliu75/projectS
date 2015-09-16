﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace shippingCodefirstTest.Controllers {
    public class XmlHelper {
        public static string Serialize<T>(T dataToSerialize) {
            try {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter,dataToSerialize);
                return stringwriter.ToString();
            } catch {
                throw;
            }
        }

        public static T Deserialize<T>(string xmlText) {
            try {
                var stringReader = new System.IO.StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            } catch {
                throw;
            }
        }
    }

   
}