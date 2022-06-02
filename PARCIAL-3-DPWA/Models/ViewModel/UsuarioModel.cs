﻿namespace PARCIAL_3_DPWA.Models.ViewModel
{
    public class UsuarioModel
    {
        public int Id_usuario { get; set; }
        public string? U_name { get; set; }
        public string? Urlfoto { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Intro { get; set; }
        public virtual Red? Redes_sociales { get; set; }
        public virtual GradoAcademicoByUsuario? Grado_academico { get; set; }
        public virtual Certificacion? Certificacion { get; set; }
    }
}
