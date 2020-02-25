using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Dtos.DocumentDto
{
    /// <summary>
    /// Класс по созданию документа.
    /// </summary>
    public class DocumentForCreateDto
    {
        public string Series { get; set; }
        public string Number { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор пациента.")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор типа документа.")]
        public int DocumentTypeId { get; set; }
        public DateTime DateUpload { get; set; }
        [Required(ErrorMessage = "Обязательно укажите сам файл.")]
        public IFormFile File { get; set; }
        public DocumentForCreateDto()
        {
            DateUpload = DateTime.Now;
        }
        
    }
}