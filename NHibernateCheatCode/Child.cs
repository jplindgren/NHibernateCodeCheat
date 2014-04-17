using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateCodeCheat {
    public class Child {
        public virtual int Id { get; set; }
        public virtual string ChildName { get; set; }
        public virtual Parent Parent { get; set; }
    }
}
