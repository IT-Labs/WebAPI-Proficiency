using FluentValidation;
using System;
using WebApplication.Contracts;

namespace WebApplication.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The First Name cannot be blank.")
                                        .Length(0, 100).WithMessage("The First Name cannot be more than 100 characters.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name cannot be blank.");

            RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
        }
    }
}
