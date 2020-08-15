using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using WebApp.Core.CustomDataValidation;

namespace WebApp.Core.Models
{
    public class Person
    {
        public int? Id { get; set; }
        [Required,StringLength(50)]
        public string Name { get; set; }
        [Required,Display(Name ="Country")]
        public int CountryId { get; set; }
        [Required, Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "Language Skills")]
        public string LanguageSkills { get; set; }
        [Required, Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Display(Name = "Resume Upload")]
        public byte[] Attachment { get; set; }
        [FileExtension("pdf,doc", 10)]
        public HttpPostedFileBase AttachFile { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
    }

    public class PersonDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [Display(Name = "Language Skills")]
        public string LanguageSkills { get; set; }
        [Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DOB { get; set; }
        public byte[] Attachment { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
        public int? TotalRecords { get; set; }
        public int? rowNum { get; set; }        
    }
}
