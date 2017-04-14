using System.Collections.Generic;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public interface IShip
    {
        int Angle { get; set; }
        string Name { get; set; }
        Position Position { get; set; }
        List<XElement> CreationXML { get; }
        XElement DestructionXML { get; }
    }
}