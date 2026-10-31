namespace Businness_Mnagment_Db_API.Models;

public class Categories
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }
}