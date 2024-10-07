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

    [HttpGet("{ids}")]
    public List<RouteDTO> GetById(string[] ids)
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
}