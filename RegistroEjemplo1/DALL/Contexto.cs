using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RegistroEjemplo1.Entidades;

namespace RegistroEjemplo1.DALL
{
   
        public class Contexto : DbContext
        {
            public DbSet<Personas> Persona { get; set; }

        public Contexto() : base("ConStr")
        { }

        }
    
}
