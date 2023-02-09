using System.ComponentModel.DataAnnotations;

namespace myapi.DTOs
{
    public class AlePayRequestPaymentDto
    {
        [Range(1, 99999999999, ErrorMessage = "Amount greater than 0")]
        public double amount { get; set; }
    }
}
