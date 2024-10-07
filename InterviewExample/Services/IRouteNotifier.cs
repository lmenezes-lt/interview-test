using InterviewExample.Models.DAO;

public interface IRouteNotifier
{
    Task Notify(RouteDAO routeDao, string eventName);
}