namespace InterviewExample.Models.DTO;

public class ShipmentDTO
{
    public string Id { get; set; }
    public string Consignee { get; set; }
    public string Shipper { get; set; }
    public bool IsLoadedOnRoute { get; set; }
    public bool WasDelivered { get; set; }
}