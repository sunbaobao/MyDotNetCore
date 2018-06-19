using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDotNetCore.Models.BdViewModels
{
    public class BdTokenDetectViewModels
    {
        [Required]
        public string Image { get; set; }

        public string Face_fields { get; set; }

        public int Max_face_num { get; set; }
    }
}
