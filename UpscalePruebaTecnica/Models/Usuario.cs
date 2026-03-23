using System;
using System.Collections.Generic;

namespace UpscalePruebaTecnica.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int TipoDocumentoId { get; set; }

    public int TipoContratacionId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? IntentosFallidos { get; set; }

    public DateTime? FechaBloqueo { get; set; }

    public string Nombres { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Nacionalidad { get; set; }

    public string? Sexo { get; set; }

    public string CorreoPrincipal { get; set; } = null!;

    public string? CorreoSecundario { get; set; }

    public string? TelefonoMovil { get; set; }

    public int? TipoTelefonoSecundarioId { get; set; }

    public string? TelefonoSecundario { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public string? Cargo { get; set; }

    public string? Institucion { get; set; }

    public virtual TiposContratacion TipoContratacion { get; set; } = null!;

    public virtual TiposDocumento TipoDocumento { get; set; } = null!;

    public virtual TiposTelefono? TipoTelefonoSecundario { get; set; }
}
