using ORM.Nine.Database.Models.Attributes;
using ORM.Nine.Database.Models.Tables.Default;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Nine.Database.Models.Tables
{
    [Table("logs")]
    public class Logs : _default
    {
        [PropertyField(Name = "description", Type = "varchar(250)", Order = 2, Default = "")]
        public string? Description { get; set; }
    }
}