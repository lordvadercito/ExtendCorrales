using System;

namespace ExtendCorrales.Models
{
    public class ExtendCorral : Entity
    {
        public int ExtendCorralId { get; set; }
        public int TropaId { get; set; }
        public int NroTropa { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string HoraLlegada { get; set;}
        public int Corral { get; set; }
        public string Raza { get; set; }
        public string Categoria { get; set;}
        public string Edad { get; set; }
        public string PesoGeneral { get; set; }
        public string EstadoGeneral { get; set; }
        public string Condicion { get; set; }
    }
}