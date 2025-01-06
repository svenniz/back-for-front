using System.ComponentModel.DataAnnotations;

namespace BackForFrontApi.Dtos
{
    // Record type gør det noget simplere at lave en dto:
    public record HouseDetailsDto(int Id, [property: Required]string? Address, [property: Required]string? Country, int Price, string? description, string? photo);
}