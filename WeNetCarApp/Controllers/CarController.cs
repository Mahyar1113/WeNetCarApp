using System.Web.Mvc;

public class CarController : Controller
{
    private CarRepository carRepository;

    public CarController()
    {
        carRepository = new CarRepository();
    }

    public ActionResult Index()
    {
        var cars = carRepository.GetAllCars();
        return View(cars);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Store(Car car)
    {
        if (ModelState.IsValid)
        {
            carRepository.InsertCar(car);
            return RedirectToAction("Index");
        }
        else
        {
            return View("Create", car);
        }
    }

    public ActionResult Edit(int id)
    {
        var car = carRepository.GetCarById(id);
        if (car == null)
        {
            return HttpNotFound();
        }
        return View(car);
    }

    [HttpPost]
    public ActionResult Update(Car car)
    {
        if (ModelState.IsValid)
        {
            carRepository.UpdateCar(car);
            return RedirectToAction("Index");
        }
        else
        {
            return View("Edit", car);
        }
    }

    public ActionResult Delete(int id)
    {
        var car = carRepository.GetCarById(id);
        if (car == null)
        {
            return HttpNotFound();
        }
        return View(car);
    }

    [HttpPost]
    public ActionResult ConfirmDelete(Car car)
    {
        if (car == null)
        {
            return HttpNotFound();
        }
        carRepository.DeleteCar(car);
        return RedirectToAction("Index");
    }

    
}