using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animals.Core.Models
{
    [Table("Animals")]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Name", Order = 2, TypeName = "nvarchar(80)")]
        public string? Name { get; set; }


        [ForeignKey("Specie_Id")]
        [Column("Specie", Order = 3, TypeName = "nvarchar(100)")]
        public string Specie { get; set; }
    }
}