using FluentValidation;
using KeyFactor.Carbone.Configuration.Localization;
using Microsoft.Extensions.Localization;
using System;
using Volo.Abp.DependencyInjection;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateUpdateProductDtoValidator : AbstractValidator<CreateUpdateProductDto>, ITransientDependency
    {
        public CreateUpdateProductDtoValidator(IStringLocalizer<ConfigurationResource> localizer)
        {
            var requiredMessage = localizer["Validation:Required"].Value;
            var maxStringLength = localizer["Validation:MaxStringLength"].Value;
            var rangeInclusive = localizer["Validation:RangeInclusive"].Value;
            var minLessThanMaxDate = localizer["Validation:MinLessThanMaxDate"].Value;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(requiredMessage)
                .MaximumLength(ProductConsts.MaxNameLength)
                .WithMessage(string.Format(maxStringLength, ProductConsts.MaxNameLength));

            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage(requiredMessage)
                .MaximumLength(ProductConsts.MaxNumberLength)
                .WithMessage(string.Format(maxStringLength, ProductConsts.MaxNumberLength));

            RuleFor(x => x.DecimalPlaces)
                .InclusiveBetween(0, 9)
                .WithMessage(string.Format(rangeInclusive, 0, 9));

            RuleFor(x => x.ValidFromDate)
                .Must(x => x.HasValue)
                .LessThanOrEqualTo(x => x.ValidToDate)
                .WithMessage(minLessThanMaxDate)
                .When(x => x.ValidToDate.HasValue);

            RuleFor(x => x.UnitId)
               .NotEqual(Guid.Empty)
               .WithMessage(requiredMessage);

        }
    }
}
