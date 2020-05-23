using System;
using System.Collections.Generic;
using System.Text;

namespace Net.DTO
{
    public class dtoUsuario
    {
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }       
        public bool IsCambioPassword { get; set; }
        public string CorreoElectronico { get; set; }
        public bool IsUsuarioActivo { get; set; }
        

    }
}
