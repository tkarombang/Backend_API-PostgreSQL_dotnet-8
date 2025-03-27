using System.ComponentModel.DataAnnotations;

public class CreateDeveloperDTO
{
  [Required]
  public required string Nama { get; set; }

  [Required]
  [EmailAddress]
  public required string Email { get; set;}
  public string? Role { get; set;}
  public string? Phone { get; set;}
  public DateTime? TanggalLahir { get; set;}
  public string? Status { get; set;}
  public int? Gender { get; set;}
}

public class UpdateDeveloperDTO
{
  public int DeveloperId { get; set;} 

  [Required]
  public required string Nama {get; set;}

  [Required]
  [EmailAddress]
  public required string Email {get; set;}
  public string? Role {get; set;}
  public string? Phone {get; set;}
  public DateTime? TanggalLahir {get; set;}
  public string? Status {get; set;}
  public int? Gender {get; set;}
}