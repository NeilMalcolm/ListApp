using System;

namespace ListApp.Models
{
    public abstract class BaseRecordableRecord
    {
        public Guid Id { get; set; }

        public DateTime? DateCreated { get; set; } = null;

        public DateTime? DateModified { get; set; }

        public BaseRecordableRecord()
        {
            Id = Guid.NewGuid();
        }
    }
}
