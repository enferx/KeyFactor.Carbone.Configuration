using FluentValidation;
using KeyFactor.Carbone.Configuration.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateProductPropertyValidator : AbstractValidator<CreateProductPropertyDto>, ITransientDependency
    {
        private readonly IStringLocalizer<ConfigurationResource> _localizer;

        public CreateProductPropertyValidator(IStringLocalizer<ConfigurationResource> localizer)
        {
            _localizer = localizer;
            When(x => x.DataType == DataType.Decimal, () =>
            {
                RuleFor(x => x.MinDecimalValue).Must(x => x.HasValue);
                RuleFor(x => x.MaxDecimalValue).Must(x => x.HasValue);
                RuleFor(x => x.MinDecimalValue).LessThan(x => x.MaxDecimalValue)
                    .WithMessage("The minimun value can not be greater than the maximum value.");
                RuleFor(x => x.DefaultValueDecimal).GreaterThanOrEqualTo(x => x.MinDecimalValue)
                    .LessThanOrEqualTo(x => x.MaxDecimalValue)
                    .WithMessage("The default value must be between the minimum and maximun value");
                When(x => x.IsRequired, () =>
                {
                    RuleFor(x => x.DefaultValueDecimal).Null().WithMessage("Required");
                });
            });

            When(x => x.DataType == DataType.Double, () =>
            {
                RuleFor(x => x.MinDoubleValue).Must(x => x.HasValue).WithMessage("Required");
                RuleFor(x => x.MaxDoubleValue).Must(x => x.HasValue).WithMessage("Required");
                RuleFor(x => x.MinDoubleValue).LessThan(x => x.MaxDoubleValue)
                    .WithMessage("The minimun value can not be greater than the maximum value.");
                RuleFor(x => x.DefaultValueDouble).GreaterThanOrEqualTo(x => x.MinDoubleValue)
                    .LessThanOrEqualTo(x => x.MaxDoubleValue)
                    .WithMessage("The default value must be between the minimum and maximun value");
                When(x => x.IsRequired, () =>
                {
                    RuleFor(x => x.DefaultValueDouble).Null().WithMessage("Required");
                });
            });

            When(x => x.DataType == DataType.Integer, () =>
            {
                RuleFor(x => x.MinIntegerValue).Must(x => x.HasValue).WithMessage("Required");
                RuleFor(x => x.MaxIntegerValue).Must(x => x.HasValue).WithMessage("Required");
                RuleFor(x => x.MinIntegerValue).LessThan(x => x.MaxIntegerValue)
                    .WithMessage("The minimun value can not be greater than the maximum value.");
                RuleFor(x => x.DefaultValueInteger).GreaterThanOrEqualTo(x => x.MinIntegerValue)
                    .LessThanOrEqualTo(x => x.MaxIntegerValue)
                    .WithMessage("The default value must be between the minimum and maximun value");
                When(x => x.IsRequired, () =>
                {
                    RuleFor(x => x.DefaultValueInteger).Null().WithMessage("Required");
                });
            });

            When(x => x.DataType == DataType.String, () =>
            {
                RuleFor(x => x.MaxLengthString).Must(x => x.HasValue).WithMessage("Required");
                When(x => x.IsRequired, () =>
                {
                    RuleFor(x => x.DefaultValueString).Null().WithMessage("Required");
                });
            });
        }
    }
}
