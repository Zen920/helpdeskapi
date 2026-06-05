using FluentValidation;
using HelpDeskAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace HelpDeskAPI.Application.Validation;

public partial class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(r => r.Password)
            .NotEmpty()
            .Length(3,20)
            .Custom((p, context) => 
            {
                if(!PasswordRegex().IsMatch(p)) context.AddFailure("Password does not match security standards.");
            });
    }

    [GeneratedRegex("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/")]
    private static partial Regex PasswordRegex();
}
