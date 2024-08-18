using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip.Entitiy
{
    internal class Data
    {
        [Key]
        public int Id { get; set; }
        public int BoyaKodu { get; set; }
        public string BoyaRengi {  get; set; }
        public string BoyaTipi { get; set; }
        public int  BoyaFiyat { get; set; }
        public int StokAdet {  get; set; }
        public string UreticiFirma { get; set ; }

    }
}
