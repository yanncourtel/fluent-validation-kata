using FluentValidation;
using FluentValidation.Internal;

namespace FluentValidationKata.Extensions
{
    //class GlobalValidationExtensions
    public static class MyExtentions
    {
        public static IRuleBuilderOptions<T, TProperty> WithGlobalMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            foreach (var item in (rule as RuleBuilder<T, TProperty>).Rule.Validators)
            {
                item.Options.SetErrorMessage(errorMessage);
            }

            return rule;
        }
    }
}
