using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServicesCommonLibrary.Models;

namespace ItemService.Models {
    public sealed class Item : IBaseModel {
        private string? _name;
        private ItemCategory? _itemCategory;

        [Column("Id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name"), Required]
        public string Name {
            get => _name 
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Name));
            set => _name = value;
        }

        [Column("Description")]
        public string? Description{ get; set; }

        [Column("Cost", TypeName="smallmoney"), Required]
        public decimal Cost { get; set; }

        [Column("Price", TypeName="smallmoney"), Required]
        public decimal Price { get; set; }

        [Column("ItemCategoryId"), Required]
        public int ItemCategoryId { get; set; }

        [NotMapped]
        public ItemCategory ItemCategory { 
            get => _itemCategory
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ItemCategory)); 
            set => _itemCategory = value; 
        }
    }
}