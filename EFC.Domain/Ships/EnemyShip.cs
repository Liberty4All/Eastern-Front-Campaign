using System.Collections.Generic;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public class EnemyShip: IShip
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Race { get; set; }
        public string Hull { get; set; }
        public int Angle { get; set; }
        public int Fleet { get; set; }
        public int Side { get; set; }

        public List<XElement> CreationXML
        {
            get
            {
                List<XElement> result = new List<XElement>();
                result.Add(
                    new XElement("create",
                        new XAttribute("angle", Angle),
                        new XAttribute("name", Name),
                        new XAttribute("z", Position.Z),
                        new XAttribute("y", Position.Y),
                        new XAttribute("x", Position.X),
                        new XAttribute("raceKeys", Race),
                        new XAttribute("hullKeys", Hull),
                        new XAttribute("fleetNumber", Fleet),
                        new XAttribute("sideValue",Side),
                        new XAttribute("type", "enemy")));
                result.Add(
                    new XElement("set_variable",
                        new XAttribute("name", Name + "Death"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")));
                result.Add(
                    new XElement("set_variable",
                        new XAttribute("name", Name + "Surrendered"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")));
                result.Add(
                    new XElement("log",
                        new XAttribute("text", "Enemy Ship: " + Name + " created")));
                return result;
            }
        }

        public XElement DestructionXML
        {
            get
            {
                return new XElement("event",
                    new XAttribute("name", Name + " Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", Name + "Death"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", this.Name)),
                    new XElement("log",
                        new XAttribute("text", this.Name + " destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", Name + "Death"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes")));
            }
        }

        public List<XElement> SurrenderXML
        {
            get
            {
                List<XElement> result = new List<XElement>();
                result.Add(
                    new XElement("event",
                        new XAttribute("name_arme",Name + " Surrendered"),
                        new XElement("if_variable",
                            new XAttribute("name",Name + "Surrendered"),
                            new XAttribute("comparator","EQUALS"),
                            new XAttribute("value","0")),
                        new XElement("if_object_property",
                            new XAttribute("name",Name),
                            new XAttribute("property","hasSurrendered"),
                            new XAttribute("comparator", "EQUALS"),
                            new XAttribute("value", "1.0")),
                        new XElement("log",
                            new XAttribute("text",Name + " has surrendered")),
                        new XElement("set_variable",
                            new XAttribute("name",Name + "Surrendered"),
                            new XAttribute("value","1"),
                            new XAttribute("integer","yes"))));
                result.Add(
                    new XElement("event",
                        new XAttribute("name_arme", Name + " Destroyed After Surrender"),
                        new XElement("if_variable",
                            new XAttribute("name", Name + "Surrendered"),
                            new XAttribute("comparator", "EQUALS"),
                            new XAttribute("value", "1")),
                        new XElement("if_variable",
                            new XAttribute("name", Name + "Death"),
                            new XAttribute("comparator", "EQUALS"),
                            new XAttribute("value", "1")),
                        new XElement("log",
                            new XAttribute("text", Name + " destroyed after it surrendered")),
                        new XElement("set_variable",
                            new XAttribute("name", Name + "Surrendered"),
                            new XAttribute("value", "2"),
                            new XAttribute("integer", "yes")),
                        new XElement("set_variable",
                            new XAttribute("name", Name + "Death"),
                            new XAttribute("value", "2"),
                            new XAttribute("integer", "yes"))));

                return result;
            }
        }
    }
}