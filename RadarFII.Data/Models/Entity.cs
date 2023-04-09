using System.ComponentModel.DataAnnotations;

namespace RadarFII.Data.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

//        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}