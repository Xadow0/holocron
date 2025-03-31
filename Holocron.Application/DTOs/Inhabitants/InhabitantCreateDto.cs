namespace Holocron.Application.DTOs.Inhabitants
{
    public class InhabitantCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string? Origin { get; set; }
        public bool IsSuspectedRebel { get; set; }
    }
}
