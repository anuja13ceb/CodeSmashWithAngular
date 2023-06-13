using System.ComponentModel.DataAnnotations;

namespace CodeSmashWithAngular.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
