using InterviewExample.Models.DAO;

namespace InterviewExample.Repositories;

public interface IShipmentRepository
{
    public Task<ShipmentDAO> GetShipmentById(Guid id);
    public Task AssignToRoute(List<ShipmentDAO> shipmentDaos, RouteDAO routeDao);
    public Task UpdateShipment(ShipmentDAO shipmentDao);
}