namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int Id { get; set; }
        public int PalestranteId { get; set; }
        public Palestrante Palestante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}