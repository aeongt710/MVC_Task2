using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileExtensAttribute : ValidationAttribute
    {
        private string[] Extensions { get; set; }
        public FileExtensAttribute(params string[] _extens)
        {
            Extensions = _extens;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string _errors = "";
                if (value is IEnumerable)
                {
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
                else if (value is IFormFile)
                {
                    var item = value as IFormFile;
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
                        _errors =  item.FileName + " contains Invalid Extension. ";

                    if (_errors == "")
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(_errors);
                }

            }

            return new ValidationResult("Value is null");
        }
        public FileExtensAttribute()
        {

        }
    }
}