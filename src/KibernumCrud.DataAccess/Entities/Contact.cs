using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibernumCrud.DataAccess.Entities;

public class Contact : BaseEntity
{
    [Column("name")]
    [MaxLength(20)]
    public string Name { get; init; } = string.Empty;
    
    [Column("phone")]
    [MaxLength(20)]
    public string PhoneNumber { get; init; } = string.Empty;
}