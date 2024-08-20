using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibernumCrud.DataAccess.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    { 
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("uuid")]
    public Guid Uuid { get; set; }
    
    [Column("creation_date", TypeName = "timestamp with time zone")]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    
    [Column("deleted")]
    public bool Deleted { get; set; }
}