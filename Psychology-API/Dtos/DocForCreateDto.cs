using System;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Dtos
{
    public class DocForCreateDto
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public int PatientId { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime DateUpload { get; set; }
        public IFormFile File { get; set; }
        public DocForCreateDto()
        {
            DateUpload = DateTime.Now;
        }
        
    }
}