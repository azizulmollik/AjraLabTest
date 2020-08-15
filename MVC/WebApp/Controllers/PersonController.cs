using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Core.BLL;
using WebApp.Core.Models;
using WebApp.Core.Utlities;
using WebApp.Core.ViewModels;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        private PersonManager personManager = new PersonManager();
        private CommonManager commonManager = new CommonManager();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //Dropdown Select List            
            ViewBag.Country = new SelectList(commonManager.GetAllCountry(), "Value", "Text", "--Select--");
            ViewBag.City = new SelectList(commonManager.GetAllCity(0), "Value", "Text", "--Select--");            
        }

        public ActionResult Index(int id,int currentPage=1)
        {
            var dataVM = new PersonViewModel();
            dataVM.SkillList = commonManager.GetAllSkill();
            dataVM.DataList = personManager.GetAll();
            // for edit
            if (id > 0)
            {                
                dataVM.Person = personManager.Get(id);
                if (dataVM.Person != null)
                {
                    ViewBag.City = new SelectList(commonManager.GetAllCity(dataVM.Person.CountryId != 0 ? (int)dataVM.Person.CountryId : 0), "Value", "Text", dataVM.Person.CityId);

                    string[] sList = dataVM.Person.LanguageSkills.Split(new string[] { ", " }, StringSplitOptions.None);
                    foreach (string s in sList)
                    {
                        foreach(SkillsViewModel sv in dataVM.SkillList)
                        {
                            if (sv.Skill == s)
                                sv.Selected = true;
                        }
                    }
                }
            }
            //paging
            //var currentPage = 1;
            var pageSize = 10;
            dataVM.DataList = personManager.GetAllWithPagination(currentPage, pageSize, "","","");
            double pageCount = (double)((decimal)dataVM.DataList[0].TotalRecords / Convert.ToDecimal(pageSize));
            dataVM.TotalPage = (int)Math.Ceiling(pageCount);
            dataVM.CurrentPage = currentPage;

            dataVM.Message = TempData["tempMsg"] != null ? (Message)TempData["tempMsg"] : dataVM.Message;
            return View(dataVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PersonViewModel dataVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = dataVM.Person;
                    //Skill
                    var s = "";
                    foreach(SkillsViewModel ss in dataVM.SkillList)
                    {
                        if(ss.Selected==true)
                        s = s + ss.Skill + ", ";
                    }
                    s=s.Remove(s.Length - 2, 2);
                    data.LanguageSkills = s;
                    //File
                    byte[] content = null;
                    if (data.AttachFile != null && data.AttachFile.ContentLength > 0)
                    {
                        using (BinaryReader br = new BinaryReader(data.AttachFile.InputStream))
                        {
                            content = br.ReadBytes(data.AttachFile.ContentLength);
                        }
                    }
                    data.Attachment = (content != null) ? content : data.Attachment;
                    data.FileName = (content != null) ? data.AttachFile.FileName : data.FileName;
                    data.FileContentType = (content != null) ? data.AttachFile.ContentType : data.FileContentType;

                    var message = personManager.CreateOrUpdate(data);
                    TempData["tempMsg"] = message;
                }
                catch (Exception ex)
                {
                    var message = new Message();
                    message.MessageType = MessageTypes.Error;
                    message.CurrentMessage = "Error Occured To Save Data.";
                    //TempData["tempMsg"] = message;
                    dataVM.DataList = personManager.GetAll();
                    dataVM.Message = message;
                    return View("Index", dataVM);
                }
            }
            return RedirectToAction("Index", new { id = 0 });
        }

        public ActionResult Preview(int id)
        {            
            try
            {
                var dataVM = new PersonViewModel();                
                dataVM.Person = personManager.Get(id);
                return View(dataVM);
            }
            catch (Exception ex)
            {
                var message = new Message();
                message.MessageType = MessageTypes.Error;
                message.CurrentMessage = "Error Occured To Preview Data.";
                TempData["tempMsg"] = message;
            }
            return RedirectToAction("Index", new { id = 0 });
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var message = personManager.Delete(id);
                TempData["tempMsg"] = message;
            }
            catch (Exception ex)
            {
                var message = new Message();
                message.MessageType = MessageTypes.Error;
                message.CurrentMessage = "Error Occured To Delete Data.";
                TempData["tempMsg"] = message;
            }
            return RedirectToAction("Index", new { id = 0 });
        }

        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            var dataVM = new PersonViewModel();
            dataVM.Person = personManager.Get(id);            
            return File(dataVM.Person.Attachment, "application/force-download", dataVM.Person.FileName);
        }

        public JsonResult GetAllCityByCountryId(int countryId)
        {
            return Json(new SelectList(commonManager.GetAllCity(countryId), "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        //[HttpPost]
        //public JsonResult GetDetails()
        //{
        //    var start = (Convert.ToInt32(Request["start"])) == 0 ? 1 : (Convert.ToInt32(Request["start"]));
        //    var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
        //    var searchvalue = Request["search[value]"] ?? "";
        //    var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
        //    var SortColumn = "";
        //    var SortOrder = "";
        //    var sortDirection = Request["order[0][dir]"] ?? "asc";
        //    var recordsTotal = 0;           

        //    try
        //    {
        //        switch (sortcoloumnIndex)
        //        {
        //            case 0:
        //                SortColumn = "Name";
        //                break;
        //            case 1:
        //                SortColumn = "Country";
        //                break;
        //            case 2:
        //                SortColumn = "City";
        //                break;
        //            case 3:
        //                SortColumn = "LanguageSkills";
        //                break;
        //            case 4:
        //                SortColumn = "DOB";
        //                break;
        //            default:
        //                SortColumn = "Id";
        //                break;
        //        }
        //        if (sortDirection == "asc")
        //            SortOrder = "asc";
        //        else
        //            SortOrder = "desc";                
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    var dataVM = new PersonViewModel();
        //    dataVM.DataList = personManager.GetAllWithPagination(start, Length, searchvalue, SortColumn, SortOrder);
        //    recordsTotal = dataVM.DataList.Count > 0 ? (int)dataVM.DataList[0].TotalRecords : 0;
        //    return Json(new { data = dataVM.DataList, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);
        //}
    }
}