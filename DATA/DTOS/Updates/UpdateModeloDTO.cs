﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.DTOS.Updates
{
    public class UpdateModeloDTO
    {
        public string Modelo { get; set; }
        public string Obs { get; set; }
        public long idMarca { get; set; }
        public long IdGrupo { get; set; }
    }
}
