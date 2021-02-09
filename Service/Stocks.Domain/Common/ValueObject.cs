using System.Collections.Generic;
using System.Linq;

namespace Stocks.Domain.Common {
    /// <summary>
    /// Represents an object which identity is based off the properties it carries.
    /// </summary>
    public abstract class ValueObject {
        /// <summary>
        /// Obtain all values that uniquely represent this value object.
        /// </summary>
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj) {
            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            ValueObject otherValueObject = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = otherValueObject.GetAtomicValues().GetEnumerator();
            // Until one or both objects run out of properties
            while (thisValues.MoveNext() && otherValues.MoveNext()) {
                if (thisValues.Current is null || otherValues.Current is null)
                    return false;
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                    return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode() {
            // Mix the hash codes of all properties
            return GetAtomicValues()
                .Select(v => v != null ? v.GetHashCode() : 0)
                .Aggregate((current, previous) => current ^ previous);
        }

        public static bool operator ==(ValueObject left, ValueObject right) {
            if (object.Equals(left, null))
                return object.Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right) {
            return !(left == right);
        }
    }
}
