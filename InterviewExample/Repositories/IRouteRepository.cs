using InterviewExample.Models.DAO;

namespace InterviewExample.Repositories;

public interface IRouteRepository
{
    public Task<RouteDAO> GetRouteById(Guid id);
    public Task CreateRoute(RouteDAO route);
}