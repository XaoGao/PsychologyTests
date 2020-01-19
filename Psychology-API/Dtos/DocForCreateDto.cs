using System;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Dtos
{
    public class DocForCreateDto
    {
        public string DocName { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public int DocType { get; set; }
        public int PatientId { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime DateUpload { get; set; }
        public string Extension { get; set; }
        public IFormFile FileBody { get; set; }
        public DocForCreateDto()
        {
            DateUpload = DateTime.Now;
            Extension = GetExtension();
        }
        private string GetExtension()
        {
            var exteinsion = DocName.Substring(DocName.LastIndexOf('.') + 1);

            return exteinsion;
        }
    }
}