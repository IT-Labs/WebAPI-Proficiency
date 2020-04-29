using FluentValidation;

namespace WebApplication.Validation
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecastDto>
    {
        public WeatherForecastValidator()
        {
            RuleSet("all", () =>
            {
                RuleFor(x => x.City).Must(CheckId).WithMessage("id must be greater than 0");
                RuleFor(x => x.TemperatureC).NotNull().When(x => x.TemperatureC != null).WithMessage("name could not be null");
            });
          
            RuleSet("city", () =>
            {
                RuleFor(x => x.City).NotNull().WithMessage("City could not be null");
            });
        }

        private bool CheckId(int? id)
        {
            return !id.HasValue || id.Value > 0;
        }
    }
}
