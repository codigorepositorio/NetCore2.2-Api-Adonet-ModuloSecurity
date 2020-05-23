using System;
using System.Collections.Generic;
using System.Text;

namespace Net.DTO
{
    public class dtoAplicativo
    {
        public int IdAplicativo { get; set; }
        public string CodAplicativo { get; set; }
        public string NomAplicativo { get; set; }
        public bool IsAplicativoActivo { get; set; }
        public DateTime RegCreate { get; set; }
        public int RegCreateIdUsuario { get; set; } = 1;       
        public int RegUpdateIdUsuario { get; set; } = 2;        
        public DateTime RegUpdate { get; set; }
    }
}
