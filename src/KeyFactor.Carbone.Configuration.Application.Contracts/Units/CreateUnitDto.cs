using System.ComponentModel.DataAnnotations;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class CreateUnitDto
    {
        [Required]
        [StringLength(UnitConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
