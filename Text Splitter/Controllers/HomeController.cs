using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO.Compression;

namespace Text_Splitter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //tanpa header
        public ActionResult UploadFiles(HttpPostedFileBase files, int sizes)
        {
           
            var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
            // empty the temp folder
            Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));
            
            int size = sizes;
            //int index = 0;

            string s = string.Empty;
            string pathSave = "~/Content/Files/DataSplit/";
            //files.SaveAs(path);
            string _FileName = Path.GetFileName(files.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/Files/UploadanAwal"), _FileName);
            files.SaveAs(path);

            var lines = System.IO.File.ReadAllLines(path);
            int part = 1;
            int index = 0;
            List<string> line = new List<string>();
            bool check = false;         
         
            foreach (var item in lines)
            {
                var tes = item;
                
                if (line.Count < sizes)
                {
                    line.Add(item);
                }
                else
                {
                    // create file jika line sudah bersisi sesuai size
                    System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line );

                    var liness = System.IO.File.ReadAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt");
                    // kosongkan list 
                    var lengt = liness.Count();
                    line = new List<string>();

                    // tambahkan current line ke list
                    line.Add(item);
                    part++;
                    check = true;
                }
                index++;
            }
            //if (line.Count > 0)
            //{
            //    System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
            //}           
            //return Json(true, JsonRequestBehavior.AllowGet);

            if (check == true)
            {
                // untuk handle part terakhir yang jumlahnya tidak sampai ke jumlah yang ditentukan
                if (line.Count > 0)
                {
                    System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
                }
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (index == sizes)
                {
                    if (line.Count > 0)
                    {
                        System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }

            }
        }

        //dengan header
        public ActionResult UploadFiles2(HttpPostedFileBase files, int sizes)
        {

            var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
            // empty the temp folder
            Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));


            int size = sizes;
            //int index = 0;

            string s = string.Empty;
            string pathSave = "~/Content/Files/DataSplit/";
            //files.SaveAs(path);
            string _FileName = Path.GetFileName(files.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/Files/UploadanAwal"), _FileName);
            files.SaveAs(path);

            var lines = System.IO.File.ReadAllLines(path);
            int part = 1;
            int index = 0;
            List<string> line = new List<string>();
            bool check = false;
            bool header = true;
            var tes = "";

            foreach (var item in lines)
            {
                if (header == true)
                {

                    header = false;
                    line.Add(item);
                    tes = item;
                    //line.Add(item);    untuk hasil tanpa header sama sekali line dan tes dihapus atau comment
                    //tes = item;

                }
                else
                {

                    if (line.Count < sizes)


                    //if (line.Count < sizes+1)  untuk header diluar hitungan
                        {
                        line.Add(item);
                    }
                    else
                    {
                        // create file jika line sudah bersisi sesuai size
                        System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);

                        // kosongkan list 
                        line = new List<string>();

                        // tambahkan current line ke list
                        line.Add(tes);
                        line.Add(item);

                        part++;
                        check = true;
                    }
                }
                index++;

            }
            //if (line.Count > 0)
            //{
            //    System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
            //}


            //return Json(true, JsonRequestBehavior.AllowGet);

            if (check == true)
            {



                // untuk handle part terakhir yang jumlahnya tidak sampai ke jumlah yang ditentukan
                if (line.Count > 0)
                {
                    System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
                }


                return Json(check, JsonRequestBehavior.AllowGet);

            }
            else
            {
                if (index == sizes)
                {
                    if (line.Count > 0)
                    {
                        System.IO.File.WriteAllLines(Server.MapPath(pathSave) + "part-" + part + ".txt", line);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }

            }




        }
       
        //tanpa header
        public FileResult DownloadFile()
        {
            var filePaths = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
            var getDir = Directory.GetFiles(filePaths, "*.txt");
            var pathList = getDir.Where(p => p != "").ToList();
            var path = @"" + filePaths[0];
            if (pathList.Count == 1)
            {
                var id = Session.SessionID;

                var archive = Server.MapPath("~/Split No Header.zip");
                //var temp = Server.MapPath("~/DataSplit");
                var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
                var temp = Directory.GetFiles(filePathss, "*.txt").ToList();

                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                //Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));

                //// copy the selected files to the temp folder
                //pathList.ForEach(f => System.IO.File.Copy(f, Path.Combine(filePathss, Path.GetFileName(f))));

                // create a new archive

                ZipFile.CreateFromDirectory(filePathss, archive);

                return File(archive, "application/zip", "Split No Header.zip");
            }
            if (pathList.Count > 1)
            {
                var id = Session.SessionID;

                var archive = Server.MapPath("~/Split No Header.zip");
                //var temp = Server.MapPath("~/DataSplit");
                var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
                var temp = Directory.GetFiles(filePathss, "*.txt").ToList();

                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                //Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));

                //// copy the selected files to the temp folder
                //pathList.ForEach(f => System.IO.File.Copy(f, Path.Combine(filePathss, Path.GetFileName(f))));

                // create a new archive

                ZipFile.CreateFromDirectory(filePathss, archive);

                return File(archive, "application/zip", "Split No Header.zip");
                //return File(archive, MimeMapping.GetMimeMapping(archive));
            }

            return null;
        }

        //dengan header
        public FileResult DownloadFile2()
        {
            var filePaths = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
            var getDir = Directory.GetFiles(filePaths, "*.txt");
            var pathList = getDir.Where(p => p != "").ToList();
            var path = @"" + filePaths[0];
            if (pathList.Count == 1)
            {
                var id = Session.SessionID;

                var archive = Server.MapPath("~/Split With Header.zip");
                //var temp = Server.MapPath("~/DataSplit");
                var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
                var temp = Directory.GetFiles(filePathss, "*.txt").ToList();

                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                //Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));

                //// copy the selected files to the temp folder
                //pathList.ForEach(f => System.IO.File.Copy(f, Path.Combine(filePathss, Path.GetFileName(f))));

                // create a new archive

                ZipFile.CreateFromDirectory(filePathss, archive);

                return File(archive, "application/zip", "Split With Header.zip");
            }
            if (pathList.Count > 1)
            {
                var id = Session.SessionID;

                var archive = Server.MapPath("~/Split With Header.zip");
                //var temp = Server.MapPath("~/DataSplit");
                var filePathss = Path.Combine(Server.MapPath("~/Content/Files/DataSplit/"));
                var temp = Directory.GetFiles(filePathss, "*.txt").ToList();

                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                //Directory.EnumerateFiles(filePathss).ToList().ForEach(f => System.IO.File.Delete(f));

                //// copy the selected files to the temp folder
                //pathList.ForEach(f => System.IO.File.Copy(f, Path.Combine(filePathss, Path.GetFileName(f))));

                // create a new archive

                ZipFile.CreateFromDirectory(filePathss, archive);

                return File(archive, "application/zip", "Split With Header.zip");
                //return File(archive, MimeMapping.GetMimeMapping(archive));
            }

            return null;
        }
        
    }
}