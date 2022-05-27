using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class DetailsController : Controller
    {
        CsvDb db = new CsvDb("uj.txt");
        // GET: DetailsController
        public ActionResult Index()
        {
            return View("Details");
        }

        // GET: DetailsController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user =  db.FindAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

            //return View("DetailsView");
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,LastName,FirstName,BirthDate,BirthPlace,City")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
  
                await db.UpdateAsync(user);
                return RedirectToAction(actionName: "Grid", controllerName: "Home");
                //Response.Redirect("~/Home/Grid");
                //return View("Home/Grid");
            }

            return View(user);
        }

        // GET: DetailsController/Add
        public ActionResult Add()
        {
            return View();

            //return View("Details");
        }
        

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,LastName,FirstName,BirthDate,BirthPlace,City")] User user)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(user);
                return RedirectToAction(actionName: "Grid", controllerName: "Home");
            }
            return View(user);
        }

        //// GET: DetailsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DetailsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: DetailsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: DetailsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: DetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
