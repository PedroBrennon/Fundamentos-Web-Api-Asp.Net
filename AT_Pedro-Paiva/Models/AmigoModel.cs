using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_Pedro_Paiva.Models
{
    public class AmigoModel
    {
        public int id { get; set; }
        [DisplayName("Nome")]
        [Required]
        [StringLength(50, ErrorMessage = "O campo Nome permite no máximo 50 caracteres!")]
        public string nome { get; set; }
        [DisplayName("Sobrenome")]
        [Required]
        public string sobrenome { get; set; }
        [DisplayName("Data de aniversário")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime aniversario { get; set; }

        public IEnumerable<AmigoModel> amigosOrdenados { get; set; }
        public IEnumerable<AmigoModel> amigosDoDia { get; set; }
    }
}