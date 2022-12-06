using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;
using TrainTicket.Service.Abstract;

namespace TrainTicket.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICityService _cityService;
        private readonly ITrainRouteService _trainRouteService;
        private readonly ITicketService _ticketService;

        public HomeController(ICityService cityService, ITrainRouteService trainRouteService, ITicketService ticketService)
        {
            _cityService = cityService;
            _trainRouteService = trainRouteService;
            _ticketService = ticketService;
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

            var intStartRo = Int16.Parse(ticketRouteViewModel.WhereToTrainRoute.StartRo);
            var intFinishRo = Int16.Parse(ticketRouteViewModel.WhereToTrainRoute.FinishRo);
            var StartCityName = _cityService.TGetById(intStartRo);
            var FinishCityName = _cityService.TGetById(intFinishRo);
            var trainRoute2 = _trainRouteService.WhereToTrainRoute(StartCityName.CityName, FinishCityName.CityName);




            if (trainRoute2 == null)
            {
                ticketRouteViewModel.WhereToTrainRoute = null;
                ViewBag.status = "false";
                ticketRouteViewModel.Cities = _cityService.TGetAll();
                ViewBag.Cities = new SelectList(ticketRouteViewModel.Cities, "CityId", "CityName");

                return View(ticketRouteViewModel);

            }
            else
            {
                ticketRouteViewModel.Cities = _cityService.TGetAll();
                ViewBag.Cities = new SelectList(ticketRouteViewModel.Cities, "CityId", "CityName");
                var trainRoute = _trainRouteService.WhereToTrainRoute(StartCityName.CityName, FinishCityName.CityName);
                TempData["StartRo"] = trainRoute.StartRo;
                TempData["Ro1"] = trainRoute.Ro1;
                TempData["Ro2"] = trainRoute.Ro2;
                TempData["Ro3"] = trainRoute.Ro3;
                TempData["FinishRo"] = trainRoute.FinishRo;
                TempData["Time"] = trainRoute.Time;
                TempData["Clock"] = trainRoute.Clock;
                TempData["Price"] = trainRoute.Price;
                TempData["TrainRouteId"] = trainRoute.RouteId;
                ViewBag.status = "true";
                return View(ticketRouteViewModel);
            }


        }


        [HttpGet]
        public IActionResult TicketBuy(int id)
        {
           var trainRoute =  _trainRouteService.TGetById(id);
            TempData["TrainRouteId"] = trainRoute.RouteId.ToString();
            TempData["StartRo"] = trainRoute.StartRo;
            TempData["FinishRo"] = trainRoute.FinishRo;
            TempData["Time"] = trainRoute.Time;
            TempData["Clock"] = trainRoute.Clock;
            TempData["Price"] = trainRoute.Price;

            string data = JsonSerializer.Serialize(trainRoute);
            TempData["trainRoute"] = data;

            return View();
        }


        [HttpPost]
        public IActionResult TicketBuy(TicketBuyViewModel ticketBuyViewModel)
        {

            var data = TempData["trainRoute"].ToString();
            var products = JsonSerializer.Deserialize<TrainRoute>(data);


            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket()
                {
                    FirstName = ticketBuyViewModel.FirstName,
                    LastName = ticketBuyViewModel.LastName,
                    Email = ticketBuyViewModel.Email,
                    PhoneNumber = ticketBuyViewModel.PhoneNumber,
                    TrainRouteId = products.RouteId,
                    FromWhere = products.StartRo,
                    ToWhere = products.FinishRo,
                    Price = products.Price
                };
                _ticketService.TAdd(ticket);
                ViewBag.success = "true";
                return View(ticketBuyViewModel);
            }
            ViewBag.status = "error";
            return View(ticketBuyViewModel);

        }

    }
}
