using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibernumCrud.DataAccess.Entities;

public class User : BaseEntity
{
    [Column("name")]
    [MaxLength(25)]
    public string Name { get; init; } = string.Empty;
    
    [Column("lastname")]
    [MaxLength(25)]
    public string LastName { get; init; } = string.Empty;
    
    [Column("email")]
    [MaxLength(35)]
    public string Email { get; init; } = string.Empty;
    
    [NotMapped]
    public virtual UserPassword? UserPassword { get; set; }
}