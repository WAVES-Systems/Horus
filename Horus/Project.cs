using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Waves.Horus
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public List<Sample> Samples { get; set; } = new List<Sample>();

        public Project(string name, string description, string id)
        {
            this.Name = name;
            this.Description = description;
            this.Id = id;
        }
        public Project()
        {

        }

        public static Project Load(string path)
        {
            Project result = new Project();
            XmlDocument file = new XmlDocument();
            file.Load(path);
            XmlNode node = file.SelectSingleNode("/project");
            result.Name = Utils.LoadAttribute(node, "name", "");
            result.Description = Utils.LoadAttribute(node, "description", "");
            result.Id = Utils.LoadAttribute(node, "id", "");
            node = file.SelectSingleNode("/project/collections");
            if (node != null)
            {
                foreach (XmlNode bacuriNode in node.ChildNodes)
                {
                    result.Samples.Add(new Sample(Utils.LoadAttribute(bacuriNode, "id", ""), Utils.LoadAttribute(bacuriNode, "path", "")));
                }
            }
            return result;
        }

        public static bool Write(Project source, string path)
        {
            try
            {
                XmlDocument file = new XmlDocument();
                XmlNode node = file.CreateXmlDeclaration("1.0", "UTF-8", null);
                file.AppendChild(node);

                node = file.CreateElement("project");
                XmlAttribute attribute = file.CreateAttribute("name");
                attribute.InnerText = source.Name;
                node.Attributes.Append(attribute);
                attribute = file.CreateAttribute("description");
                attribute.InnerText = source.Description;
                node.Attributes.Append(attribute);
                attribute = file.CreateAttribute("id");
                attribute.InnerText = source.Id;
                node.Attributes.Append(attribute);



                XmlNode collections = file.CreateElement("collections");

                foreach (Sample bacuriCollect in source.Samples)
                {
                    XmlNode collection = file.CreateElement("collection");
                    attribute = file.CreateAttribute("id");
                    attribute.InnerText = source.Description;
                    node.Attributes.Append(attribute);
                    attribute = file.CreateAttribute("path");
                    attribute.InnerText = source.Id;
                    node.Attributes.Append(attribute);
                    collections.AppendChild(collection);
                }

                node.AppendChild(collections);

                file.AppendChild(node);

                file.Save(path);

                return true;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error writing file!\n" + e.Message, "Save project", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return false;
        }
    }
}
