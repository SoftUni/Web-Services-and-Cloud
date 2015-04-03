namespace SoftUniSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GameResult
    {
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        public byte Bulls { get; set; }

        public byte Cows { get; set; }

        public Guid GameId { get; set; }
        
        public virtual Game Game { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
