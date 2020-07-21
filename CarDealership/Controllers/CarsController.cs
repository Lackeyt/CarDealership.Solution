using Microsoft.AspNetCore.Mvc;
using Dealership.Models;
using System;
using System.Collections.Generic;

namespace Dealership.Controllers
{
    public class CarsController : Controller
    {

      [HttpGet("/Cars")]
      public ActionResult Index(double maxPrice = Double.PositiveInfinity)
      {
        List<Car> Cars = new List<Car>();
        foreach(Car car in Car.GetAll())
        {
          if (car.Price < maxPrice)
          {
            Cars.Add(car);
          }
        }
        return View(Cars);
      }
      
      [HttpPost("/Cars")]
      public ActionResult FilterPrice(string stringMaxPrice)
      {
        double maxPrice = double.Parse(stringMaxPrice);
        return RedirectToAction("Index", maxPrice);
      }
    }
}