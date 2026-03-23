using System;
using System.Collections.Generic;

namespace UpscalePruebaTecnica.Models;

public partial class TiposContratacion
{
    public int TipoContratacionId { get; set; }

    public string Abreviatura { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
