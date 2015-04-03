namespace SoftUniSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.Id = Guid.NewGuid();
            this.State = GameState.WaitingForPlayer;
        }

        public Guid Id { get; set; }
        
        public GameState State { get; set; }

        [Required]
        public string Number { get; set; }

        public virtual ICollection<GameResult> Results { get; set; }

        [Required]
        public string UserOneId { get; set; }

        public virtual ApplicationUser UserOne { get; set; }

        public string UserTwoId { get; set; }

        public virtual ApplicationUser UserTwo { get; set; }
    }
}
