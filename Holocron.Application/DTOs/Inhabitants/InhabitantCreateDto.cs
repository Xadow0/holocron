namespace Holocron.Application.DTOs.Inhabitants
{ //comentario random para ver si funciona el Build Validation en las PR
    public class InhabitantCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string? Origin { get; set; }
        public bool IsSuspectedRebel { get; set; }
    }
}
