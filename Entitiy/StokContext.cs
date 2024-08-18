using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Entitiy
{
    internal class StokContext : DbContext
    {
        public DbSet<Data> Datas { get; set; }
        public StokContext() : base("name=StokTakipDB")
        {

        }
    }
}
