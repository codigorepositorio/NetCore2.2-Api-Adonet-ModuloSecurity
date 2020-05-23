using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class Perfil : Hseguridad
    {
        public int IdPerfil { get; set; }
        public string NomPerfil { get; set; }
        public bool IsPerfilActivo { get; set; }

    }
}
