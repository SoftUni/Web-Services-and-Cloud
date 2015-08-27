namespace BugTracker.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public BugStatus Status { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
