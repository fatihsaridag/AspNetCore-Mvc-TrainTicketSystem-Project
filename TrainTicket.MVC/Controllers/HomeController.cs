using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TrainTicket.MVC.Models;
using TrainTicket.Service.Abstract;

namespace TrainTicket.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICityService _cityService;
        private readonly ITrainRouteService _trainRouteService;

        public HomeController(ICityService cityService, ITrainRouteService trainRouteService)
        {
            _cityService = cityService;
            _trainRouteService = trainRouteService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            TicketRouteViewModel ticketRouteViewModel = new TicketRouteViewModel();
            ticketRouteViewModel.Cities = _cityService.TGetAll();
            ViewBag.Cities = new SelectList(ticketRouteViewModel.Cities, "CityId", "CityName");
            return View();
        }

        [HttpPost]
        public IActionResult Index(TicketRouteViewModel ticketRouteViewModel)
        {
            if (ticketRouteViewModel.WhereToTrainRoute.StartRo == null || ticketRouteViewModel.WhereToTrainRoute.FinishRo == null || ticketRouteViewModel.WhereToTrainRoute.StartRo == ticketRouteViewModel.WhereToTrainRoute.FinishRo)
            {
                ticketRouteViewModel.WhereToTrainRoute = null;
                return View(ticketRouteViewModel);
            }
            else
            {
                var intStartRo =  Int16.Parse(ticketRouteViewModel.WhereToTrainRoute.StartRo);
                var intFinishRo = Int16.Parse(ticketRouteViewModel.WhereToTrainRoute.FinishRo);
                var StartCityName = _cityService.TGetById(intStartRo);
                var FinishCityName = _cityService.TGetById(intFinishRo);
                  
                var trainRoute = _trainRouteService.WhereToTrainRoute(StartCityName.CityName, FinishCityName.CityName);
                TempData["StartRo"] = trainRoute.StartRo;
                TempData["FinishRo"] = trainRoute.FinishRo;
       
                return View(ticketRouteViewModel);
            }


        }
    }
}
