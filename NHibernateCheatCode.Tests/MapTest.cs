using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Mapping.ByCode;
using NHibernateCodeCheat.mapping;
using System.Xml.Serialization;

namespace NHibernateCodeCheat.Tests {
    [TestClass]
    public class MapTest {
        [TestMethod]
        public void CanGenerateChildMapXml() {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<ChildMap>();

            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            XmlSerializer xmlSerializer = new XmlSerializer(hbmMapping.GetType());

            xmlSerializer.Serialize(Console.Out, hbmMapping);
            
        }

        [TestMethod]
        public void CanGenerateParentMapXml() {
            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<ParentMap>();

            var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            XmlSerializer xmlSerializer = new XmlSerializer(hbmMapping.GetType());

            xmlSerializer.Serialize(Console.Out, hbmMapping);

        }
    } // class
}
