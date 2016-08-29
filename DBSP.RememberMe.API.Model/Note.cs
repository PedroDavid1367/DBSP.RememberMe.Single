using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.API.Model
{
  public class Note
  {
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string OwnerId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(3000)]
    public string Content { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; }

    [Required]
    public int Priority { get; set; }
  }
}
