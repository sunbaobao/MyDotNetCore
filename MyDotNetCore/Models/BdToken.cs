using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDotNetCore.Models
{
    public class BdToken
    {
       
        public int Id { get; set; }
        public String Token { get; set; }
        public DateTime Exexpires_in { get; set; }
    }
}
