namespace InterviewExample.Models.DTO;

public class RouteDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TrailerName { get; set; }
    public string TrailerId { get; set; }
    public List<ShipmentDTO> Shipments { get; set; }
}