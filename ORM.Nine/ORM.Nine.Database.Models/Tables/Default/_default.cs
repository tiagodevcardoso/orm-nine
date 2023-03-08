using ORM.Nine.Database.Models.Attributes;

namespace ORM.Nine.Database.Models.Tables.Default
{
    public class _default
    {
        [PropertyField(Primary = true, Name = "_id", Type = "uniqueidentifier", Order = 0, Default = "newid()")]
        public Guid _Id { get; set; }

        [PropertyField(Name = "active", Type = "bit", Order = 9998, Default = "1")]
        public bool Active { set; get; }

        [PropertyField(Name = "created", Type = "datetime", Order = 9999, Default = "getdate()")]
        public DateTime? Created { set; get; }
    }
}