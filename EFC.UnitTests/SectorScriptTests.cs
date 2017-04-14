using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Xml.Linq;
using EFC.Sectors;
using System.Collections.Generic;
using EFC.Domain.Ships;
using EFC.Domain;

namespace EFC.UnitTests
{
    [TestClass]
    public class SectorScriptTests
    {
        [TestMethod]
        [TestCategory("Undefined")]
        public void CreateSector_SectorName_SectorCreated()
        {
            // Arrange
            PlayerShip artemis = new PlayerShip
            {
                Name = "Artemis",
                Slot = 0,
                Position = new Position(10000, 0, 10000),
                Angle = 0,
                ShipType = ShipType.Battleship
            };

            EnemyShip enemy = new EnemyShip
            {
                Name = "Test1",
                Position = new Position(20000, 0, 20000),
                Race = "Kralien enemy standard",
                Hull = "Cruiser small",
                Angle = 90,
                Fleet = 5,
                Side = 1
            };

            EnemyShip enemy2 = new EnemyShip
            {
                Name = "Vindicator",
                Position = new Position(20000, 0, 30000),
                Race = "Torgoth enemy support whalehater",
                Hull = "Goliath small",
                Angle = 270,
                Fleet = 6,
                Side = 1
            };

            NPCShip npcShip = new NPCShip
            {
                Name = "Tanker",
                Angle = 180,
                Hull = "Bulk Cargo cargo",
                Position = new Position(25000, 0, 25000),
                Race = "Terran",
                Side = 0
            };

            MissionOptions options = new MissionOptions()
            {
                SectorName = "Alpha",
                MissionName = "FirstMission",
            };
            options.PlayerShips.Add(artemis);
            options.EnemyShips.Add(enemy);
            options.EnemyShips.Add(enemy2);
            options.Neutrals.Add(npcShip);

            XElement fakeXML = CreateTestXML();

            // Act
            SectorScript result = new SectorScript(options);

            // Assert
            result.Should().NotBeNull();
            result.MissionXML.Should().NotBeNull();
            result.MissionXML.IsEmpty.Should().BeFalse();
            System.Diagnostics.Debug.WriteLine(result.MissionXML);

            result.MissionXML.ToString().ShouldBeEquivalentTo(fakeXML.ToString());
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void PlayerShipCreationXML_Nothing_CorrectXML()
        {
            // Arrange
            PlayerShip ship = new PlayerShip
            {
                Angle = 90,
                Name = "Fuzzy Bunny",
                Position = new Position(50000, 0, 50000),
                ShipType = ShipType.MissileCruiser,
                Slot = 0
            };
            XElement fakeXML1 = new XElement("create",
                new XAttribute("angle", ship.Angle),
                new XAttribute("name", ship.Name),
                new XAttribute("z", ship.Position.Z),
                new XAttribute("y", ship.Position.Y),
                new XAttribute("x", ship.Position.X),
                new XAttribute("player_slot", "0"),
                new XAttribute("raceKeys", "TSN player"),
                new XAttribute("hullKeys", "Missile Cruiser player"),
                new XAttribute("type", "player"));
            XElement fakeXML2 = new XElement("set_variable",
                new XAttribute("name", ship.Name + "Death"),
                new XAttribute("value", "0"),
                new XAttribute("integer", "yes"));
            XElement fakeXML3 = new XElement("log",
                new XAttribute("text", "Player Ship: " + ship.Name + " created"));
            List<XElement> fakeXML = new List<XElement> { fakeXML1, fakeXML2, fakeXML3 };

            // Act
            var result = ship.CreationXML;

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(fakeXML.Count);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Should().BeEquivalentTo(fakeXML[i]);
            }
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void PlayerShipDestructionXML_Nothing_CorrectXML()
        {
            // Arrange
            PlayerShip ship = new PlayerShip
            {
                Angle = 90,
                Name = "Fuzzy Bunny",
                Position = new Position(50000, 0, 50000),
                ShipType = ShipType.MineLayer,
                Slot = 0
            };

            XElement fakeXML = new XElement("event",
                new XAttribute("name", ship.Name + " Death Check"),
                new XElement("if_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0")),
                new XElement("if_not_exists", 
                    new XAttribute("name", ship.Name)),
                new XElement("log",
                    new XAttribute("text", ship.Name + " destroyed")),
                new XElement("set_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("value", "1"),
                    new XAttribute("integer", "yes")));

            // Act
            var result = ship.DestructionXML;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fakeXML);
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void EnemyShipCreationXML_NoParameters_CorrectXML()
        {
            // Arrange
            EnemyShip enemy = new EnemyShip()
            {
                Angle = 90,
                Name = "Nemesis",
                Position = new Position(40000, 0, 40000),
                Fleet = 1,
                Hull = "Goliath small",
                Race = "Torgoth enemy support whalehater",
                Side = 1
            };
            XElement fakeXML1 = new XElement("create",
                new XAttribute("angle", enemy.Angle),
                new XAttribute("name", enemy.Name),
                new XAttribute("z", enemy.Position.Z),
                new XAttribute("y", enemy.Position.Y),
                new XAttribute("x", enemy.Position.X),
                new XAttribute("raceKeys", enemy.Race),
                new XAttribute("hullKeys", enemy.Hull),
                new XAttribute("fleetNumber", enemy.Fleet),
                new XAttribute("sideValue", enemy.Side),
                new XAttribute("type", "enemy"));
            XElement fakeXML2 = new XElement("set_variable",
                new XAttribute("name", enemy.Name + "Death"),
                new XAttribute("value", "0"),
                new XAttribute("integer", "yes"));
            XElement fakeXML3 = new XElement("set_variable",
                new XAttribute("name", enemy.Name + "Surrendered"),
                new XAttribute("value", "0"),
                new XAttribute("integer", "yes"));
            XElement fakeXML4 = new XElement("log",
                new XAttribute("text", "Enemy Ship: " + enemy.Name + " created"));
            List<XElement> fakeXML = new List<XElement> { fakeXML1, fakeXML2, fakeXML3, fakeXML4 };

            // Act
            var result = enemy.CreationXML;

            // Assert
            result.Should().NotBeNullOrEmpty().And.HaveCount(fakeXML.Count);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Should().BeEquivalentTo(fakeXML[i]);
            }
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void EnemyShipDestructionXML_Nothing_CorrectXML()
        {
            // Arrange
            EnemyShip ship = new EnemyShip
            {
                Angle = 90,
                Name = "Nemesis",
                Position = new Position(40000, 0, 40000),
                Fleet = 1,
                Hull = "Goliath small",
                Race = "Torgoth enemy support whalehater",
                Side = 1
            };

            XElement fakeXML = new XElement("event",
                new XAttribute("name", ship.Name + " Death Check"),
                new XElement("if_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0")),
                new XElement("if_not_exists",
                    new XAttribute("name", ship.Name)),
                new XElement("log",
                    new XAttribute("text", ship.Name + " destroyed")),
                new XElement("set_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("value", "1"),
                    new XAttribute("integer", "yes")));

