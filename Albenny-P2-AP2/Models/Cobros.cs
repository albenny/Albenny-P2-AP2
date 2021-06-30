using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Albenny_P2_AP2.Models
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Debe realizar un cobro")]
        public double TotalCobrado { get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("CobroId")]
        public virtual List<CobrosDetalle> Detalle { get; set; } = new List<CobrosDetalle>();
    }
}
