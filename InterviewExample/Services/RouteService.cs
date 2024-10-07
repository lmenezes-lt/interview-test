using InterviewExample.Models.DAO;
using InterviewExample.Repositories;

namespace InterviewExample.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IRouteNotifier _routeNotifier;

        public RouteService(IRouteRepository routeRepository,
            IShipmentRepository shipmentRepository,
            IRouteNotifier routeNotifier)
        {
            _routeRepository = routeRepository;
            _shipmentRepository = shipmentRepository;
            _routeNotifier = routeNotifier;
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

        public async Task CreateRoute(string name, string trailerId, string[] shipmentIds)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(trailerId))
            {
                List<ShipmentDAO> shipmentDaos = new List<ShipmentDAO>();
                foreach (var id in shipmentIds)
                {
                    var shipmentDao = _shipmentRepository.GetShipmentById(new Guid(id)).Result;
                    if (shipmentDao.IsLoadedOnRoute == true)
                        throw new Exception("Shipment already loaded");
                    if (shipmentDao.WasDelivered == true)
                        throw new Exception("Shipment already delivered");

                    shipmentDaos.Append(shipmentDao);
                }

                var routeDao = new RouteDAO
                {
                    Id = Guid.NewGuid(),
                    TrailerId = new Guid(trailerId),
                    TrailerName = null,
                    Name = name
                };

                await _routeRepository.CreateRoute(routeDao).ContinueWith(s =>
                {
                    _shipmentRepository.AssignToRoute(shipmentDaos, routeDao);
                });

                foreach (var shipmentDao in routeDao.Shipments)
                {
                    shipmentDao.IsLoadedOnRoute = true;
                    await _shipmentRepository.UpdateShipment(shipmentDao);
                }

                await _routeNotifier.Notify(routeDao, "route-created");
            }
            else
            {
                throw new Exception("cannot create route");
            }
        }
    }
}

public interface IRouteService
{
    public Task<List<RouteDAO>> GetByIds(string[] ids);
    public Task CreateRoute(string name, string trailerId, string[] shipmentIds);
}

