using System;

namespace Psychology_API.Dtos.DocumentDto
{
    [Serializable]
    public class DocumentForInterdepartRequestDto
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public byte[] Body { get; set; }
    }
}