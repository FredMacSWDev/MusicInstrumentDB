using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentFamilyModels
{
    public class InstrumentFamilyCreate
    {
        [Required]
        [MinLength(5, ErrorMessage = "Please enter at least 5 characters.")]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string FamilyName { get; set; }      

    }
}
