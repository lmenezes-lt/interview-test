using InterviewExample.Models.DAO;
using InterviewExample.Repositories;

namespace InterviewExample.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IShipmentRepository _shipmentRepository;

        public RouteService(IRouteRepository routeRepository,
            IShipmentRepository shipmentRepository)
        {
            _routeRepository = routeRepository;
            _shipmentRepository = shipmentRepository;
        }

        public Task<List<RouteDAO>> GetByIds(string[] ids)
        {
            List<RouteDAO> routes = new List<RouteDAO>();
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("invalid id");
                }

                var route = _routeRepository.GetRouteById(new Guid(id)).Result;
                routes.Append(route);
            }

            return Task.FromResult(routes);
        }

        public async Task<RouteDAO> LoadRouteById(string id, string[] shipmentIds)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Invalid id");
            }

            var route = _routeRepository.GetRouteById(new Guid(id)).Result;

            foreach (var shipmentId in shipmentIds)
            {
                var shipment = _shipmentRepository.GetShipmentById(new Guid(shipmentId)).Result;
                if (shipment.IsLoadedOnRoute == true)
                {
                    throw new Exception("Shipment already loaded");
                }

                if (shipment.WasDelivered == true)
                {
                    throw new Exception("Shipment already delivered");
                }

                shipment.IsLoadedOnRoute = true;
                route.Shipments.Append(shipment);
                _routeRepository.UpdateRoute(route);
            }

            return route;
        }
    }
}

public interface IRouteService
{
    public Task<List<RouteDAO>> GetByIds(string[] ids);
    public Task<RouteDAO> LoadRouteById(string id, string[] shipmentIds);
}

