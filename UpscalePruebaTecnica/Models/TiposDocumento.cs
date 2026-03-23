using System;
using System.Collections.Generic;

namespace UpscalePruebaTecnica.Models;

public partial class TiposDocumento
{
    public int TipoDocumentoId { get; set; }

    public string Abreviatura { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
