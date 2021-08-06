using System;
using System.ComponentModel.DataAnnotations;

namespace PosLibrary.Model.Entities
{
    public class CommonEntity
    {
        [Required]
        public int Id { get; set; } = 0;
        [Required]
        public bool Deleted { get; set; } = false;
        [Required]
        public bool Condition_Status { get; set; } = true;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
