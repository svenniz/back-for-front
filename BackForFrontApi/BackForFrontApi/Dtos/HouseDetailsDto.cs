namespace BackForFrontApi.Dtos
{
    // Record type gør det noget simplere at lave en dto:
    public record HouseDetailsDto(int Id, string? Address, string? Country, int Price, string? description, string? photo);

    //public class HouseDetailsDto
    //{
    //    public HouseDetailsDto(int id, string? address, string? country, int price, string? description, string? photo)
    //    {
    //        Id = id;
    //        Address = address;
    //        Country = country;
    //        Price = price;
    //        Description = description;
    //        Photo = photo;
    //    }

    //    public int Id { get; set; }
    //    public string? Address { get; set; }
    //    public string? Country { get; set; }
    //    public int Price { get; set; }
    //    public string? Description { get; set; }
    //    public string? Photo { get; set; }
    //}
}
