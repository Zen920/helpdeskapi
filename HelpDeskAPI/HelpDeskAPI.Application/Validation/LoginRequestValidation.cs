using FluentValidation;
using HelpDeskAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace HelpDeskAPI.Application.Validation;

internal partial class LoginRequestValidation : AbstractValidator<LoginRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(r => r.Password)
            .NotEmpty()
            .Length(3,20)
            .Custom((p, context) =>
            {
                PasswordRegex().IsMatch(p);
            });
    }

    [GeneratedRegex("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/")]
    private static partial Regex PasswordRegex();
}
