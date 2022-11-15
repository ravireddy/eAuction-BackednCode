using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models
{
    public class RefreshTokendetails
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ExpireTimeInMinutes { get; set; }
        public string ProtectedToken { get; set; }
        public string ClientId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

    }
}
