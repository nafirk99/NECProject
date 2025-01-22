using System.ComponentModel.DataAnnotations;

namespace NEC.API.Models
{
    public class CreateDTO
    {
        [Required]
        public string tx_name { get; set; }

        [Required]
        public int id_age { get; set; }

        [Required]
        public DateTime dt_date_of_birth { get; set; }

        [Required]
        public decimal dec_salary { get; set; }

        [Required]
        public DateTime dt_join_date { get; set; }

        [Required]
        public string tx_email { get; set; }

        [Required]
        public string tx_phone_no { get; set; }
    }
}
