using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class UsuarioPerfil : Hseguridad
    {
        public int IdUsuarioPerfil { get; set; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public bool IsUsuarioPerfilActivo { get; set; }
        public bool IsAccesoDirecto { get; set; }
    }
}
