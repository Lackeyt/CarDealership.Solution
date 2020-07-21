using Microsoft.AspNetCore.Mvc;
using Dealership.Models;
using System;
using System.Collections.Generic;

namespace Dealership.Controllers
{
  public class CarsController : Controller
  {
    [HttpGet("/Cars")]
    public ActionResult Index(double maxPrice = Double.PositiveInfinity, double minPrice = 0, double minMileage = 0, double maxMileage = double.PositiveInfinity)
    {
      List<Car> Cars = new List<Car>();
      foreach(Car car in Car.GetAll())
      {
        if (car.Price <= maxPrice && car.Price >= minPrice && car.Miles <= maxMileage && car.Miles >= minMileage)
        {
          Cars.Add(car);
        }
      }
      return View(Cars);
    }
    
    [HttpPost("/Cars")]
    public ActionResult FilterPrice(string stringMaxPrice, string stringMinPrice, string stringMinMileage, string stringMaxMileage)
    {  
      double newMaxPrice;
      double newMinPrice;
      double newMinMileage;
      double newMaxMileage;

      if (stringMaxPrice == null)
      {
        newMaxPrice = Double.PositiveInfinity;
      } 
      else
      {
        newMaxPrice = double.Parse(stringMaxPrice);
      }
      
      if (stringMinPrice == null)
      {
        newMinPrice = 0;
      }
      else
      {
        newMinPrice = double.Parse(stringMinPrice);
      }
      
      if (stringMinMileage == null)
      {
        newMinMileage = 0;
      }
      else
      {
        newMinMileage = double.Parse(stringMinMileage);
      }

      if (stringMaxMileage == null)
      {
        newMaxMileage = double.PositiveInfinity;
      }
      else
      {
        newMaxMileage = double.Parse(stringMaxMileage);
      }
      return RedirectToAction("Index", new { maxPrice = newMaxPrice, minPrice = newMinPrice, minMileage = newMinMileage, maxMileage = newMaxMileage });
    }

    [HttpGet("/Cars/Add")]
    public ActionResult AddCar()
    {
      return View();
    }

    [HttpPost("/Cars/Add")]
    public ActionResult AddCarToList(string makeModel, string price, string miles)
    {
      int newPrice = int.Parse(price);
      int newMiles = int.Parse(miles);
      Car newCar = new Car(makeModel, newPrice, newMiles);
      return RedirectToAction("Index");
    }

    [HttpGet("/Cars/Remove")]
    public ActionResult RemoveCar()
    {
      return View();
    }

    [HttpPost("/Cars/Remove")]
    public ActionResult RemoveCarFromList(string carToRemove)
    {
      List<Car> carList = Car.GetAll();
      for(int i=0; i < carList.Count; i++)
      {
        if (carList[i].MakeModel == carToRemove)
        {
          carList[i].RemoveCar();
          break;
        }
      }
      return RedirectToAction("Index");
    }
  }
}