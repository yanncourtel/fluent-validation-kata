using System.Collections.Generic;

namespace FluentValidationKata.Domain
{
    /// <summary>
    /// The validation product attributes
    /// </summary>
    public class ProductAttribute
    {
        /// <summary>
        /// Gets or sets the property reference.
        /// </summary>
        public string PropertyReference { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public IEnumerable<string> Values { get; set; }
    }
}
