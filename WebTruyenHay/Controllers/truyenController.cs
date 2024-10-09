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
        truyenhayEntities truyendata = new truyenhayEntities();
        // GET: truyen
        public ActionResult Index(string category)
        {

            if (category == null)
            {
                var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe);
                return View(productList);
            }
            else
            {
                var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe)
                .Where(x => x.theloai == category);
                return View(productList);
            }

        }
        public ActionResult Indexuser(string category)
        {

            if (category == null)
            {
                var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe);
                return View(productList);
            }
            else
            {
                var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe)
                .Where(x => x.theloai == category);
                return View(productList);
            }

        }
        public ActionResult Create()
        {
            Truyen truyen1 = new Truyen();
            return View(truyen1);
        }
        [HttpPost]

        public ActionResult Create(Truyen truyen1)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    if (truyen1.UploadImage != null && truyen1.UploadImage.ContentLength > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(truyen1.UploadImage.FileName);
                        string extent = Path.GetExtension(truyen1.UploadImage.FileName);
                        filename = filename + extent;
                        truyen1.imagetruyen = "~/Content/images/" + filename;
                        truyen1.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                   
                    truyen1.IDtruyen = Guid.NewGuid().ToString("N").Substring(0, 8);
                    truyen1.NgayTao = DateTime.Now;
                    truyendata.Truyens.Add(truyen1);
                    truyendata.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message; 
            }
            return View(truyen1);
        }
        public ActionResult Edit(String id)
        {
            var product = truyendata.Truyens.Find(id);
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
                    var existingProduct = truyendata.Truyens.Find(sach.IDtruyen);
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
                    truyendata.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(sach);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Detailsuser(String id)
        {
            var product = truyendata.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Details(String id)
        {
            var product = truyendata.Truyens.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Delete(String id)
        {
            var sach = truyendata.Truyens.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            // Xóa sản phẩm
            truyendata.Truyens.Remove(sach);
            truyendata.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString)
        {
            var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe);

            if (!string.IsNullOrEmpty(searchString))
            {
                productList = (IOrderedQueryable<Truyen>)productList.Where(x => x.TieuDe.Contains(searchString));
            }

            return View("Index", productList.ToList());
        }
        public ActionResult Searchuser(string searchString)
        {
            var productList = truyendata.Truyens.OrderByDescending(x => x.TieuDe);

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
            var books = truyendata.Truyens.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.TieuDe.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.TacGia.Contains(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.theloai.Contains(category));
            }
            return View("Index", books.ToList());
        }
        public ActionResult AdvancedSearchuser(string title, string author, string category)
        {
            var books = truyendata.Truyens.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.TieuDe.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.TacGia.Contains(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.theloai.Contains(category));
            }
            return View("Indexuser", books.ToList());
        }
        public ActionResult chuong(int id)
        {
            var sach = truyendata.Truyens.Find(id);
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
                if (chuong.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(chuong.UploadImage.FileName);
                    string extent = Path.GetExtension(chuong.UploadImage.FileName);
                    filename = filename + extent;
                    chuong.imagechuong = "~/Content/images/" + filename;
                    chuong.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    chuong.NgayDang = DateTime.Now;
                }
                chuong.NgayDang = DateTime.Now;
                truyendata.Chuongs.Add(chuong);
                truyendata.SaveChanges();
                return RedirectToAction("Details", new { id = chuong.TruyenID });
            }

            return View(chuong);
        }
        public ActionResult nextchap(int thutu)
        {
            var currentChapter = truyendata.Chuongs.Find(thutu);
            if (currentChapter == null)
            {
                return HttpNotFound();
            }

            var nextChapter = truyendata.Chuongs
                .Where(c => c.SoThuTu > thutu)
                .OrderBy(c => c.SoThuTu)
                .FirstOrDefault();

            if (nextChapter == null)
            {
                return RedirectToAction("LastChapter");
            }

            return View("ChapterView", nextChapter);
        }
        public ActionResult returnchap(int thutu)
        {
            var currentChapter = truyendata.Chuongs.Find(thutu);
            if (currentChapter == null)
            {
                return HttpNotFound();
            }

            var returnChapter = truyendata.Chuongs
                .Where(c => c.SoThuTu < thutu)
                .OrderBy(c => c.SoThuTu)
                .FirstOrDefault();

            if (returnChapter == null)
            {
                return RedirectToAction("LastChapter");
            }

            return View("ChapterView", returnChapter);
        }
        
    }
}