using System;

namespace Psychology_API.Dtos.InterdepartDto
{
    [Serializable]
    public class InterdepartRequestDto
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public int InterdepartStatusId { get; set; }
    }
}