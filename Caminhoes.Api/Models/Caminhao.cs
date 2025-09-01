using System.ComponentModel.DataAnnotations;

namespace Caminhoes.Api.Models
{
    public enum Modelo
    {
        FH,
        FM,
        VM
    }

    public enum Planta
    {
        Brasil,
        Suecia,
        EstadosUnidos,
        Franca
    }

    public class Caminhao
    {
        [Required]
        [EnumDataType(typeof(Modelo))]
        public Modelo Modelo { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Ano de fabricação inválido.")]
        public int AnoFabricacao { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CodigoChassi { get; set; }

        [Required]
        [StringLength(30)]
        public string Cor { get; set; }

        [Required]
        [EnumDataType(typeof(Planta))]
        public Planta Planta { get; set; }
    }
}
