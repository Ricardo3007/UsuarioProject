using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsuarioProject.Domain.Entities;

public partial class Person
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string LastName { get; set; }

    [Required]
    [Column(TypeName = "varchar(5)")]
    public string DocumentType { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string DocumentNumber { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string Email { get; set; }

    public int UserId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public string FullName => $"{Name} {LastName}";
    public string Document => $"{DocumentType} - {DocumentNumber}";


    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
