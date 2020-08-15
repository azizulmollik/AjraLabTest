using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Core.Models;

namespace WebApp.Core.CustomDataValidation
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionAttribute : ValidationAttribute, IClientValidatable
    {
        private List<string> AllowedExtensions { get; set; }
        private int AllowedContentLength { get; set; }


        public FileExtensionAttribute(string fileExtensions, int contentLength)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            AllowedContentLength = contentLength * 1024 * 1024;//MB
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            bool isValid = true;

            if (file != null)
            {
                var fileName = file.FileName;
                isValid = AllowedExtensions.Any(y => fileName.EndsWith(y));

                if (isValid == false)
                {
                    ErrorMessage = string.Format("Only {0} files ext are allowed.", string.Join(", ", AllowedExtensions));
                }
                //Check Size
                if (file.ContentLength > AllowedContentLength)
                {
                    isValid = false;
                    ErrorMessage = "File too large, maximum allowed upto " + (AllowedContentLength / 2048).ToString() + "Mb.";
                }
            }

            return isValid;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "fileextension",
                ErrorMessage = ErrorMessage
            };
            rule.ValidationParameters.Add("fileextension", string.Join(",", AllowedExtensions));
            yield return rule;
        }
    }
}

