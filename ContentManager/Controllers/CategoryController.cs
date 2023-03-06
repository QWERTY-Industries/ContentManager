using ContentManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Controllers
{
    public class CategoryController : Controller
    {
        FirebaseLink firebaseLink = new FirebaseLink();

        //Q The Methods Below will Call the Views when the Appropriate Buttons are Clicked
        public IActionResult AdminIndex()
        {
            List<Category> categoryList = firebaseLink.PullCategoryList();

            return View(categoryList);
        }

        public IActionResult UserIndex(string id)
        {
            List<Category> categoryList = firebaseLink.PullCategoryList();

            return View(categoryList);
        }

        public ActionResult Create(Category category)
        {
            //Q This Stops a Category from being Created when the View is First Opened
            if (category.categoryName != "")
            {
                firebaseLink.AddCategory(category);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            List<Category> clistTEST = firebaseLink.PullCategoryList();
            return View(clistTEST.Where(x => x.categoryId == id));
        }

        public ActionResult AdminDetails(string id)
        {
            List<Category> categoryList = firebaseLink.PullCategoryList();
            string imageURL = new string("");
            string videoURL = new string("");
            string audioURL = new string("");

            foreach (var item in categoryList)
            {
                if (item.categoryId == id)
                {
                    imageURL = item.categoryImageUrl;
                    videoURL = item.categoryVideoUrl;
                    audioURL = item.categoryAudioUrl;
                }
            }

            ViewBag.Image = imageURL;
            ViewBag.Video = videoURL;
            ViewBag.Audio = audioURL;
            return View(categoryList.Where(x => x.categoryId == id));
        }

        public ActionResult UserDetails(string id)
        {
            List<Category> categoryList = firebaseLink.PullCategoryList();
            string imageURL = new string("");
            string videoURL = new string("");
            string audioURL = new string("");

            foreach (var item in categoryList)
            {
                if (item.categoryId == id)
                {
                    imageURL = item.categoryImageUrl;
                    videoURL = item.categoryVideoUrl;
                    audioURL = item.categoryAudioUrl;
                }
            }

            ViewBag.Image = imageURL;
            ViewBag.Video = videoURL;
            ViewBag.Audio = audioURL;

            return View(categoryList.Where(x => x.categoryId == id));
        }

        public ActionResult Delete(string id)
        {
            firebaseLink.DeleteCategory(id);
            List<Category> categoryList = firebaseLink.PullCategoryList();
            return View(categoryList.Where(x => x.categoryId == id));
        }
    }
}
