using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibernumCrud.DataAccess.Entities;

[Table("contacts", Schema = "public")]
public class Contact : BaseEntity
{
    [Column("name")]
    [MaxLength(20)]
    public string Name { get; init; } = string.Empty;
    
    [Column("phone")]
    [MaxLength(20)]
    public string PhoneNumber { get; init; } = string.Empty;
    
    [Column("userid")]
    public int UserId { get; init; }
    
    [NotMapped]
    public virtual User? User { get; set; }
}