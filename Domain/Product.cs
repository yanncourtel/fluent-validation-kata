using FluentValidation;
using FluentValidation.Results;
using FluentValidationKata.Domain.Validation;
using System;
using System.Collections.Generic;

namespace FluentValidationKata.Domain
{
    /// <summary>
    /// The product.
    /// </summary>
    public class Product : ICanValidate
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the ean.
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Gets or sets the seller reference.
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the pictures.
        /// </summary>
        public IEnumerable<string> Pictures { get; set; }

        /// <summary>
        /// Gets or sets the category reference.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the brand reference.
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets the product attributes.
        /// </summary>
        public IEnumerable<ProductAttribute> Attributes { get; set; }

        public ValidationResult Validate()
        {
            return new ProductValidator().Validate(this);
        }
    }
}