            // Act
            var result = ship.DestructionXML;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fakeXML);
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void NPCShipCreationXML_NoParameters_CorrectXML()
        {
            // Arrange
            NPCShip ship = new NPCShip()
            {
                Angle = 90,
                Name="Friendly Freddy",
                Position = new Position(10000,0,10000),
                Race = "Terran",
                Hull = "Bulk Cargo cargo",
                Side = 2
            };

            XElement fakeXML1 = new XElement("create",
                new XAttribute("angle", ship.Angle),
                new XAttribute("name", ship.Name),
                new XAttribute("z", ship.Position.Z),
                new XAttribute("y", ship.Position.Y),
                new XAttribute("x", ship.Position.X),
                new XAttribute("raceKeys", ship.Race),
                new XAttribute("hullKeys", ship.Hull),
                new XAttribute("sideValue", ship.Side),
                new XAttribute("type", "neutral"));
            XElement fakeXML2 = new XElement("set_variable",
                new XAttribute("name", ship.Name + "Death"),
                new XAttribute("value", "0"),
                new XAttribute("integer", "yes"));
            XElement fakeXML3 = new XElement("log",
                new XAttribute("text", "NPC Ship: " + ship.Name + " created"));
            List<XElement> fakeXML = new List<XElement> { fakeXML1, fakeXML2, fakeXML3 };

            // Act
            var result = ship.CreationXML;

            // Assert
            result.Should().NotBeNullOrEmpty().And.HaveCount(fakeXML.Count);
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void NPCShipDestructionXML_Nothing_CorrectXML()
        {
            // Arrange
            NPCShip ship = new NPCShip
            {
                Angle = 90,
                Name = "Friendly Freddy",
                Position = new Position(40000, 0, 40000),
                Hull = "Bulk Cargo cargo",
                Race = "Terran",
                Side = 0
            };

            XElement fakeXML = new XElement("event",
                new XAttribute("name", ship.Name + " Death Check"),
                new XElement("if_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("comparator", "EQUALS"),
                    new XAttribute("value", "0")),
                new XElement("if_not_exists",
                    new XAttribute("name", ship.Name)),
                new XElement("log",
                    new XAttribute("text", ship.Name + " destroyed")),
                new XElement("set_variable",
                    new XAttribute("name", ship.Name + "Death"),
                    new XAttribute("value", "1"),
                    new XAttribute("integer", "yes")));

            // Act
            var result = ship.DestructionXML;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fakeXML);
        }

