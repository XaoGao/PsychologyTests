using System;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Dtos.DocumentDto
{
    public class DocumentForCreateDto
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public int PatientId { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime DateUpload { get; set; }
        public IFormFile File { get; set; }
        public DocumentForCreateDto()
        {
            DateUpload = DateTime.Now;
        }
        
    }
}