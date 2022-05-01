using Email.Application.Models.Dto;
using FluentValidation;

namespace Email.Application.Validators
{
    public class EmailValidator : AbstractValidator<EmailDto>
    {
        public EmailValidator()
        {
            RuleFor(r => r).NotNull();
            RuleFor(r => r.DictionaryData).NotNull();
            RuleFor(r => r.SubjectMail).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(r => r.TemplateName).NotNull().NotEmpty();
            RuleFor(r => r.TemplateType).NotNull().NotEmpty();
            RuleForEach(r => r.Recipients).NotNull().EmailAddress();
        }
    }
}
