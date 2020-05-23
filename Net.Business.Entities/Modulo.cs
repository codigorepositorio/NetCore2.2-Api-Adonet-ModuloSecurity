using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class Modulos : Hseguridad
    {
        public int IdModulo { get; set; }
        public int IdAplicativo { get; set; }
        public string NomModulo { get; set; }
        public bool IsModuloActivo { get; set; }
    }
}

