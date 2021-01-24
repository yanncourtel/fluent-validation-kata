using FluentValidation;
using FluentValidationKata.Extensions;
using System;
using System.Globalization;
using System.Linq;

namespace FluentValidationKata.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        /// <summary>
        /// - A mandatory property is neither null nor empty nor blank 
        /// - Properties Reference, Language, Gtin, SellerId and CategoryId are mandatory
        /// </summary>
        public const string MandatoryPropertyErrorMessage = 
            "Property {0} is mandatory (a mandatory property is neither null nor empty nor blank)";

        public const string LanguageISO639ComplianceValidationErrorMessage = 
            "The Language is [ISO 639-1](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes) compliant (2 chars string, lower case)";        
        
        public const string GtinFormatAllowanceValidationErrorMessage =
            "The Gtin allows GTIN-8, GTIN-12, GTIN-13 and GTIN-14 formats";        
        
        public const string DescriptionValidationErrorMessage =
            "If provided, the Description is neither empty nor blank";

        public const string PicturesValidationErrorMessage =
            "If provided, there must be between 2 and 5 pictures";        
        
        public const string CategoryIdValidationErrorMessage =
            "The CategoryId is a sequence of 2, 4 or 6 uppercase letters";

        public ProductValidator()
        {
            AddMandatoryFieldsRules();
            AddLanguageIso639ComplianceRule();
            AddGtinFormatRule();
            AddDescriptionCustomRule();
            AddPicturesCustomRule();
            AddCategoryIdCustomRule();
        }

        private void AddMandatoryFieldsRules()
        {
            RuleFor(x => x.Reference).NotNull().NotEmpty().WithGlobalMessage(string.Format(MandatoryPropertyErrorMessage, nameof(Product.Reference)));
            RuleFor(x => x.Language).NotNull().NotEmpty().WithGlobalMessage(string.Format(MandatoryPropertyErrorMessage, nameof(Product.Language)));
            RuleFor(x => x.Gtin).NotNull().NotEmpty().WithGlobalMessage(string.Format(MandatoryPropertyErrorMessage, nameof(Product.Gtin)));
            RuleFor(x => x.SellerId).NotNull().NotEmpty().WithGlobalMessage(string.Format(MandatoryPropertyErrorMessage, nameof(Product.SellerId)));
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithGlobalMessage(string.Format(MandatoryPropertyErrorMessage, nameof(Product.CategoryId)));
        }

        private void AddLanguageIso639ComplianceRule()
        {
            RuleFor(x => x.Language)
                .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length == 2 && x.ToString().All(char.IsLower))
                .WithMessage(LanguageISO639ComplianceValidationErrorMessage);
        }

        /// <summary>
        /// Add Gtin format validation rule : Format allowed => GTIN-8, GTIN-12, GTIN-13, GTIN-14
        /// </summary>
        private void AddGtinFormatRule()
        {
            RuleFor(x => x.Gtin)
                .Must(x => !string.IsNullOrWhiteSpace(x)
                           && (x.Length == 7 || x.Length == 11 || x.Length == 12 || x.Length == 13)
                           && double.TryParse(x, out _))
                .WithMessage(GtinFormatAllowanceValidationErrorMessage);
        }

        private void AddDescriptionCustomRule()
        {
            RuleFor(x => x.Description)
                .Must(x => x == null || !string.IsNullOrWhiteSpace(x))
                .WithMessage(DescriptionValidationErrorMessage);
        }

        private void AddPicturesCustomRule()
        {
            RuleFor(x => x.Pictures)
                .Must(x => x == null || (x.Count() >= 2 && x.Count() <= 5))
                .WithMessage(PicturesValidationErrorMessage);
        }

        private void AddCategoryIdCustomRule()
        {
            RuleFor(x => x.CategoryId)
                .Must(x => !string.IsNullOrWhiteSpace(x) 
                           && (x.Length == 2 || x.Length == 4 || x.Length == 6) 
                           && x.ToString().All(char.IsUpper))
                .WithMessage(CategoryIdValidationErrorMessage);
        }
    }
}
