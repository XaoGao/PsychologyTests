using System;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.DocumentDto
{
    public class DocumentForReturnDto
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime DateUpload { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumenType { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}