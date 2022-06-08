

using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        [Required(ErrorMessage = "Il campo è obbligatorio")]

        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
    
        public string Descrizione { get; set; }
       
        public string? sFoto { get; set; }


        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1, 20)]
        public double Prezzo { get; set; }

        public IFormFile Foto { get; set; }


        public Pizza()
        {

        }

        public Pizza(int id, string nome, string descrizione, string foto, double prezzo)
        {
            Id = id;
            Nome = nome;
            Descrizione = descrizione;
            sFoto = foto;
            Prezzo = prezzo;

        }
    }
}