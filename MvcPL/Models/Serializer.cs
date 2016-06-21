using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using MvcPL.Models.Photo;

namespace MvcPL.Models
{
    public static class Serializer
    {
        public static string ToString(List<PhotoViewModel> list)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, list);
                return textWriter.ToString();
            }
        }

        public static List<PhotoViewModel> FromString(string item)
        {
            var serializer = new XmlSerializer(typeof(List<PhotoViewModel>));
            List<PhotoViewModel> result;

            using (TextReader reader = new StringReader(item))
            {
                result = (List<PhotoViewModel>)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}