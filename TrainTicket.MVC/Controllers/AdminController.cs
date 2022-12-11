using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;
using TrainTicket.MVC.Models;
using TrainTicket.Service.Abstract;

namespace TrainTicket.MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITrainRouteService _trainRouteService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(ITicketService ticketService, ITrainRouteService trainRouteService, ICityService cityService, IMapper mapper,UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _ticketService = ticketService;
            _trainRouteService = trainRouteService;
            _cityService = cityService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> TrainRouteEdit(TrainRouteEditViewModel trainUpdateViewModel, IFormFile Image)
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

                if (Image != null && Image.Length > 0)
                {
                    
                    var trainRoute = _trainRouteService.TGetById(trainUpdateViewModel.RouteId);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TrenRouteMap", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                        trainRoute.Image = "/TrenRouteMap/" + fileName;

                    };

                    if (trainRoute != null)
                    {
                        trainRoute.StartRo = startRoCityName;
                        trainRoute.Ro1 = ro1CityName;
                        trainRoute.Ro2 = ro2CityName;
                        trainRoute.Ro3 = ro3CityName;
                        trainRoute.FinishRo = finishRoCityName;
                        trainRoute.Time = trainUpdateViewModel.Time;
                        trainRoute.Clock = trainUpdateViewModel.Clock;
                        trainRoute.Price = trainUpdateViewModel.Price;
                        _trainRouteService.TUpdate(trainRoute);
                        return RedirectToAction("TrainRouteList");
                    }
                    return View(trainUpdateViewModel);
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
        public async Task<IActionResult> TrainRouteAdd(TrainRouteAddViewModel trainRouteAddViewModel,  IFormFile Image)
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
                if (Image != null && Image.Length >0)
                {
                    var trainRoute = new TrainRoute();
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TrenRouteMap", fileName);
                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                        trainRoute.Image = "/TrenRouteMap/" + fileName;
                    }

                    trainRoute.StartRo = startRoCityName;
                    trainRoute.Ro1 = ro1CityName;
                    trainRoute.Ro2 = ro2CityName;
                    trainRoute.Ro3 = ro3CityName;
                    trainRoute.FinishRo = finishRoCityName;
                    trainRoute.Time = trainRouteAddViewModel.Time;
                    trainRoute.Clock = trainRouteAddViewModel.Clock;
                    trainRoute.Price = trainRouteAddViewModel.Price;

                    _trainRouteService.TAdd(trainRoute);
                    return RedirectToAction("TrainRouteList");
                }
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

        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userViewModel = user.Adapt<UserViewModel>();

            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
          var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userViewModel = user.Adapt<UserViewModel>();
            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
              var user = await  _userManager.FindByNameAsync(User.Identity.Name);   //Cookie üzerinden

                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
               var result =await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyProfile");
                }
                return View(userViewModel);
            }
            return View(userViewModel);
        }


    }
}
