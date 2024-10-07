namespace InterviewExample.Models.DAO;

public class RouteDAO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TrailerName { get; set; }
    public Guid TrailerId { get; set; }
    public List<ShipmentDAO> Shipments { get; set; }
}