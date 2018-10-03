namespace RL.Services.ServicesExtensions {
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public static class ServiceHelper {
        public static string Serialize<T>(this T value) {

            if(value == null)
                return string.Empty;

            try {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using(var writer = XmlWriter.Create(stringWriter)) {
                    xmlSerializer.Serialize(
                        writer
                        , value);
                    return stringWriter.ToString();
                }
            }
            catch(Exception ex) {
                throw new Exception(
                    "An error occurred"
                    , ex);
            }
        }
    }
}