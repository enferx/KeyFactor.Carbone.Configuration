using FluentValidation;
using KeyFactor.Carbone.Configuration.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateProductPropertyValidator : AbstractValidator<CreateProductPropertyDto>, ITransientDependency
    {
        public CreateProductPropertyValidator(IStringLocalizer<ConfigurationResource> localizer)
        {
            var requiredMessage = localizer["Validation:Required"].Value;
            var lessThanMessage = localizer["Validation:MinLessThanMaxValue"].Value;
            var defaultBetweenLimits = localizer["Validation:DefaultBetweenLimits"].Value;

            When(x => x.DataType == DataType.Decimal, () =>
            {
                RuleFor(x => x.MinDecimalValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MaxDecimalValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MinDecimalValue)
                    .LessThan(x => x.MaxDecimalValue)
                    .WithMessage(lessThanMessage);
                RuleFor(x => x.DefaultValueDecimal)
                    .GreaterThanOrEqualTo(x => x.MinDecimalValue)
                    .LessThanOrEqualTo(x => x.MaxDecimalValue)
                    .WithMessage(defaultBetweenLimits);
                RuleFor(x => x.DefaultValueDecimal)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage)
                    .When(x => x.IsRequired);
            });

            When(x => x.DataType == DataType.Double, () =>
            {
                RuleFor(x => x.MinDoubleValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MaxDoubleValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MinDoubleValue)
                    .LessThan(x => x.MaxDoubleValue)
                    .WithMessage(lessThanMessage);
                RuleFor(x => x.DefaultValueDouble)
                    .GreaterThanOrEqualTo(x => x.MinDoubleValue)
                    .LessThanOrEqualTo(x => x.MaxDoubleValue)
                    .WithMessage(defaultBetweenLimits);
                RuleFor(x => x.DefaultValueDouble)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage)
                    .When(x => x.IsRequired);
            });

            When(x => x.DataType == DataType.Integer, () =>
            {
                RuleFor(x => x.MinIntegerValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MaxIntegerValue)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.MinIntegerValue)
                    .LessThan(x => x.MaxIntegerValue)
                    .WithMessage(lessThanMessage);
                RuleFor(x => x.DefaultValueInteger)
                    .GreaterThanOrEqualTo(x => x.MinIntegerValue)
                    .LessThanOrEqualTo(x => x.MaxIntegerValue)
                    .WithMessage(defaultBetweenLimits);
                RuleFor(x => x.DefaultValueInteger)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage)
                    .When(x => x.IsRequired);
            });

            When(x => x.DataType == DataType.String, () =>
            {
                RuleFor(x => x.MaxLengthString)
                    .Must(x => x.HasValue)
                    .WithMessage(requiredMessage);
                RuleFor(x => x.DefaultValueString)
                    .Must(x => string.IsNullOrWhiteSpace(x))
                    .WithMessage(requiredMessage)
                    .When(x => x.IsRequired);
            });
        }
    }
}
