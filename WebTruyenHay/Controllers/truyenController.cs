using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebTruyenHay.Models;

namespace WebTruyenHay.Controllers
{
    public class truyenController : Controller
    {
        truyenEntities1 truyen = new truyenEntities1();
        // GET: truyen
        public ActionResult Index(string category)
        {

            if (category == null)
            {
                var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe);
                return View(productList);
            }
            else
            {
                var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe)
                .Where(x => x.theloai == category);
                return View(productList);
            }

        }
        public ActionResult Indexuser(string category)
        {

            if (category == null)
            {
                var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe);
                return View(productList);
            }
            else
            {
                var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe)
                .Where(x => x.theloai == category);
                return View(productList);
            }

        }
        public ActionResult Create()
        {
            Truyen sach = new Truyen();
            return View(sach);
        }
        [HttpPost]

        public ActionResult Create(Truyen sach)
        {
            try
            {
                if (sach.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sach.UploadImage.FileName);
                    string extent = Path.GetExtension(sach.UploadImage.FileName);
                    filename = filename + extent;
                    sach.imagetruyen = "~/Content/images/" + filename;
                    sach.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    sach.NgayTao = DateTime.Now;
                }
                truyen.Truyens.Add(sach);
                truyen.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var product = truyen.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Truyen sach)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingProduct = truyen.Truyens.Find(sach.IDtruyen);
                    if (existingProduct == null)
                    {
                        return HttpNotFound();
                    }

                    // Cập nhật thông tin truyên
                    existingProduct.TieuDe = sach.TieuDe;
                    existingProduct.MoTa = sach.MoTa;
                    existingProduct.theloai = sach.theloai;
                    existingProduct.TrangThai = sach.TrangThai;

                    // Cập nhật hình ảnh nếu có
                    if (sach.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(sach.UploadImage.FileName);
                        string extent = Path.GetExtension(sach.UploadImage.FileName);
                        filename = filename + extent;
                        existingProduct.imagetruyen = "~/Content/images/" + filename;
                        sach.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }

                    // Lưu thay đổi vào database
                    truyen.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(sach);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Detailsuser(int id)
        {
            var product = truyen.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Details(int id)
        {
            var product = truyen.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            var sach = truyen.Truyens.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = truyen.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            // Xóa sản phẩm
            truyen.Truyens.Remove(product);
            truyen.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString)
        {
            var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe);

            if (!string.IsNullOrEmpty(searchString))
            {
                productList = (IOrderedQueryable<Truyen>)productList.Where(x => x.TieuDe.Contains(searchString));
            }

            return View("Index", productList.ToList());
        }
        public ActionResult Searchuser(string searchString)
        {
            var productList = truyen.Truyens.OrderByDescending(x => x.TieuDe);

            if (!string.IsNullOrEmpty(searchString))
            {
                productList = (IOrderedQueryable<Truyen>)productList.Where(x => x.TieuDe.Contains(searchString));
            }
            var email = "minhchi521@gmail.com"; // Replace with the actual email or retrieve it dynamically
            var mailService = new MailService();
            Task.Run(() => mailService.SendMailAsync(email, "Test new", "Sản phẩm mượn bị quá hạn"));
            return View("Indexuser", productList.ToList());
        }
        public ActionResult AdvancedSearch(string title, string author, string category)
        {
            var books = truyen.Truyens.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.TieuDe.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.TacGiaID.Contains(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.theloai.Contains(category));
            }
            return View("Index", books.ToList());
        }
        public ActionResult AdvancedSearchuser(string title, string author, string category)
        {
            var books = truyen.Truyens.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.TieuDe.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.TacGiaID.Contains(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.theloai.Contains(category));
            }
            return View("Indexuser", books.ToList());
        }
        public ActionResult chuong(int id)
        {
            var sach = truyen.Truyens.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }

            var chuongMoi = new Chuong
            {
                TruyenID = id.ToString() // Assuming TruyenID is a string in the Chuong model
            };

            return View(chuongMoi);
        }

        [HttpPost]
        public ActionResult chuong(Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                chuong.NgayDang = DateTime.Now;
                truyen.Chuongs.Add(chuong);
                truyen.SaveChanges();
                return RedirectToAction("Details", new { id = chuong.TruyenID });
            }

            return View(chuong);
        }
    }
}