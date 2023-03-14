using System;

namespace Payscrow.PaymentInvite.Domain.SeedWork
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        Guid _Id;
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public Guid AccountId { get; set; }
        public Guid TenantId { get; set; }

        public DateTime CreateUtc { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }
        public Guid? UpdateUserId { get; set; }


        public Entity()
        {
            _Id = SequentialGuid.GenerateComb();

            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }

        //private List<INotification> _domainEvents;
        //public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        //public void AddDomainEvent(INotification eventItem)
        //{
        //    _domainEvents = _domainEvents ?? new List<INotification>();
        //    _domainEvents.Add(eventItem);
        //}

        //public void RemoveDomainEvent(INotification eventItem)
        //{
        //    _domainEvents?.Remove(eventItem);
        //}

        //public void ClearDomainEvents()
        //{
        //    _domainEvents?.Clear();
        //}

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
