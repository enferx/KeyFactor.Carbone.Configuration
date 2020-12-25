using System.ComponentModel.DataAnnotations;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class UpdateUnitDto
    {
        [Required]
        [StringLength(UnitConsts.MaxNameLength)]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
