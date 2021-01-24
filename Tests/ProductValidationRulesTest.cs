using FluentAssertions;
using FluentValidationKata.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using FluentValidation.Results;
using Xunit;
using static FluentValidationKata.Domain.Validation.ProductValidator;

namespace FluentValidationKata.Tests
{
    public class ProductValidationRulesTest
    {
        #region Invalid Objects Unit Tests

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_No_Reference_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, string.Format(MandatoryPropertyErrorMessage, nameof(product.Reference)));
        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_No_Gtin_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, string.Format(MandatoryPropertyErrorMessage, nameof(product.Gtin)));

        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_No_Language_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, string.Format(MandatoryPropertyErrorMessage, nameof(product.Language)));
        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_No_SellerId_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, string.Format(MandatoryPropertyErrorMessage, nameof(product.SellerId)));
        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_No_CategoryId_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, string.Format(MandatoryPropertyErrorMessage, nameof(product.CategoryId)));
        }

        [Theory]
        [InlineData("d")]
        [InlineData("US")]
        [InlineData("Fr")]
        [InlineData("France")]
        [InlineData("")]
        [Category("Invalid Product")]
        public void A_Product_With_Language_Not_ISO639Compliant_Should_Not_Be_Validated(string language)
        {
            //Arrange 
            var product = new Product { Language = language };
            var expectedErrorMessage = LanguageISO639ComplianceValidationErrorMessage;

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, LanguageISO639ComplianceValidationErrorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [Category("Invalid Product")]
        public void A_Product_With_A_Description_Empty_Or_Blank_Should_Not_Be_Validated(string description)
        {
            //Arrange 
            var product = new Product { Description = description };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, DescriptionValidationErrorMessage);
        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_Less_Than_Two_Pictures_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { Pictures = new List<string> { "4325252TRETT43534.jpg" } };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, PicturesValidationErrorMessage);
        }

        [Fact]
        [Category("Invalid Product")]
        public void A_Product_With_More_Than_5_Pictures_Should_Not_Be_Validated()
        {
            //Arrange 
            var product = new Product { Pictures = new List<string> { "2222.jpg", "2223.jpg", "2224.jpg", "2225.jpg", "2226.jpg", "2227.jpg" } };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, PicturesValidationErrorMessage);
        }

        [Theory]
        [InlineData("d")]
        [InlineData("de")]
        [InlineData("De")]
        [InlineData("DDD")]
        [InlineData("dede")]
        [InlineData("DEde")]
        [InlineData("ddddd")]
        [InlineData("retail")]
        [Category("Invalid Product")]
        public void A_Product_CategoryId_That_Is_Not_A_Sequence_Of_2_4_Or_6_Uppercase_Letters_Should_Not_Be_Valid(string category)
        {
            //Arrange 
            var product = new Product { CategoryId = category };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, CategoryIdValidationErrorMessage);
        }

        [Theory]
        [InlineData("111111")]
        [InlineData("ddd")]
        [InlineData("123456789")]
        [InlineData("1234567891")]
        [InlineData("123456789123456")]
        [Category("Invalid Product")]
        public void A_Product_Gtin_That_Is_Not_A_Gtin8_12_13_Or_14_Should_Not_Be_Valid(string gtin)
        {
            //Arrange 
            var product = new Product { Gtin = gtin };

            //Act
            var validationResult = product.Validate();

            //Assert
            ValidationShouldFailWithErrorMessage(validationResult, GtinFormatAllowanceValidationErrorMessage);
        }

        private void ValidationShouldFailWithErrorMessage(ValidationResult validationResult, string expectedErrorMessage)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().Contain(x => x.ErrorMessage == expectedErrorMessage);
        }

        #endregion Invalid Objects Unit Tests

        #region Valid Objects Unit Tests

        [Fact]
        public void A_Product_With_All_Mandatory_Property_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("DE")]
        [InlineData("COSM")]
        [InlineData("RETAIL")]
        [Category("Valid Product")]
        public void A_Product_With_All_Mandatory_Property_And_A_CategoryId_That_Is_A_Sequence_Of_2_4_Or_6_Uppercase_Letters_Is_Validated(string category)
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.CategoryId = category;

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_A_Gtin8_Format_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Gtin = "1111111";

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_A_Gtin12_Format_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Gtin = "12345678911";

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_A_Gtin13_Format_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Gtin = "123456789111";

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_A_Gtin14_Format_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Gtin = "1234567891111";

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_2_Pictures_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Pictures = new List<string> {"dede", "deee"};

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_5_Pictures_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Pictures = new List<string> {"dede", "deee", "deee", "deee", "deee"};

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        [Fact]
        public void A_Product_With_All_Mandatory_Property_And_A_Non_Blank_Description_Is_Validated()
        {
            //Arrange 
            var product = GenerateProductWithMandatoryProperties();
            product.Pictures = new List<string> {"dede", "deee", "deee", "deee", "deee"};

            //Act / Assert
            product.Validate().IsValid.Should().BeTrue();
        }

        private Product GenerateProductWithMandatoryProperties()
        {
            return new()
            {
                Reference = "ref",
                Gtin = "1111111",
                Language = "de",
                CategoryId = "DE",
                SellerId = "seller1"
            };
        }

        #endregion Valid Objects Unit Tests
    }
}
