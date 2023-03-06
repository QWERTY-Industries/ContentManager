using ContentManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Controllers
{
    public class UserController : Controller
    {
        FirebaseLink firebaseLink = new FirebaseLink();

        //Q The Methods Below will Call the Views when the Appropriate Buttons are Clicked
        public IActionResult AdminIndex()
        {
            List<User> userList = firebaseLink.PullUserList();
            List<User> adminList = new List<User>();

            foreach (var item in userList)
            {
                if (item.admin == true)
                {
                    adminList.Add(item);
                }
            }

            return View(adminList);
        }

        public IActionResult UserIndex()
        {
            List<User> userList = firebaseLink.PullUserList();
            List<User> standardUserList = new List<User>();

            foreach (var item in userList)
            {
                if (item.admin == false)
                {
                    standardUserList.Add(item);
                }
            }

            return View(standardUserList);
        }

        public IActionResult LogIn(User user)
        {
            bool correctDetails = false;
            bool admin = false;
            List<User> userList = firebaseLink.PullUserList();

            //Q Code From Create
            if (user.email != "")
            {
                foreach (var item in userList)
                {
                    if (user.email == item.email)
                    {
                        if (user.password == item.password)
                        {
                            correctDetails = true;
                            admin = item.admin;
                        }
                    }
                }
            }

            if (correctDetails)
            {
                //Q Log In User

                //Q Admin
                if (admin == true)
                {
                    return RedirectToAction("AdminIndex", "Category");
                }
                else //Q User
                {
                    return RedirectToAction("UserIndex", "Category");
                }
            }
            else
            {
                //Q Wrong Username and Password Combination

                if (user.email != "")
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Username/Password Combination");
                }

                return View();
            }
        }

        public ActionResult Register(User user)
        {
            //Q This Stops a User from being Created when the View is First Opened
            if (user.email != "")
            {
                firebaseLink.AddUser(user);

                ModelState.AddModelError(string.Empty, "Account Created Successfully");
            }

            return View();
        }

        public ActionResult Create(User user)
        {
            //Q This Stops an Admin from being Created when the View is First Opened
            if (user.email != "")
            {
                firebaseLink.AddAdmin(user);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }

            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            List<User> userList = firebaseLink.PullUserList();
            User detailsUser = new User();

            foreach (var item in userList)
            {
                if (item.id == id)
                {
                    detailsUser = item;
                }
            }

            return View(detailsUser);
        }

        public ActionResult Delete(string id)
        {
            firebaseLink.DeleteAdmin(id);
            List<User> usersList = firebaseLink.PullUserList();
            return View(usersList.Where(x => x.id == id));
        }
    }
}
