using Common.Interface;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class AuditableEntity : TypedEntity, IValidableEntity
    {
        public AuditableEntity()
        {
            _validationResults = new Dictionary<string, string[]>();
        }

        public int CreatedById { get; internal set; }
        public string CreatedBy { get; internal set; }
        public DateTime CreatedOn { get; internal set; }


        public int? ModifiedById { get; internal set; }
        public string ModifiedBy { get; internal set; }
        public DateTime? ModifiedOn { get; internal set; }


        public bool Deleted { get; internal set; }
        public int? DeletedById { get; internal set; }
        public string DeletedBy { get; internal set; }
        public DateTime? DeletedOn { get; internal set; }


        protected Dictionary<string, string[]> _validationResults;
        public IReadOnlyDictionary<string, string[]> ValidationErros => _validationResults;

        public void MarkAsDeleted(int userId, string userName, IDateTime dateTime)
        {
            Deleted = true;
            DeletedById = userId;
            DeletedBy = userName;
            DeletedOn = dateTime.Now;
        }

        public void MarkAsUnDeleted(int userId, string userName, IDateTime dateTime)
        {
            Deleted = false;
            ModifiedById = userId;
            ModifiedBy = userName;
            ModifiedOn = dateTime.Now;
        }

        public void MarkAsChanged(int userId, string userName, IDateTime dateTime)
        {
            ModifiedById = userId;
            ModifiedBy = userName;
            ModifiedOn = dateTime.Now;
        }

        public void MarkAsCreated(int userId, string userName, IDateTime dateTime)
        {
            CreatedById = userId;
            CreatedBy = userName;
            CreatedOn = dateTime.Now;
            Deleted = false;
        }

        public abstract bool IsValid();
    }
}
