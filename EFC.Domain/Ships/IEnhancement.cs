using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFC.Domain.Ships
{
    public interface IEnhancement
    {
        EnhancementType EnhancementType { get; }
        string Name { get; }
        List<XElement> StartXML { get; }
        List<XElement> BodyXML { get; }
    }

    public enum EnhancementType
    {
        ThetaGenerator
    }
}
