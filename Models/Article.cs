using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog1.Models;

public class Article
{
       [Key]
public int ArticleId{get; set;}
[Required]
[StringLength(200)]
public string Title {get;set;}
[Required]

public string Body {get; set;} //  Allow Html Content
public DateTime CreateDate{get;set;}=DateTime.UtcNow;
[Required]
public DateTime StartDate{get; set;}
[Required]
public DateTime EndDate{get; set;}
// [Required]
// public string ContributorUsername{get;set;}

// public CustomUser Contributor{get;set;}

}
