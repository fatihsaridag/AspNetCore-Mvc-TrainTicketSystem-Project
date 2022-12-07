using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;
using TrainTicket.Service.Abstract;

namespace TrainTicket.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITrainRouteService _trainRouteService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public AdminController(ITicketService ticketService, ITrainRouteService trainRouteService, ICityService cityService, IMapper mapper)
        {
            _ticketService = ticketService;
            _trainRouteService = trainRouteService;
            _cityService = cityService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var tickets = _ticketService.TGetAll();
            var trainRoutes = _trainRouteService.TGetAll();
            var Cities = _cityService.TGetAll();

            AdminList adminList = new AdminList()
            {
                Tickets = tickets,
                TrainRoutes = trainRoutes,
                Cities = Cities
            };
            return View(adminList);
        }

        public IActionResult TicketList()
        {
            var ticketList = _ticketService.TGetAll();
            return View(ticketList);
        }

        [HttpGet]
        public IActionResult TicketEdit(int id)
        {
            var ticket = _ticketService.TGetById(id);
            var ticketEditViewModel = ticket.Adapt<TicketEditViewModel>();
            return View(ticketEditViewModel);
        }


        [HttpPost]
        public IActionResult TicketEdit(TicketEditViewModel ticketEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticket = _ticketService.TGetById(ticketEditViewModel.TicketId);
                if (ticket != null)
                {
                    ticket.PhoneNumber = ticketEditViewModel.PhoneNumber;
                    ticket.Email = ticketEditViewModel.Email;
                    ticket.FirstName = ticketEditViewModel.FirstName;
                    ticket.LastName = ticketEditViewModel.LastName;
                    ticket.FromWhere = ticketEditViewModel.FromWhere;
                    ticket.ToWhere = ticketEditViewModel.ToWhere;
                    _ticketService.TUpdate(ticket);
                    return RedirectToAction("TicketList");
                }
                return View(ticketEditViewModel);
            }

            return View(ticketEditViewModel);

        }

        public IActionResult TicketDelete(int id)
        {
            var deletedTicket = _ticketService.TGetById(id);
            _ticketService.TDelete(deletedTicket);
            return RedirectToAction("TicketList");
        }

        [HttpGet]
        public  IActionResult TicketAdd()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TicketAdd(TicketAddViewModel ticketAddViewModel)
        {
            if (ModelState.IsValid)
            {
              var addedTicket=   _mapper.Map<Ticket>(ticketAddViewModel);
                _ticketService.TAdd(addedTicket);
                return RedirectToAction("TicketList");
            }
            return View(ticketAddViewModel);
        }



        public IActionResult CityList()
        {
            var cityList = _cityService.TGetAll();
            return View(cityList);
        }
        public IActionResult TrainRouteList()
        {
            var trainList = _trainRouteService.TGetAll();
            return View(trainList);
        }

        [HttpGet]
        public IActionResult TrainRouteEdit(int id)
        {

            TicketRouteViewModel ticketRouteViewModel = new TicketRouteViewModel();
            ticketRouteViewModel.Cities = _cityService.TGetAll();
            ViewBag.Cities = new SelectList(ticketRouteViewModel.Cities, "CityId", "CityName");
            var trainRoute  = _trainRouteService.TGetById(id);
            var TrainRouteViewModel = trainRoute.Adapt<TrainRouteEditViewModel>();




            return View(TrainRouteViewModel);
        }


        [HttpPost]
        public IActionResult TrainRouteEdit(TrainRouteEditViewModel trainUpdateViewModel)
        {
            var intStartRo = int.Parse(trainUpdateViewModel.StartRo);
            var intRo1 = int.Parse(trainUpdateViewModel.Ro1);
            var intRo2 = int.Parse(trainUpdateViewModel.Ro2);
            var intRo3 = int.Parse(trainUpdateViewModel.Ro3);
            var intFinishRo = int.Parse(trainUpdateViewModel.FinishRo);

            var startRoCityName = _cityService.TGetById(intStartRo).CityName;
            var ro1CityName = _cityService.TGetById(intRo1).CityName;
            var ro2CityName = _cityService.TGetById(intRo2).CityName;
            var ro3CityName = _cityService.TGetById(intRo3).CityName;
            var finishRoCityName = _cityService.TGetById(intFinishRo).CityName;
            if (ModelState.IsValid) {

                var trainRoute = _trainRouteService.TGetById(trainUpdateViewModel.RouteId);
                if (trainRoute != null)
                {
                    trainRoute.StartRo = startRoCityName;
                    trainRoute.Ro1 = ro1CityName;
                    trainRoute.Ro2 = ro2CityName;
                    trainRoute.Ro3 = ro3CityName;
                    trainRoute.FinishRo = finishRoCityName;
                    trainRoute.Time = trainUpdateViewModel.Time;
                    trainRoute.Clock = trainUpdateViewModel.Clock;
                    trainRoute.Image = trainUpdateViewModel.Image;
                    trainRoute.Price = trainUpdateViewModel.Price;
                    _trainRouteService.TUpdate(trainRoute);
                    return RedirectToAction("TrainRouteList");
                }
                return View(trainUpdateViewModel);
            }
            return View(trainUpdateViewModel);
        }



        public IActionResult TrainRouteDelete(int id)
        {
            var trainRoute = _trainRouteService.TGetById(id);
            _trainRouteService.TDelete(trainRoute);
            return RedirectToAction("TrainRouteList");
        }


        [HttpGet]
        public IActionResult TrainRouteAdd()
        {
            TicketRouteViewModel ticketRouteViewModel = new TicketRouteViewModel();
            ticketRouteViewModel.Cities = _cityService.TGetAll();
            ViewBag.Cities = new SelectList(ticketRouteViewModel.Cities, "CityId", "CityName");
            return View();
        }

        [HttpPost]
        public IActionResult TrainRouteAdd(TrainRouteAddViewModel trainRouteAddViewModel)
        {
            var intStartRo = int.Parse(trainRouteAddViewModel.StartRo);
            var intRo1 = int.Parse(trainRouteAddViewModel.Ro1);
            var intRo2 = int.Parse(trainRouteAddViewModel.Ro2);
            var intRo3 = int.Parse(trainRouteAddViewModel.Ro3);
            var intFinishRo = int.Parse(trainRouteAddViewModel.FinishRo);

            var startRoCityName = _cityService.TGetById(intStartRo).CityName;
            var ro1CityName = _cityService.TGetById(intRo1).CityName;
            var ro2CityName = _cityService.TGetById(intRo2).CityName;
            var ro3CityName = _cityService.TGetById(intRo3).CityName;
            var finishRoCityName = _cityService.TGetById(intFinishRo).CityName;


            if (ModelState.IsValid)
            {
               var trainRoute = new TrainRoute();
                trainRoute.StartRo = startRoCityName;
                trainRoute.Ro1 = ro1CityName;
                trainRoute.Ro2 = ro2CityName;
                trainRoute.Ro3 = ro3CityName;
                trainRoute.FinishRo = finishRoCityName;
                trainRoute.Time = trainRouteAddViewModel.Time;
                trainRoute.Clock = trainRouteAddViewModel.Clock;
                trainRoute.Image = trainRouteAddViewModel.Image;
                trainRoute.Price = trainRouteAddViewModel.Price;

                _trainRouteService.TAdd(trainRoute);
                return RedirectToAction("TrainRouteList");
            }
            return View(trainRouteAddViewModel);
        }

        [HttpGet]
         public IActionResult CityEdit(int id)
         {
            var city = _cityService.TGetById(id);
            var cityViewModel = city.Adapt<CityEditViewModel>();
            return View(cityViewModel);
        }

        [HttpPost]
        public IActionResult CityEdit(CityEditViewModel cityEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var city =   _cityService.TGetById(cityEditViewModel.CityId);
                city.CityName = cityEditViewModel.CityName;
                _cityService.TUpdate(city);
                return RedirectToAction("CityList");
            }
            return View(cityEditViewModel);
        }


        public IActionResult CityDelete(int id)
        {
           var city =  _cityService.TGetById(id);
           _cityService.TDelete(city);
            return RedirectToAction("CityList");
        }

        [HttpGet]
        public IActionResult CityAdd() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CityAdd(CityAddViewModel cityAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var cityEntity = _mapper.Map<City>(cityAddViewModel);
                _cityService.TAdd(cityEntity);
                return RedirectToAction("CityList");
            }
            return View(cityAddViewModel);
        }



  


    }
}
