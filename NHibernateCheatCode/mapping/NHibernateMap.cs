using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateCodeCheat.mapping {
    public class ParentMap : ClassMapping<Parent> {
        public ParentMap() {
            Lazy(true);
            Id(x => x.Id, mapper => { mapper.Generator(Generators.Native); });
            Property(x => x.ParentName);
            Component<ValueObject>(x => x.ValueObject, map => { map.Class<ValueObject>(); 
                                                                map.Property(x => x.value1);
                                                                map.Property(x => x.value2);
            });
            Bag<Child>(x => x.Childs, 
                       map => { 
                            map.Key( x => x.Column("ParentId"));
                            map.Lazy(CollectionLazy.Extra);
                            map.Fetch(CollectionFetchMode.Select);
                            map.Inverse(true);
                       }, 
                       m => m.OneToMany());
        }
    }// class

    public class ParentWithGuidMap : ClassMapping<ParentWithGuid> {
        public ParentWithGuidMap() {
            Id(x => x.Id, mapper => { mapper.Generator(Generators.GuidComb); });
            Property(x => x.ParentName);
        }
    }// class

    public class ChildMap : ClassMapping<Child> {
        public ChildMap() {
            Id(x => x.Id, mapper => { mapper.Generator(Generators.Identity); });
            Property(x => x.ChildName);
            ManyToOne<Parent>(x => x.Parent,
                    map => {
                        map.Column("ParentId");
                    });
        }
    }// class
}
