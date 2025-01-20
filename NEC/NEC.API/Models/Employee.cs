using System.ComponentModel.DataAnnotations;

namespace NEC.API.Models
{
    public class Employee
    {
        [Key]
        public int id_employee_key { get; set; }
        public int id_employee_ver { get; set; }
        public DateTime? dtt_created { get; set; }
        public DateTime? dtt_updated { get; set; }

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
