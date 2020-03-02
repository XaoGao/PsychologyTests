using System;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.DocumentDto
{
    public class DocumentForReturnListDto
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime DateUpload { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public int PatientId { get; set; }
        public int InterdepartRequestId { get; set; }
        public int InterdepartStatusId { get; set; }
    }
}