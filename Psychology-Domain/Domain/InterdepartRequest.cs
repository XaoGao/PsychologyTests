using System;

namespace Psychology_Domain.Domain
{
    public class InterdepartRequest
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public DateTime Create { get; set; }
        public DateTime Request { get; set; }
        public DateTime Response { get; set; }
        public int InterdepartStatusId { get; set; }
        public InterdepartStatus InterdepartStatus { get; set; }
        public InterdepartRequest()
        {
            Create = DateTime.Now;
            InterdepartStatusId = 1;
        }
    }
}