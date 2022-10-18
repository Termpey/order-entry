using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesCommonLibrary.Models {
    public interface IBaseModel {
        [Column("Id"), Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
    }
}