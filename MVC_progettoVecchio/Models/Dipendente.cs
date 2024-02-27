using System.ComponentModel.DataAnnotations;

namespace MVC_progettoVecchio.Models
{
    // posso inserire i controlli nel model per evitare di farlo nel controller
    public class Dipendente
    {
        public int IdUtente { get; set; }

        [Required(ErrorMessage = "Inserire il Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserire il Cognome.")]
        [StringLength(50, MinimumLength = 3)]
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string CodiceFiscale { get; set; }
        public bool Coniugato { get; set; }

        public int NumeroFigli { get; set; }
        public string Mansione { get; set; }

        public Dipendente()
        { }

        public Dipendente(int IdUtente, string nome, string cognome, string indirizzo, string codiceFiscale, bool coniugato, int numeroFigli, string mansione)
        {
            this.IdUtente = IdUtente;
            this.Nome = nome;
            this.Cognome = cognome;
            this.Indirizzo = indirizzo;
            this.CodiceFiscale = codiceFiscale;
            this.Coniugato = coniugato;
            this.NumeroFigli = numeroFigli;
            this.Mansione = mansione;
        }
    }
}