﻿using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models.ViewModel
{
    public class UsuarioModel
    {

        public UsuarioModel()
        {
            Redes_sociales = new HashSet<RedesModel>();
            Certificacion = new HashSet<CertificacionModel>();
        }   

        public int Id_usuario { get; set; }
        public string? U_name { get; set; }
        public string? Urlfoto { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Intro { get; set; }
        public virtual ICollection<RedesModel> Redes_sociales { get; set; }
        public virtual GradoAcademicoModel Grado_academico { get; set; }
        public virtual ICollection<CertificacionModel> Certificacion { get; set; }
    }
}
