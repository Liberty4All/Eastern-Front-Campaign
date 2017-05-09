using EFC.Domain.Ships;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EFC.Sectors
{
    /// <summary>
    /// The class for all the mission options
    /// </summary>
    public class MissionOptions
    {
        public string MissionName { get; set; }
        public string SectorName { get; set; }
        public List<IShip> PlayerShips { get; set; }
        public List<IShip> EnemyShips { get; set; }
        public List<IShip> Neutrals { get; set; }

        public MissionOptions()
        {
            PlayerShips = new List<IShip>();
            EnemyShips = new List<IShip>();
            Neutrals = new List<IShip>();
        }
    }

    /// <summary>
    /// The class that generates all the XML scripting for a mission
    /// </summary>
    public class SectorScript
    {
        private MissionOptions _options;

        public XElement MissionXML { get; set; }

        public SectorScript(MissionOptions options)
        {
            _options = options;
            MissionXML = CreateMission();
        }

        /// <summary>
        /// Creates the mission script
        /// </summary>
        /// <returns>The entire XML script for the mission</returns>
        private XElement CreateMission()
        {
            XElement result = new XElement("mission_data");
            result.Add(new XAttribute("version", "2.4"));
            result.Add(CreateStart());
            DestructionEvents(ref result);
            SurrenderEvents(ref result);

            return result;
        }

        /// <summary>
        /// Add Surrender tracking for each ship
        /// </summary>
        /// <param name="xml">The XML fragment to add the code to</param>
        private void SurrenderEvents(ref XElement xml)
        {
            xml.Add(new XComment("===== Surrender Tracking Events ====="));
            foreach (IShip ship in _options.EnemyShips)
            {
                xml.Add(new XComment("***** " + ship.Name + " Surrender Tracking *****"));
                foreach (XElement item in (ship as EnemyShip).SurrenderXML)
                {
                    xml.Add(item);
                }
            }
        }

        /// <summary>
        /// Creates all the destruction logging events for all the ships
        /// </summary>
        /// <param name="xml">The XML to add the events to</param>
        private void DestructionEvents(ref XElement xml)
        {
            xml.Add(new XComment("===== Player Destruction Events ====="));
            AddDestructionCode(ref xml, _options.PlayerShips);

            xml.Add(new XComment("===== Enemy Destruction Events ====="));
            AddDestructionCode(ref xml, _options.EnemyShips);

            xml.Add(new XComment("===== Neutral Destruction Events ====="));
            AddDestructionCode(ref xml, _options.Neutrals);
        }

        /// <summary>
        /// Creates the destruction code for each ship in the list
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="ships"></param>
        private void AddDestructionCode(ref XElement xml, List<IShip> ships)
        {
            foreach (IShip ship in ships)
            {
                xml.Add(new XComment("***** " + ship.Name + " Destruction *****"));
                xml.Add(ship.DestructionXML);
            }
        }

        /// <summary>
        /// Builds the start section
        /// </summary>
        /// <returns>All the statrt xml</returns>
        private XElement CreateStart()
        {
            string MissionTitle = _options.SectorName + ": " + _options.MissionName + " Started";
            XElement result = new XElement("start");

            // diff tool does not like comments right after start
            //result.Add(new XComment("===== Create Player Ships ====="));
            CreateShips(ref result, _options.PlayerShips);

            result.Add(new XComment("===== Create Enemy Ships ====="));
            CreateShips(ref result, _options.EnemyShips);

            result.Add(new XComment("===== Create Neutral Ships ====="));
            CreateShips(ref result, _options.Neutrals);

            result.Add(
                new XElement("big_message",
                    new XAttribute("title", MissionTitle),
                    new XAttribute("subtitle1", "Good hunting!")));

            result.Add(
                new XElement("log",
                    new XAttribute("text", MissionTitle)));

            return result;
        }

        /// <summary>
        /// Builds the creation code for all the ships in the list
        /// </summary>
        /// <param name="xml">The XML to add the code to</param>
        /// <param name="ships">The list of ships to get the creation code from</param>
        private void CreateShips(ref XElement xml, List<IShip> ships)
        {
            foreach (IShip ship in ships)
            {
                // diff tool does not like comments right after start
                //xml.Add(new XComment("***** " + ship.Name + " Creation *****"));

                foreach (XElement item in ship.CreationXML)
                {
                    xml.Add(item);
                }
            }
        }
    }
}