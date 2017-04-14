using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public class PlayerShip: IShip
    {
        public int Angle { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Slot { get; set; }
        public ShipType ShipType { get; set; }
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
                        new XAttribute("player_slot", Slot),
                        new XAttribute("raceKeys","TSN player"),
                        new XAttribute("hullKeys",GetShipType() + " player"),
                        new XAttribute("type", "player")));
                result.Add(
                    new XElement("set_variable",
                        new XAttribute("name", Name + "Death"),
                        new XAttribute("value","0"),
                        new XAttribute("integer","yes")));
                result.Add(
                    new XElement("log",
                        new XAttribute("text", "Player Ship: " + Name + " created")));
                return result;
            }
        }

        private string GetShipType()
        {
            string result = "Light Cruiser";

            switch (this.ShipType)
            {
                case ShipType.Scout:
                    result = "Scout";
                    break;
                case ShipType.LightCruiser:
                    result = "Light Cruiser";
                    break;
                case ShipType.Battleship:
                    result = "Battleship";
                    break;
                case ShipType.MissileCruiser:
                    result = "Missile Cruiser";
                    break;
                case ShipType.Dreadnought:
                    result = "Dreadnought";
                    break;
                case ShipType.Carrier:
                    result = "Carrier";
                    break;
                case ShipType.MineLayer:
                    result = "Mine Layer";
                    break;
                case ShipType.Juggernaut:
                    result = "Juggernaut";
                    break;
                default:
                    break;
            }

            return result;
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
                        new XAttribute("value","0")),
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

    }

   public enum ShipType
    {
        Scout,
        LightCruiser,
        Battleship,
        MissileCruiser,
        Dreadnought,
        Carrier,
        MineLayer,
        Juggernaut
    }
}