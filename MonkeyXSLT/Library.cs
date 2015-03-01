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
        public static XPathNodeIterator MailchimpSubscribe(string mail, string listId, string APIKeyAlias)
        {
            try
            {
                string apiKey = ConfigurationManager.AppSettings[APIKeyAlias];
                MailChimpManager MC = new MailChimpManager(apiKey);
                EmailParameter email = new EmailParameter()
                {
                    Email = mail
                };
                EmailParameter results = MC.Subscribe(listId, email, null, "html", false, true, true, false);

                string xmlVal = "<status success=\"true\">" + results.Email + "</status>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlVal);
                return doc.CreateNavigator().Select(".");

            }
            catch (Exception ex)
            {
                return GetErrorXML(ex.Message);
            }
        }

        public static XPathNodeIterator MailchimpUnsubscribe(string mail, string listId, string APIKeyAlias)
        {
            try
            {
                string apiKey = ConfigurationManager.AppSettings[APIKeyAlias];
                MailChimpManager MC = new MailChimpManager(apiKey);
                EmailParameter email = new EmailParameter()
                {
                    Email = mail
                };
                UnsubscribeResult results = MC.Unsubscribe(listId, email, false, false, false);

                string xmlVal = "<status success=\"true\">" + results.Complete.ToString() + "</status>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlVal);
                return doc.CreateNavigator().Select(".");

            }
            catch (Exception ex)
            {
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