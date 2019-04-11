using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendCorrales.Models
{
    public class Entity
    {
        [Display(Name = "Creado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.Now;

        [Display(Name = "Actualizado")]
        public DateTime Updated { get; set; } = DateTime.Now;
        
    }
}
