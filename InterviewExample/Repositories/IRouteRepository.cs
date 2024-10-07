using InterviewExample.Models.DAO;

namespace InterviewExample.Repositories;

public interface IRouteRepository
{
    public Task<RouteDAO> GetRouteById(Guid id);
    Task UpdateRoute(RouteDAO route);
}