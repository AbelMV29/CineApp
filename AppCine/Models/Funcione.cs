using System;
using System.Collections.Generic;

namespace AppCine.Models;

public partial class Funcione
{
    public int FuncionId { get; set; }

    public string? Titulo { get; set; }

    public DateTime? FechaHora { get; set; }

    public int? Duracion { get; set; }

    public string? Genero { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
}
