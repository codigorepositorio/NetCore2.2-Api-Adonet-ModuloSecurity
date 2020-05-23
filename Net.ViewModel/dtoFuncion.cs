using System;
using System.Collections.Generic;
using System.Text;

namespace Net.DTO
{
   public class dtoFuncion
    {
        public int IdFuncion { get; set; }
        public int IdModulo { get; set; }
        public string NomFuncion { get; set; }
        public string CodAcceso { get; set; }
        public string IsFuncionActivo { get; set; }
        public string RegCreate { get; set; } 
        public int RegCreateIdUsuario { get; set; } 
        public int RegUpdateIdUsuario { get; set; } 
        public string RegUpdate { get; set; } 
    }
}
