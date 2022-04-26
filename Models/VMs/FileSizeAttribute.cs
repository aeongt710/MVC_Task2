using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task2.Models.VMs
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private int Size { get; set; }
        public FileSizeAttribute(int _size)
        {
            Size = _size;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string _errors = "";
                if(value is IEnumerable)
                {
                    var list = value as IEnumerable<object>;

                    foreach (IFormFile item in list)
                    {

                        if ((item.Length / 1024) > Size * 1024)
                        {
                            _errors = _errors + item.FileName + " size Exceed from " + Size + "MB. ";
                        }
                    }
                    if (_errors == "")
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(_errors);
                }
                else if(value is IFormFile)
                {
                    var item = value as IFormFile;
                    if ((item.Length / 1024) > Size * 1024)
                    {
                        _errors =  item.FileName + " size Exceed from " + Size + "MB. ";
                    }
                    if (_errors == "")
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(_errors);
                }

            }
            return new ValidationResult("Value is null");
        }
    }
}