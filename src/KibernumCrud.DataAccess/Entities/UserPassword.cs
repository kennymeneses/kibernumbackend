using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KibernumCrud.DataAccess.Entities;

[Table("userpasswords", Schema = "public")]
public class UserPassword: BaseEntity
{
    [Column("userid")]
    public int UserId { get; set; }
    
    [Column("password")]
    [MaxLength(350)]
    public string Password { get; set; } = string.Empty;
    
    public virtual User User { get; set; }
}