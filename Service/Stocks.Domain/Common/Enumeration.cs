using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Stocks.Domain.Common {

    /// <summary>
    /// Domain base class for keeping an observable record on possible values of an enumeration
    /// that other domain objects may use.
    /// </summary>
    public abstract class Enumeration : IComparable {
        public int Id { get; }
        public string Name { get; }

        protected Enumeration(int id, string name) {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Retrieve all the possible values for an enumeration.
        /// This assumes that all possible values are defined as static fields on the type.
        /// </summary>
        /// <remarks>
        /// This method uses reflection so it should be avoided when possible.
        /// </remarks>
        /// <typeparam name="TEnumeration">Enumeration type to list.</typeparam>
        /// <returns>A collection of all possible enumeration values for the provided generic type.</returns>
        public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration {
            // Grab all static fields declared on the specific generic type
            var fields = typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }

        /// <summary>
        /// Obtain an enumeration value based on its Id.
        /// </summary>
        /// <remarks>
        /// This uses the functionality of the <see cref="GetAll{TEnumeration}"/> method which implies specific
        /// constraints and a possible performance impact due to reflection.
        /// </remarks>
        /// <typeparam name="TEnumeration">Type of the enumeration.</typeparam>
        /// <param name="id">Id of the enumeration value to retrieve.</param>
        /// <exception cref="InvalidOperationException">Thrown when the id cannot be matched with one of the available values.</exception>
        /// <returns>A matching enumeration value.</returns>
        public static TEnumeration FromValue<TEnumeration>(int id) where TEnumeration : Enumeration {
            var matchingItem = GetAll<TEnumeration>().FirstOrDefault(e => e.Id == id);

            if (matchingItem == null)
                throw new InvalidOperationException($"There is no available enumeration value with an Id of '{id}'");

            return matchingItem;
        }

        /// <summary>
        /// Compare to another enumeration value based on its Id.
        /// </summary>
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        public override bool Equals(object obj) {
            if (!(obj is Enumeration otherValue))
                return false;

            if (GetType() != obj.GetType())
                return false;

            return Id.Equals(otherValue.Id);
        }

        public static bool operator ==(Enumeration left, Enumeration right) {
            if (object.Equals(left, null))
                return object.Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(Enumeration left, Enumeration right) {
            return !(left == right);
        }

        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => Name;
    }
}
