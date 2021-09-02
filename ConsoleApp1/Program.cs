using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static List<Data> NewDataSet = new List<Data> { new Data { Id= "{1d22ea11-1e32-424e-89ab-9fedbadb6ce1}", AnalystLegacy="Test",CreatedBy="Test",InHouseCouncil="Test",Title="Test" } };
        static void Main(string[] args)
        {
            var filterNameList = new List<string> { "Analyst_x0020__x0028_Legacy_x002", "Created_x0020_By_x0020__x0028_Le", "In_x002d_House_x0020_Council_x00", "ID", "Title" };

            XmlDocument doc = new XmlDocument();


            string oldUrl = "http://intranet-old/legal/_vti_bin/Lists.asmx";
            const string listName = "NDA Log"; // "{BDE7884E-9056-48DD-A6D9-9D54CED47A85}";// "NDA Log";

            //var client = new ListServiceProxy.ListsSoapClient("ListsSoap", oldUrl);
            //var query = new XElement("Query", "");
            //var viewFields = new XElement("ViewFields", "");
            //var queryOptions = new XElement("QueryOptions", "");

            try
            {
                var result = File.ReadAllText("XMLFile1.xml");
                //var result = client.GetList(listName);
                doc.LoadXml(result);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                var data = JsonConvert.DeserializeObject<Respose>(jsonText);
                
                foreach(var d in data.Data.Rows)
                {
                    var analyst = d.Analysts;
                    var id = d.ID;
                    // do the logic here....
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
    }
    class Data
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AnalystLegacy { get; set; }
        public string CreatedBy { get; set; }
        public string InHouseCouncil { get; set; }
    }
    class Respose
    {
        [JsonProperty("@xmlns:s")]
        public string S { get; set; }
        [JsonProperty("@xmlns:dt")]
        public string DT { get; set; }
        [JsonProperty("@xmlns:rs")]
        public string RS { get; set; }
        [JsonProperty("@xmlns:z")]
        public string Z { get; set; }
        [JsonProperty("rs:data")]
        public RSData Data { get; set; } 
    }
    class RSData
    {
        [JsonProperty("@ItemCount")]
        public int ItemCount { get; set; }
        [JsonProperty("@ListItemCollectionPositionNext")]
        public string NextPosition { get; set; }
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
   
}

