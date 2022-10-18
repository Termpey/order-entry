using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServicesCommonLibrary.Models;

namespace ClientService.Models {
    public sealed class ClientContact : IBaseModel {
        private string? _firstName;
        private string? _lastName;

        [Column("Id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Column("FirstName"), Required]
        public string FirstName { 
            get => _firstName
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(FirstName));
            set => _firstName = value; 
        }

        [Column("LastName"), Required]
        public string LastName { 
            get => _lastName
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(LastName));
            set => _lastName = value;
        }

        [Column("Title")]
        public string? Title { get; set; }

        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Column("Email")]
        public string? Email { get; set; }
        
        [Column("ContactTypeId"), Required]
        public int ContactTypeId { get; set; }

        [Column("ClientId"), Required]
        public int ClientId { get; set; }

        [Column("LocationId")]
        public int? LocationId { get; set; }

        [NotMapped]
        public ContactType? ContactType { get; set; }
    }
}