using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument doc = new XmlDocument();


           

            try
            {
                #region xml with newtonsoftjson
                var result = File.ReadAllText("XMLFile1.xml");
                doc.LoadXml(result);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                var data = JsonConvert.DeserializeObject<ListItems>(jsonText);
                
                foreach(var d in data.Data.Rows)
                {
                    var analyst = d.Analysts;
                    var id = d.ID;
                    // do the logic here....
                }
                #endregion

                #region xml with builtin serializer

                CarCollection cars = null;

                XmlSerializer serializer = new XmlSerializer(typeof(CarCollection));

                StreamReader reader = new StreamReader("XMLFile2.xml");
                cars = (CarCollection)serializer.Deserialize(reader);
                reader.Close();

                #endregion

                #region File1Serializing
                XmlSerializer newser = new XmlSerializer(typeof(ListItemsXML));
                StreamReader sr = new StreamReader("XMLFile1.xml");
                var newway = (ListItemsXML)newser.Deserialize(sr);
                #endregion

                #region JSON string deserializer
                var text = File.ReadAllText("JSONFile1.json");
                var student = JsonConvert.DeserializeObject<List<Student>>(text);
                #endregion

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
    }

    [XmlRoot("listitems")]
    class ListItemsXML
    {
        [System.Xml.Serialization.XmlArray("rs:data")]
        [System.Xml.Serialization.XmlArrayItem("z:row",typeof(RowXML))]
        public RowXML[] Data { get; set; }
    }
    
    class RowXML
    {
        [System.Xml.Serialization.XmlAttribute("ows_ID")]
        public int ID { get; set; }
        [System.Xml.Serialization.XmlAttribute("ows_Analysts")]
        public string Analysts { get; set; }
        [System.Xml.Serialization.XmlAttribute("ows_Author")]
        public string Author { get; set; }
        [System.Xml.Serialization.XmlAttribute("ows_Title")]
        public string Title { get; set; }
        [System.Xml.Serialization.XmlAttribute("ows_Farallon Attorneys")]
        public string FarallonAttorney { get; set; }
    }
    class ListItems
    {
        //[JsonProperty("@xmlns:s")]
        //public string S { get; set; }
        //[JsonProperty("@xmlns:dt")]
        //public string DT { get; set; }
        //[JsonProperty("@xmlns:rs")]
        //public string RS { get; set; }
        //[JsonProperty("@xmlns:z")]
        //public string Z { get; set; }
        [JsonProperty("rs:data")]
        public RSData Data { get; set; } 
    }
    class RSData
    {
        [JsonProperty("@ItemCount")]
        public int ItemCount { get; set; }
        //[JsonProperty("@ListItemCollectionPositionNext")]
        //public string NextPosition { get; set; }
        [JsonProperty("z:row")]
        public List<Row> Rows { get; set; }
    }
    class Row
    {
        [JsonProperty("@ows_ID")]
        public int ID { get; set; }
        [JsonProperty("@ows_Analysts")]
        public string Analysts { get; set; }
        [JsonProperty("@ows_Author")]
        public string Author { get; set; }
        [JsonProperty("@ows_Title")]
        public string Title { get; set; }
        [JsonProperty("@ows_Farallon Attorneys")]
        public string FarallonAttorney { get; set; }
    }


    public class Car
    {
        [System.Xml.Serialization.XmlAttribute("StockName")]
        public string StockName { get; set; }
        //[System.Xml.Serialization.XmlElement("StockNumber")]
        public string StockNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }


    [System.Xml.Serialization.XmlRoot("CarCollection")]
    public class CarCollection
    {
        [XmlArray("Cars")]
        [XmlArrayItem("Car", typeof(Car))]
        public Car[] Car { get; set; }
    }


    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Blood { get; set; }
        public List<StuddentScore> Scores { get; set; }
    }
    public class StuddentScore
    {
        public string Subject { get; set; }
        public int Score { get; set; }
    }

}

