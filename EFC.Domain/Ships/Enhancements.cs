﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public class ThetaGeneratorEnhancement : IEnhancement
    {
        public EnhancementType EnhancementType { get => EnhancementType.ThetaGenerator; }
        public string Name { get => "Theta Generator"; }

        public List<XElement> StartXML => new List<XElement>() {
            new XElement("set_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("value", "0.0")),
            new XElement("set_comms_button",
                new XAttribute("text", "Activate Theta Collector"))
        };

        public List<XElement> BodyXML => new List<XElement>() {
            new XElement("folder_arme",
                new XAttribute("name_arme", "Theta Collection System"),
                new XAttribute("id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1")),
            new XElement("event",
                new XAttribute("name_arme", "Activate Theta Collectors"),
                new XAttribute("id_arme", "fb894052-2832-46df-9d20-0819853aeb2d"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0.0")),
                new XElement("if_comms_button",
                    new XAttribute("text", "Activate Theta Collector")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "Theta Collectors Activated"),
                    new XAttribute("consoles", "E")),
                new XElement("set_variable",
                    new XAttribute("name", "Collection Bonus"),
                    new XAttribute("value", "1.0")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("seconds", "3")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "1.0")),
	            new XElement("set_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("value", "1.0")),
                new XElement("clear_comms_button",
                    new XAttribute("text", "Activate Theta Collector")),
                new XElement("set_comms_button",
                    new XAttribute("text", "Deactivate Theta Collector"))),
            new XElement("event",
                new XAttribute("name_arme", "Theta Collectors Active"),
                new XAttribute("id_arme", "91a28c7e-b444-4886-aa8c-15ba839ce06f"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_object_property",
                    new XAttribute("property", "energy"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "LESS_EQUAL"),
                    new XAttribute("value", "981.0")),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "LESS"),
                    new XAttribute("value", "0.5")),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.1")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collection")),
                new XElement("if_object_property",
                    new XAttribute("property", "shieldsOn"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0.0")),
                new XElement("addto_object_property",
                    new XAttribute("value", "20.0"),
                    new XAttribute("property", "energy"),
                    new XAttribute("name", "{ShipName}")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("seconds", "3"))),
            new XElement("event",
                new XAttribute("name_arme", "Deactivate Theta Generator"),
                new XAttribute("id_arme", "e29f5dc6-4bb2-4ee2-9c5a-769fb5f9959e"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XAttribute("value", "0.0"),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_comms_button",
                    new XAttribute("text", "Deactivate Theta Collector")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "Theta Collectors Deactivated"),
                    new XAttribute("consoles", "E")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("value", "0.0")),
                new XElement("set_variable",
                    new XAttribute("name", "Collection Bonus"),
                    new XAttribute("value", "0.0")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "0.0")),
                new XElement("clear_comms_button",
                    new XAttribute("text", "Deactivate Theta Collector")),
                new XElement("set_comms_button",
                    new XAttribute("text", "Activate Theta Collector"))),
            new XElement("folder_arme",
                new XAttribute("name_arme", "Theta Generator System Limits"),
                new XAttribute("id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XAttribute("parent_id_arme", "1b665acb-40f7-4ca9-883b-88e1c75586f1"),
                new XAttribute("expanded_arme", "")),
            new XElement("event",
                new XAttribute("name_arme", "Damage 1"),
                new XAttribute("id_arme", "4c969593-9bb4-460f-b121-4b10f1c4dc4c"),
                new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.8")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0"),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collector Limits")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "WARNING: Theta Collection systems still active."),
                    new XAttribute("consoles", "HE")),
                new XElement("set_player_grid_damage",
                    new XAttribute("value", "1.0" ),
                    new XAttribute("index", "1"),
                    new XAttribute("systemType", "systemWarp"),
                    new XAttribute("countFrom", "front")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "2.0"))),
            new XElement("event",
                new XAttribute("name_arme", "Damage 2"),
                new XAttribute("id_arme", "5295247c-a42b-4586-b646-e1e6c0c41fdc"),
                new XAttribute("parent_id_arme" ,"33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.8")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "2.0")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collector Limits")),
                new XElement("set_player_grid_damage",
                    new XAttribute("value", "1.0"),
                    new XAttribute("index", "0"),
                    new XAttribute("systemType", "systemWarp"),
                    new XAttribute("countFrom", "front")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "3.0"))),
            new XElement("event",
                new XAttribute("name_arme", "Damage 3"),
                new XAttribute("id_arme", "3197aec4-5916-4fed-b9b8-ae94fdc77dc3"),
                new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.8")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "3.0")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collector Limits")),
                new XElement("set_player_grid_damage",
                    new XAttribute("value", "1.0"),
                    new XAttribute("index", "2"),
                    new XAttribute("systemType", "systemWarp"),
                    new XAttribute("countFrom", "front")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "4.0"))),
            new XElement("event",
                new XAttribute("name_arme", "Damage 4"),
                new XAttribute("id_arme", "e0c03179-7f3f-40ed-a9ac-63f01a25b117"),
                new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.8")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value","1.0")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "4.0")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collector Limits")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "WARNING: Theta Collection systems still active."),
                    new XAttribute("consoles", "HE")),
                new XElement("set_player_grid_damage",
                    new XAttribute("value", "1.0"),
                    new XAttribute("index", "3"),
                    new XAttribute("systemType", "systemWarp"),
                    new XAttribute("countFrom", "front")),
                new XElement("set_timer",
                    new XAttribute("name","Theta Collector Limits"),
                    new XAttribute("seconds","3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "5.0"))),
            new XElement("event",
                new XAttribute("name_arme", "Damage 5"),
                new XAttribute("id_arme", "7bcdfa93-60ed-423f-82f0-a7a4b27c30a9"),
                new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
                new XElement("if_object_property",
                    new XAttribute("property", "throttle"),
                    new XAttribute("name", "{ShipName}"),
                    new XAttribute("comparator", "GREATER"),
                    new XAttribute("value", "0.8")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collection"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "1.0")),
                new XElement("if_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "5.0")),
                new XElement("if_timer_finished",
                    new XAttribute("name", "Theta Collector Limits")),
                new XElement("warning_popup_message",
                    new XAttribute("message", "WARNING: Theta Collection systems still active."),
                    new XAttribute("consoles", "HE")),
                new XElement("set_player_grid_damage",
                    new XAttribute("value","1.0"),
                    new XAttribute("index", "1"),
                    new XAttribute("systemType", "systemImpulse"),
                    new XAttribute("countFrom","front")),
                new XElement("set_timer",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("seconds", "3")),
                new XElement("set_variable",
                    new XAttribute("name", "Theta Collector Limits"),
                    new XAttribute("value", "6.0"))),
        new XElement("event",
            new XAttribute("name_arme", "Damage 6"),
            new XAttribute("id_arme", "972f15b7-03ad-4351-95a9-38931fdc8ee5"),
            new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
            new XElement("if_object_property",
                new XAttribute("property", "throttle"),
                new XAttribute("name", "{ShipName}"),
                new XAttribute("comparator", "GREATER"),
                new XAttribute("value", "0.8")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "1.0")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "6.0")),
            new XElement("if_timer_finished",
                new XAttribute("name", "Theta Collector Limits")),
            new XElement("set_player_grid_damage",
                new XAttribute("value", "1.0"),
                new XAttribute("index", "0"),
                new XAttribute("systemType", "systemImpulse"),
                new XAttribute("countFrom", "front")),
            new XElement("set_timer",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("seconds", "3")),
            new XElement("set_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("value", "7.0"))),
        new XElement("event",
            new XAttribute("name_arme", "Damage 7"),
            new XAttribute("id_arme", "2028b441-d0b7-4690-a185-9b88c5ead9a3"),
            new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
            new XElement("if_object_property",
                new XAttribute("property", "throttle"),
                new XAttribute("name", "{ShipName}"),
                new XAttribute("comparator","GREATER"),
                new XAttribute("value", "0.8")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "1.0")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "7.0")),
            new XElement("if_timer_finished",
                new XAttribute("name", "Theta Collector Limits")),
            new XElement("set_player_grid_damage",
                new XAttribute("value", "1.0"),
                new XAttribute("index", "2"),
                new XAttribute("systemType", "systemImpulse"),
                new XAttribute("countFrom", "front")),
            new XElement("set_timer",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("seconds", "3")),
            new XElement("set_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("value", "8.0"))),
        new XElement("event",
            new XAttribute("name_arme", "Damage 8"),
            new XAttribute("id_arme", "b0b0e9ad-7d97-4356-aaf4-32f0eaab2135"),
            new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
            new XElement("if_object_property",
                new XAttribute("property", "throttle"),
                new XAttribute("name", "{ShipName}"),
                new XAttribute("comparator", "GREATER"),
                new XAttribute("value", "0.8")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "1.0")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "8.0")),
            new XElement("if_timer_finished",
                new XAttribute("name", "Theta Collector Limits")),
            new XElement("warning_popup_message",
                new XAttribute("message", "WARNING: Theta Collection system still active."),
                new XAttribute("consoles", "HE")),
            new XElement("set_player_grid_damage",
                new XAttribute("value", "1.0"),
                new XAttribute("index", "3"),
                new XAttribute("systemType", "systemImpulse"),
                new XAttribute("countFrom", "front")),
            new XElement("set_timer",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("seconds", "3")),
            new XElement("set_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("value", "9.0"))),
        new XElement("event",
            new XAttribute("name_arme", "Damage 9"),
            new XAttribute("id_arme", "b6c71bd8-35c3-4a38-8a78-39f1b23ee95c"),
            new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
            new XElement("if_object_property",
                new XAttribute("property", "throttle"),
                new XAttribute("name", "{ShipName}"),
                new XAttribute("comparator", "GREATER"),
                new XAttribute("value", "0.8")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "1.0")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("comparator", "EQUALS" ),
                new XAttribute("value", "9.0")),
            new XElement("if_timer_finished",
                new XAttribute("name", "Theta Collector Limits")),
            new XElement("warning_popup_message",
                new XAttribute("message", "WARNING: Theta Collection system still active."),
                new XAttribute("consoles", "HE")),
            new XElement("set_player_grid_damage",
                new XAttribute("value", "1.0"),
                new XAttribute("index", "4"),
                new XAttribute("systemType", "systemImpulse"),
                new XAttribute("countFrom", "front")),
            new XElement("set_timer",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("seconds", "3")),
            new XElement("set_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("value", "10.0"))),
        new XElement("event",
            new XAttribute("name_arme", "Damage 10"),
            new XAttribute("id_arme", "7649fe59-81e1-4658-9709-6867282c3705"),
            new XAttribute("parent_id_arme", "33553c4d-d819-4d2d-a3f6-123b3e6249ae"),
            new XElement("if_object_property",
                new XAttribute("property", "throttle"),
                new XAttribute("name", "{ShipName}"),
                new XAttribute("comparator","GREATER"),
                new XAttribute("value", "0.8")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collection"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "1.0")),
            new XElement("if_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("comparator", "EQUALS"),
                new XAttribute("value", "10.0")),
            new XElement("if_timer_finished",
                new XAttribute("name", "Theta Collector Limits")),
            new XElement("warning_popup_message",
                new XAttribute("message", "WARNING: Theta Collection system still active."),
                new XAttribute("consoles", "HE")),
            new XElement("set_player_grid_damage",
                new XAttribute("value", "1.0"),
                new XAttribute("index", "5"),
                new XAttribute("systemType", "systemImpulse"),
                new XAttribute("countFrom", "front")),
            new XElement("set_timer",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("seconds", "3")),
            new XElement("set_variable",
                new XAttribute("name", "Theta Collector Limits"),
                new XAttribute("value", "1.0"))))
        };
    }
}
