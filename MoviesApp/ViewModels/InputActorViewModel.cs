using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels
{
    public class InputActorViewModel
    {
        [NameFilter]
        public string FirstName { get; set; }
        [NameFilter]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
