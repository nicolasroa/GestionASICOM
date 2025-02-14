﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlow.Entidades
{
   public class NotariasBase: Base
    {
        public int Estado_Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }

    }
    public class NotariasInfo : NotariasBase
    {
        public string DescripcionEstado { get; set; }
    }
}
