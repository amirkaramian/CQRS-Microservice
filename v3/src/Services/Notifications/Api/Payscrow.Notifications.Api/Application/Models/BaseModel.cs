using System;

namespace Payscrow.Notifications.Api.Application.Models
{
    public class BaseModel
    {
        private int? _requestedHashCode;
        private Guid _Id;

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

        public Guid TenantId { get; set; }
        public DateTime CreateUtc { get; set; }

        public BaseModel()
        {
            _Id = SequentialGuid.GenerateComb();

            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseModel))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            BaseModel item = (BaseModel)obj;

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

        public static bool operator ==(BaseModel left, BaseModel right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(BaseModel left, BaseModel right)
        {
            return !(left == right);
        }
    }
}