using System.ComponentModel.DataAnnotations;

namespace DBSP.RememberMe.API.Model
{
  public class Contact
  {
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string OwnerId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(100)]
    public string Phone { get; set; }

    [StringLength(100)]
    public string Organization { get; set; }

    [StringLength(500)]
    public string Misc { get; set; }
  }
}
