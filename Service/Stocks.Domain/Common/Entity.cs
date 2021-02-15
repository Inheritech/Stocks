namespace Stocks.Domain.Common {

    /// <summary>
    /// Represents an object which identity is based on an ID
    /// </summary>
    public abstract class Entity {
        public virtual int Id { get; protected set; }
        public bool HasIdentity() => this.Id != default;

        public override bool Equals(object obj) {
            if (obj is null)
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            if (obj is Entity otherEntity)
                // If our own Id is 0 it means we don't have an identity yet
                return this.HasIdentity() && this.Id == otherEntity.Id;
            else
                return false; // Not an entity
        }

        public override int GetHashCode() {
            if (this.HasIdentity()) {
                return this.Id.GetHashCode() ^ 31;
            }
            return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right) {
            if (object.Equals(left, null))
                return object.Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right) {
            return !(left == right);
        }
    }
}
