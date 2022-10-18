using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServicesCommonLibrary.Models;

namespace ItemService.Models {
    public sealed class ItemCategory : IBaseModel{
        private string? _name;

        [Column("Id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name"), Required]
        public string Name {
            get => _name 
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Name));
            set => _name = value;
        }
    }
}