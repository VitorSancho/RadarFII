using System.ComponentModel.DataAnnotations;

namespace ToDo.WebAPI.ViewModels
{
    public abstract class EntityViewModel
    {
        protected EntityViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

//        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}