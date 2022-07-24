using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public int State { get; set; }
    }
}
