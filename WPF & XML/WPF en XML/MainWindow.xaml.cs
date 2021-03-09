using System;
using System.Windows;
using System.Xml;

namespace WPF_en_XML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string pad = Environment.CurrentDirectory + @"\berichten.xml";

        private void btnXmlReader_Click(object sender, RoutedEventArgs e)
        {
            using (XmlReader xmlIn = XmlReader.Create(pad))
            {

                while (xmlIn.Read())
                {
                    if (xmlIn.NodeType == XmlNodeType.Element)
                    {
                        listXML.Items.Add("START EL: " + xmlIn.Name);
                        if (xmlIn.GetAttribute("tijd") != null)
                        {
                            listXML.Items.Add("ATTR: " + xmlIn.GetAttribute("tijd"));
                        }
                    }
                    else if (xmlIn.NodeType == XmlNodeType.Text)
                    {
                        listXML.Items.Add(xmlIn.Value);
                    }
                    else if (xmlIn.NodeType == XmlNodeType.EndElement)
                    {
                        listXML.Items.Add("END EL: " + xmlIn.Name);
                    }
                }

            }
        }

        private void btnXmlDom_Click(object sender, RoutedEventArgs e)
        {
            listXML.Items.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pad);


            listXML.Items.Add(xmlDoc.ChildNodes[0].Name); //xml
            listXML.Items.Add(xmlDoc.DocumentElement.Name); //berichten

            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].Attributes[0].Name); //tijd
            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].Attributes[0].Value); //12:03


            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[0].Name);
            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].LastChild.InnerText);
            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].FirstChild.Name);
            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2].PreviousSibling.Name);
            listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[1].ParentNode.Name);

            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes[0].ChildNodes.Count; i++)
            {
                listXML.Items.Add(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[i].Name);

            }
        }

        private void btnXmlAanpassen_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pad);
            //verwijder kind[2] -> komt overeen met wanneer
            xmlDoc.DocumentElement.ChildNodes[0].RemoveChild(xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2]);

            XmlElement newElement = null;
            XmlText newText = null;

            newElement = xmlDoc.CreateElement("cc");
            newElement.SetAttribute("reden", "Ter info");
            newText = xmlDoc.CreateTextNode("Hans");
            newElement.AppendChild(newText);
            xmlDoc.DocumentElement.ChildNodes[0].AppendChild(newElement);

            //xmlDoc.DocumentElement.ChildNodes[0].InsertBefore(newElement, xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2]);
            //xmlDoc.DocumentElement.ChildNodes[0].ReplaceChild(newElement, xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[2]);

            xmlDoc.Save(Environment.CurrentDirectory + @"\berichten.xml");
        }

        private void btnXmlNieuw_Click(object sender, RoutedEventArgs e)
        {
            using (XmlWriter writer = XmlWriter.Create(Environment.CurrentDirectory + @"\uitvoer.xml"))
            {
                writer.WriteStartElement("berichten");
                writer.WriteStartElement("nota");
                writer.WriteAttributeString("tijd", DateTime.Now.ToShortTimeString());
                writer.WriteElementString("van", "kerstman");
                writer.WriteElementString("aan", "Arno");
                writer.WriteElementString("onderwerp", "Playstation 5 is overal uitverkocht.");
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private void btnBerichtInlezen_Click(object sender, RoutedEventArgs e)
        {
            //Let op XML != HTML 
            webBrowser1.Navigate(pad);
        }
    }
}
