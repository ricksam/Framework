﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace lib.Class
{
    public class XML
    {
        public string Serialize(object o)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer xml_serializer = new System.Xml.Serialization.XmlSerializer(o.GetType());
            xml_serializer.Serialize(writer, o);
            return writer.ToString();
        }

        public T Deserialize<T>(string string_xml)
        {
            System.Xml.XmlReader xml_reader = System.Xml.XmlReader.Create(new System.IO.StringReader(string_xml));
            System.Xml.Serialization.XmlSerializer xml_serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)xml_serializer.Deserialize(xml_reader);
        }
    }
}
