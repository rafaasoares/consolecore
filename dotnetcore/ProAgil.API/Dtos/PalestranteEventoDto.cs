using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dtos
{
    public class PalestranteEventoDto
    {
        public int Id { get; set; }
        public int PalestranteId { get; set; }
        public PalestranteDto Palestante { get; set; }
        public int EventoId { get; set; }
        public EventoDto Evento { get; set; }
    }
}
