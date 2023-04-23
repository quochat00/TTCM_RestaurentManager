//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Session;
//using TTCM_RestaurentManager.Models;

//namespace TTCM_RestaurentManager.Controllers
//{
//    public class CartController : Controller
//    {
//        RestaurentManagerContext db = new RestaurentManagerContext();
//        /* Add to Cart List use */
//        List<CartItem> li = new List<CartItem>();
//        // GET: Cart
//        public ActionResult AddtoCart(int id)
//        {
//            var query = db.MonAns.Where(x => x.MaMa == id).SingleOrDefault();
//            return View(query);
//        }

//        [HttpPost]
//        public ActionResult AddtoCart(string id, int qty)
//        {
//            MonAn p = db.MonAns.Where(x => x.MaMa == id).SingleOrDefault();
//            CartItem c = new CartItem();
//            c.MonAn.MaMa = id;
//            c.MonAn.TenMa = p.TenMa;
//            c.MonAn.Gia = Convert.ToInt32(p.Gia);
//            c.Quantity = Convert.ToInt32(qty);
//            c.Total = c.MonAn.Gia * qty;
//            if (TempData["cart"] == null)
//            {
//                li.Add(c);
//                TempData["cart"] = li;
//            }
//            else
//            {
//                List<CartItem> li2 = TempData["cart"] as List<CartItem>;
//                int flag = 0;
//                foreach (var item in li2)
//                {
//                    if (item.MonAn.MaMa == c.MonAn.MaMa)
//                    {
//                        item.Quantity += qty;
//                        item.Total += c.Total;
//                        flag = 1;
//                    }

//                }
//                if (flag == 0)
//                {
//                    li2.Add(c);
//                    //new item
//                }
//                TempData["cart"] = li2;

//            }

//            TempData.Keep();

//            return RedirectToAction("Index");
//        }
//    }
//}
