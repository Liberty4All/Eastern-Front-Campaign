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
        public List<EnhancementType> Enhancements { get; set; }
        public List<XElement> CreationXML
        {
            get
            {
                List<XElement> result = new List<XElement>
                {
                    new XElement("create",
                        new XAttribute("angle", Angle),
                        new XAttribute("name", Name),
                        new XAttribute("z", Position.Z),
                        new XAttribute("y", Position.Y),
                        new XAttribute("x", Position.X),
                        new XAttribute("player_slot", Slot),
                        new XAttribute("raceKeys", "TSN player"),
                        new XAttribute("hullKeys", GetShipType() + " player"),
                        new XAttribute("type", "player")),
                    new XElement("set_variable",
                        new XAttribute("name", Name + "Death"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                    new XElement("log",
                        new XAttribute("text", "Player Ship: " + Name + " created"))
                };
                return result;
            }
        }

        public PlayerShip()
        {
            Enhancements = new List<EnhancementType>();
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

        public List<XElement> AddEnhancement(EnhancementType enhancementType)
        {
            List<XElement> result = new List<XElement>();

            switch (enhancementType)
            {
                case EnhancementType.ThetaGenerator:
                    result = GetThetaGenerator();
                    break;
                default:
                    break;
            }
            return result;
        }

        private List<XElement> GetThetaGenerator()
        {
            List<XElement> result = new List<XElement>();
            result.Add(new XElement("folder_arme",
                new XAttribute("name_arme", "Fuel Collection System"),
                new XAttribute("id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1")));
            result.Add(new XElement("event",
                new XAttribute("name_arme", "Activate Fuel Collectors"),
                new XAttribute("id_arme", "fb894052-2832-46df-9d20-0819853aeb2d"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XElement("if_variable",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0.0")),
                new XElement("if_client_key",
                    new XAttribute("keyText", "R")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "Fuel Collectors Activated"),
                    new XAttribute("consoles", "E")),
                new XElement("set_variable",
                    new XAttribute("name", "Collection Bonus"),
                    new XAttribute("value", "1.0")),
                new XElement("set_timer",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("seconds", "3")),
                new XElement("set_timer",
                    new XAttribute("name", "Fuel Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Fuel Collector Limits"),
                    new XAttribute("value", "1.0")),
                new XElement("set_variable",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("value", "1.0"))));
            result.Add(new XElement("event",
                new XAttribute("name_arme", "Fuel Collectors Active"),
                new XAttribute("id_arme", "91a28c7e-b444-4886-aa8c-15ba839ce06f"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XElement("if_variable",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_object_property",
                    new XAttribute("property", "energy"),
                    new XAttribute("name", this.Name),
                    new XAttribute("comparator", "LESS_EQUAL"),
                    new XAttribute("value", "981.0")),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", this.Name),
                    new XAttribute("comparator", "LESS"),
                    new XAttribute("value", "0.5")),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", this.Name),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.1")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Fuel Collection")),
                new XElement("if_object_property",
                    new XAttribute("property", "shieldsOn"),
                    new XAttribute("name", this.Name),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0.0")),
                new XElement("addto_object_property",
                    new XAttribute("value", "20.0"),
                    new XAttribute("property", "energy"),
                    new XAttribute("name", this.Name)),
                  new XElement("set_timer",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("seconds", "3"))));
            result.Add(new XElement("event",
                new XAttribute("name_arme", "Deactivate Fuel Collectors"),
                new XAttribute("id_arme", "e29f5dc6-4bb2-4ee2-9c5a-769fb5f9959e"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XElement("if_variable",
                    new XAttribute("name", "Fuel Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_client_key",
                    new XAttribute("keyText", "T")),
                new XElement("set_variable",
                    new XAttribute("name", "Fuel Collection")),
                    new XAttribute("value", "0.0"),
                new XElement("set_variable",
                    new XAttribute("name", "Collection Bonus"),
                    new XAttribute("value", "0.0")),
                new XElement("set_variable",
                    new XAttribute("name", "Fuel Collector Limits"),
                    new XAttribute("value", "0.0"))));

            return result;
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