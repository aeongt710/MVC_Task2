using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC_Task2.Models.VMs
{

    public class MaxFilesAttribute : ValidationAttribute
    {
        private int Count { get; set; }

        public MaxFilesAttribute(int _count)
        {
            Count = _count;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value!=null)
            {
                var list = value as IEnumerable<object>;

                if (list.Count() != Count)
                {
                    return new ValidationResult("Files are not equal to "+Count);
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(this.FormatErrorMessage("Value is null"));
        }
    }
}