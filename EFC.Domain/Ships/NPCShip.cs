using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public class NPCShip : IShip
    {
        public int Angle { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Race { get; set; }
        public string Hull { get; set; }
        public int Side { get; set; }
        public List<XElement> CreationXML
        {
            get
            {
                List<XElement> result = new List<XElement>();
                result.Add(
                    new XElement("create",
                        new XAttribute("angle", this.Angle),
                        new XAttribute("name", this.Name),
                        new XAttribute("z", this.Position.Z),
                        new XAttribute("y", this.Position.Y),
                        new XAttribute("x", this.Position.X),
                        new XAttribute("raceKeys", this.Race),
                        new XAttribute("hullKeys", this.Hull),
                        new XAttribute("sideValue", this.Side),
                        new XAttribute("type", "neutral")));
                result.Add(
                    new XElement("set_variable",
                        new XAttribute("name", this.Name + "Death"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")));
                result.Add(
                    new XElement("log",
                        new XAttribute("text", "NPC Ship: " + this.Name + " created")));
                return result;
            }
        }

        public XElement DestructionXML
        {
            get
            {
                return new XElement("event",
                    new XAttribute("name", this.Name + " Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", this.Name + "Death"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", this.Name)),
                    new XElement("log",
                        new XAttribute("text", this.Name + " destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", this.Name + "Death"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes")));
            }
        }

    }
}
