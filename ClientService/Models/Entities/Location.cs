using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServicesCommonLibrary.Models;

namespace ClientService.Models {
    public sealed class Location : IBaseModel {
        private string? _name;
        private string? _address1;
        private string? _city;
        private string? _state;
        private string? _country;
        private string? _postalCode;

        [Column("Id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name"), Required]
        public string Name { 
            get => _name
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Name));
            set => _name = value;
        }

        [Column("Address1"), Required]
        public string Address1 { 
            get => _address1
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Address1));
            set => _address1 = value; 
        }

        [Column("Address2")]
        public string? Address2 { get; set; }

        [Column("City"), Required]
        public string City { 
            get => _city
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(City));
            set => _city = value; 
        }

        [Column("State"), Required]
        public string State { 
            get => _state
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(State));
            set => _state = value; 
        }

        [Column("Country"), Required]
        public string Country { 
            get => _country
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Country));
            set => _country = value; 
        }

        [Column("PostalCode"), Required]
        public string PostalCode { 
            get => _postalCode
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(PostalCode));  
            set => _postalCode = value; 
        }

        [Column("ClientId"), Required]
        public int ClientId { get; set; }

        [NotMapped]
        public List<ClientContact>? Contacts { get; set; }
    }
}