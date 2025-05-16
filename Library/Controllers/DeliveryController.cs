using Application.Features.Definitions.Delivery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllResrvDelivery()
        {
            var list = await _deliveryService.GetAllResrvDelivery();
            return View(list);
        }

       [HttpPost]
        public async Task<IActionResult> ReservationDelivery(long id)
        {
            var result = await _deliveryService.ReservationDelivery(id);

            if (result.Contains("خطا"))
            {
                return BadRequest(result); 
            }

            return Ok(result); 
        }


        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]       
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        public ActionResult Edit(int id)
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Delete(int id)
        {
            return View();
        }

       
        [HttpPost]
        
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
