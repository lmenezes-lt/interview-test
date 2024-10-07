namespace InterviewExample.Models.DAO;

public class ShipmentDAO
{
    public Guid Id { get; set; }
    public string Consignee { get; set; }
    public string Shipper { get; set; }
    public bool IsLoadedOnRoute { get; set; }
    public bool WasDelivered { get; set; }
}