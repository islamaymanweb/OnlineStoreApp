using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineStore.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100 only!!!")]
        public int DiplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
