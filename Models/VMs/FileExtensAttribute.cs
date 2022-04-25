using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileExtensAttribute : ValidationAttribute
    {
        private string[] Extensions { get; set; }
        public FileExtensAttribute(string[] _extens)
        {
            Extensions = _extens;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string _errors = "";
                var list = value as IEnumerable<object>;

                foreach (IFormFile item in list)
                {
                    bool validateFile = false;
                    foreach (string extension in Extensions)
                    {
                        if (item.FileName.EndsWith(extension))
                        {
                            validateFile = true;
                            break;
                        }
                    }
                    if (!validateFile)
                        _errors = _errors + item.FileName + " contains Invalid Extension. ";
                }
                if (_errors == "")
                    return ValidationResult.Success;
                else
                    return new ValidationResult(_errors);
            }
            return new ValidationResult("Value is null");
        }
    }
}