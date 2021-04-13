using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Data
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            return (value as IFormFile).Length <= _maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("O tamanho do arquivo nao pode exceder o tamanho de 80MB.");
        }
    }
}
