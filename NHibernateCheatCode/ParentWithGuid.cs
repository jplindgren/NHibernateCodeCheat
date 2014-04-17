using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateCodeCheat {
    /// <summary>
    /// Usando Guid(GuidComb no mapeamento) ou Hilo, o hibernate não precisa ir na tabela 
    /// fazer um select para retornar o id do objeto. Assim podemos fazer insert em batch
    /// </summary>
    public class ParentWithGuid {
        public virtual Guid Id { get; set; }
        public virtual string ParentName { get; set; }
    }// class
}
