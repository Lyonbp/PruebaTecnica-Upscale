using System;
using System.Collections.Generic;

namespace UpscalePruebaTecnica.Models;

public partial class TiposTelefono
{
    public int TipoTelefonoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
