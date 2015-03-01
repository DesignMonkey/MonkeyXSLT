using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailChimp;
using MailChimp.Lists;
using MailChimp.Helper;
using System.Xml;
using System.Configuration;
using System.Xml.XPath;

namespace MonkeyXSLT {
    public class Library {
        public static XPathNodeIterator AddToMailchimp(string mail, string name, string listId, string APIKeyAlias) {
            try {
                string apiKey = ConfigurationManager.AppSettings[APIKeyAlias];
                string dc = apiKey.Substring(apiKey.Length - 3);
                /*
                string url = "http://" + dc + ".api.mailchimp.com/1.3/?method=listSubscribe" +
                    "&apikey=" + apiKey +
                        "&id=" + listId +
                        "&email_address=" + mail +
                        "&merge_vars[NAME]=" + name +
                        "&output=json";

                APICall api = new APICall(url);
                string response = api.GetResponse();
                */

                var response = "der";
                string xmlVal = "<status success=\"true\">" + response + "</status>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlVal);
                return doc.CreateNavigator().Select(".");

            }
            catch (Exception ex) {
                return GetErrorXML(ex.Message);
            }
        }

        private static XPathNodeIterator GetErrorXML(string Error) {
            string xmlVal = "<status success=\"false\">" + Error + "</status>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlVal);
            return doc.CreateNavigator().Select(".");
        }
        
    }
}