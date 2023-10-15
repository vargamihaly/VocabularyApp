
// using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Persistence.MsSql.Entities;

public class Word
{
    [Key]
    public string WordTitle { get; set; }
    public string Description { get; set; }
}