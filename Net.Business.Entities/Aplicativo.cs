using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class Aplicativo: Hseguridad
    {
        public int IdAplicativo { get; set; }
        public string CodAplicativo { get; set; }
        public string NomAplicativo { get; set; }
        public bool IsAplicativoActivo { get; set; }        
    }
}
