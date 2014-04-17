using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateCodeCheat {
    public class Parent {
        public virtual int Id { get; set; }
        public virtual string ParentName { get; set; }
        public virtual ValueObject ValueObject { get; set; }
        public virtual IList<Child> Childs { get;set; }
    }
}
