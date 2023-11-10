using System;
using System.Collections.Generic;

namespace AppCine.Models;

public partial class Boleto
{
    public string BoletoId { get; set; } = null!;

    public int? FuncionId { get; set; }

    public int? AsientoId { get; set; }

    public virtual Asiento? Asiento { get; set; }

    public virtual Funcione? Funcion { get; set; }
}
