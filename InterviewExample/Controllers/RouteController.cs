using InterviewExample.Models.DTO;
using InterviewExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewExample.Controllers;

[ApiController]
[Route("routes")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly IRouteService _routeService;

    public RouteController(ILogger<RouteController> logger)
    {
        _logger = logger;
    }

    [HttpPost("get-by-id/")]
    public List<RouteDTO> GetById([FromBody]string[] ids, CancellationToken ct)
    {
        var routes = _routeService.GetByIds(ids).Result;

        List<RouteDTO> response = new List<RouteDTO>();
        foreach (var route in routes)
        {
            var routeDTO = new RouteDTO();
            routeDTO.Id = route.Id.ToString();
            routeDTO.Name = route.Name;
            routeDTO.TrailerId = route.TrailerId.ToString();
            routeDTO.TrailerName = route.TrailerName;
            routeDTO.Shipments = new List<ShipmentDTO>();

            foreach (var shipment in route.Shipments)
            {
                var shipmentsDTO = new ShipmentDTO();
                shipmentsDTO.Id = shipment.Id.ToString();
                shipmentsDTO.Consignee = shipment.Consignee;
                shipmentsDTO.Shipper = shipment.Shipper;
                shipmentsDTO.WasDelivered = shipment.WasDelivered;
                shipmentsDTO.IsLoadedOnRoute = shipment.IsLoadedOnRoute;

                routeDTO.Shipments.Append(shipmentsDTO);
            }

            response.Append(routeDTO);
        }

        return response;
    }

    [HttpPost("create-route/")]
    public Task CreateRoute(
        [FromBody]string name,
        [FromBody] string trailerId,
        [FromBody] string[] shipmentIds,
        CancellationToken ct)
    {
        return _routeService.CreateRoute(name, trailerId, shipmentIds);
    }
}