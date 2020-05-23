using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class Funcion : Hseguridad
    {
        public int IdFuncion { get; set; }
        public int IdModulo { get; set; }
        public string NomFuncion { get; set; }
        public string CodAcceso { get; set; }
        public bool IsFuncionActivo { get; set; }
        

    }
}
