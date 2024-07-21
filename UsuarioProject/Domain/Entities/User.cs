using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsuarioProject.Domain.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PersonId { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Username { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Password { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public bool Status { get; set; }

    public virtual Person Person { get; set; } = null!;
}
