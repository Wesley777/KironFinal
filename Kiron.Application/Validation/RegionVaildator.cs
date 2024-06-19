using FluentValidation;
using Kiron.Application.Models;

namespace Kiron.Application.Validation;

public class RegionValidator : AbstractValidator<RegionRequest>
{
    public RegionValidator()
    {
        RuleFor(x => x.RegionId)
            .NotEmpty().WithMessage("Region Id is required.");
    }

}
