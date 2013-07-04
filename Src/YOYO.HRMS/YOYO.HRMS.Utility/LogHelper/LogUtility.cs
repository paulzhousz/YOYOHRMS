using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace YOYO.HRMS.Utility
{
    public class LogUtility
    {
        public static string BuildExceptionMessage(Exception x)
        {

            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = Environment.NewLine + "Error in Path :" + System.Web.HttpContext.Current.Request.Path;

            // Get the QueryString along with the Virtual Path
            strErrorMsg += Environment.NewLine + "Raw Url :" + System.Web.HttpContext.Current.Request.RawUrl;

            // Get the error message
            strErrorMsg += Environment.NewLine + "Message :" + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "Source :" + logException.Source;

            // Stack Trace of the error

            strErrorMsg += Environment.NewLine + "Stack Trace :" + logException.StackTrace;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "TargetSite :" + logException.TargetSite;
            return strErrorMsg;
        }

        public static string BuildWebVariablesMessage()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);

            writer.WriteStartElement("WebVariables");

            // -----------------------------------------
            // Server Variables
            // -----------------------------------------
            writer.WriteStartElement("serverVariables");

            foreach (string key in HttpContext.Current.Request.ServerVariables.AllKeys)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", key);

                writer.WriteStartElement("value");
                writer.WriteAttributeString("string", HttpContext.Current.Request.ServerVariables[key].ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();



            writer.WriteStartElement("Session");

            foreach (string key in HttpContext.Current.Session.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", key);

                writer.WriteStartElement("value");
                writer.WriteAttributeString("string", HttpContext.Current.Session[key].ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // -----------------------------------------
            // Cookies
            // -----------------------------------------
            writer.WriteStartElement("cookies");

            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", key);

                writer.WriteStartElement("value");
                writer.WriteAttributeString("string", HttpContext.Current.Request.Cookies[key].Value.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            // -----------------------------------------

            writer.WriteEndElement();
            // -----------------------------------------

            writer.Flush();
            writer.Close();

            string xml = sb.ToString();
            return xml;
        }
    }
}
