using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blog1.Models;

public class CustomUser : IdentityUser {
  public CustomUser() : base() { }
  // [Key]
  // public int ContributorId{get;set;}
[Required]
  public string? FirstName { get; set; }
  [Required]
  public string? LastName { get; set; }
      public string Status { get; set; }="Pending";
    public string Role { get; set; }="";

  public bool IsApproved{get; set;}=false;
}
