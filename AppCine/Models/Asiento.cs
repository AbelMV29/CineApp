using System;
using System.Collections.Generic;

namespace AppCine.Models;

public partial class Asiento
{
    public int AsientoId { get; set; }

    public int? FuncionId { get; set; }

    public string? Fila { get; set; }

    public int? Columna { get; set; }

    public bool Disponible { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

    public virtual Funcione? Funcion { get; set; }
}
