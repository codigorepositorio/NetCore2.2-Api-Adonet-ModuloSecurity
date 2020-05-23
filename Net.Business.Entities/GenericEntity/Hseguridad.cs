using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class Hseguridad 
    {
        public DateTime RegCreate { get; set; } = DateTime.Now;
        public int RegCreateIdUsuario { get; set; } = 5;      
        public int RegUpdateIdUsuario { get; set; } = 6;        
        public DateTime RegUpdate { get; set; }=DateTime.Now;
    }
}



