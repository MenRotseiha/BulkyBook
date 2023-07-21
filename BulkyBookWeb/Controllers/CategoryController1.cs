using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController1 : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController1(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList= _db.Categories;//Store_db.Category on objCategoryList
            return View(objCategoryList);
        }
        //GET
        public IActionResult Createnew()
        {
            
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Createnew(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order can't exactly match Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);//add to database
                _db.SaveChanges();//Save to database
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		public IActionResult Edit(int? id)
		{
            if(id== null || id==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst=_db.Categories.FirstOrDefault(u=>u.ID==id);
			if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
		}
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "Display order can't exactly match Name");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);//add to database
				_db.SaveChanges();//Save to database
				TempData["success"] = "Category Edit Successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);//add to database
            _db.SaveChanges();//Save to database
			TempData["success"] = "Category Delete Successfully";
			return RedirectToAction("Index");
                 
        }

    }
}
