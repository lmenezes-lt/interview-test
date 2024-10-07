using InterviewExample.Models.DAO;

namespace InterviewExample.Repositories;

public interface IShipmentRepository
{
    public Task<ShipmentDAO> GetShipmentById(Guid id);
}