        private XElement CreateTestXML()
        {
            XElement result = new XElement("mission_data",
                new XAttribute("version", "2.4"),
                new XElement("start",
                    new XComment("===== Create Player Ships ====="),
                    new XComment("***** Artemis Creation *****"),
                    new XElement("create",
                        new XAttribute("angle", "0"),
                        new XAttribute("name", "Artemis"),
                        new XAttribute("z", "10000"),
                        new XAttribute("y", "0"),
                        new XAttribute("x", "10000"),
                        new XAttribute("player_slot", "0"),
                        new XAttribute("raceKeys","TSN player"),
                        new XAttribute("hullKeys","Battleship player"),
                        new XAttribute("type", "player")),
                    new XElement("set_variable",
                        new XAttribute("name", "ArtemisDeath"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                    new XElement("log",
                        new XAttribute("text","Player Ship: Artemis created")),
                    new XComment("===== Create Enemy Ships ====="),
                    new XComment("***** Test1 Creation *****"),
                    new XElement("create",
                        new XAttribute("angle", "90"),
                        new XAttribute("name", "Test1"),
                        new XAttribute("z", "20000"),
                        new XAttribute("y", "0"),
                        new XAttribute("x", "20000"),
                        new XAttribute("raceKeys", "Kralien enemy standard"),
                        new XAttribute("hullKeys","Cruiser small"),
                        new XAttribute("fleetNumber","5"),
                        new XAttribute("sideValue","1"),
                        new XAttribute("type", "enemy")),
                    new XElement("set_variable",
                        new XAttribute("name", "Test1Death"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                     new XElement("set_variable",
                        new XAttribute("name", "Test1Surrendered"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                   new XElement("log",
                        new XAttribute("text", "Enemy Ship: Test1 created")),
                    new XComment("***** Vindicator Creation *****"),
                    new XElement("create",
                        new XAttribute("angle", "270"),
                        new XAttribute("name", "Vindicator"),
                        new XAttribute("z", "30000"),
                        new XAttribute("y", "0"),
                        new XAttribute("x", "20000"),
                        new XAttribute("raceKeys", "Torgoth enemy support whalehater"),
                        new XAttribute("hullKeys", "Goliath small"),
                        new XAttribute("fleetNumber", "6"),
                        new XAttribute("sideValue", "1"),
                        new XAttribute("type", "enemy")),
                    new XElement("set_variable",
                        new XAttribute("name", "VindicatorDeath"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                     new XElement("set_variable",
                        new XAttribute("name", "VindicatorSurrendered"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                   new XElement("log",
                        new XAttribute("text", "Enemy Ship: Vindicator created")),
                    new XComment("===== Create Neutral Ships ====="),
                    new XComment("***** Friendly Freddy Creation *****"),
                    new XElement("create",
                        new XAttribute("angle", "180"),
                        new XAttribute("name", "Friendly Freddy"),
                        new XAttribute("z", "25000"),
                        new XAttribute("y", "0"),
                        new XAttribute("x", "25000"),
                        new XAttribute("raceKeys", "Terran"),
                        new XAttribute("hullKeys", "Bulk Cargo cargo"),
                        new XAttribute("sideValue", "0"),
                        new XAttribute("type", "neutral")),
                    new XElement("set_variable",
                        new XAttribute("name", "FriendlyFreddyDeath"),
                        new XAttribute("value", "0"),
                        new XAttribute("integer", "yes")),
                   new XElement("log",
                        new XAttribute("text", "Neutral Ship: Friendly Freddy created")),

                    new XElement("big_message",
                        new XAttribute("title", "Alpha: FirstMission Started"),
                        new XAttribute("subtitle1","Good hunting!")),
                    new XElement("log",
                        new XAttribute("text","Alpha: FirstMission Started"))),
                new XComment("===== Player Destruction Events ====="),
                new XComment("***** Artemis Destruction *****"),
                new XElement("event",
                    new XAttribute("name", "Artemis Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", "ArtemisDeath"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", "Artemis")),
                    new XElement("log",
                        new XAttribute("text", "Artemis destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", "ArtemisDeath"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes"))),
                new XComment("===== Enemy Destruction Events ====="),
                new XComment("***** Test1 Destruction *****"),
                new XElement("event",
                    new XAttribute("name", "Test1 Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", "Test1Death"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", "Test1")),
                    new XElement("log",
                        new XAttribute("text", "Test1 destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", "Test1Death"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes"))),
                new XComment("***** Vindicator Destruction *****"),
                new XElement("event",
                    new XAttribute("name", "Vindicator Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", "VindicatorDeath"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", "Vindicator")),
                    new XElement("log",
                        new XAttribute("text", "Vindicator destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", "VindicatorDeath"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes"))),
                new XComment("===== Neutral Destruction Events ====="),
                new XComment("***** Friendly Freddy Destruction *****"),
                new XElement("event",
                    new XAttribute("name", "Friendly Freddy Death Check"),
                    new XElement("if_variable",
                        new XAttribute("name", "FriendlyFreddyDeath"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_not_exists",
                        new XAttribute("name", "Friendly Freddy")),
                    new XElement("log",
                        new XAttribute("text", "Friendly Freddy destroyed")),
                    new XElement("set_variable",
                        new XAttribute("name", "FriendlyFreddyDeath"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes"))),
                new XComment("===== Surrender Tracking Events ====="),
                new XComment("***** Test1 Surrender Tracking *****"),
                new XElement("event",
                    new XAttribute("name_arme","Test1 Surrendered"),
                    new XElement("if_variable",
                        new XAttribute("name","Test1Surrendered"),
                        new XAttribute("comparator","EQUALS"),
                        new XAttribute("value","0")),
                    new XElement("if_object_property",
                        new XAttribute("name","Test1"),
                        new XAttribute("property","hasSurrendered"),
                        new XAttribute("comparator","EQUALS"),
                        new XAttribute("value","1.0")),
                    new XElement("log",
                        new XAttribute("text","Test1 has surrendered")),
                    new XElement("set_variable",
                        new XAttribute("name","Test1Surrendered"),
                        new XAttribute("value","1"),
                        new XAttribute("integer","yes"))),
                    new XElement("event",
                        new XAttribute("name_arme","Test1 Destroyed After Surrender"),
                    new XElement("if_variable",
                        new XAttribute("name","Test1Surrendered"),
                        new XAttribute("comparator","EQUALS"),
                        new XAttribute("value","1")),
                    new XElement("if_variable",
                        new XAttribute("name","Test1Death"),
                        new XAttribute("comparator","EQUALS"),
                        new XAttribute("value","1")),
                    new XElement("log",
                        new XAttribute("text","Test1 destroyed after it surrendered")),
                    new XElement("set_variable",
                        new XAttribute("name","Test1Surrendered"),
                        new XAttribute("value","2"),
                        new XAttribute("integer","yes")),
                    new XElement("set_variable",
                        new XAttribute("name","Test1Death"),
                        new XAttribute("value","2"),
                        new XAttribute("integer","yes"))),
                new XComment("***** Vindicator Surrender Tracking *****"),
                new XElement("event",
                    new XAttribute("name_arme", "Vindicator Surrendered"),
                    new XElement("if_variable",
                        new XAttribute("name", "VindicatorSurrendered"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "0")),
                    new XElement("if_object_property",
                        new XAttribute("name", "Vindicator"),
                        new XAttribute("property", "hasSurrendered"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "1.0")),
                    new XElement("log",
                        new XAttribute("text", "Vindicator has surrendered")),
                    new XElement("set_variable",
                        new XAttribute("name", "VindicatorSurrendered"),
                        new XAttribute("value", "1"),
                        new XAttribute("integer", "yes"))),
                    new XElement("event",
                        new XAttribute("name_arme", "Vindicator Destroyed After Surrender"),
                    new XElement("if_variable",
                        new XAttribute("name", "VindicatorSurrendered"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "1")),
                    new XElement("if_variable",
                        new XAttribute("name", "VindicatorDeath"),
                        new XAttribute("comparator", "EQUALS"),
                        new XAttribute("value", "1")),
                    new XElement("log",
                        new XAttribute("text", "Vindicator destroyed after it surrendered")),
                    new XElement("set_variable",
                        new XAttribute("name", "VindicatorSurrendered"),
                        new XAttribute("value", "2"),
                        new XAttribute("integer", "yes")),
                    new XElement("set_variable",
                        new XAttribute("name", "VindicatorDeath"),
                        new XAttribute("value", "2"),
                        new XAttribute("integer", "yes"))));
            return result;
        }
    }
}
