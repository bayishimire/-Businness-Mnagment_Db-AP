namespace Businness_Mnagment_Db_API.Models;

public class Orders
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = "Pending";
}