using System.Collections.Generic;
using Email.Application.Models.Dao;
using MongoDB.Driver;

namespace Email.Infrastructure.Database
{
    public class TemplateSeed
    {
        public static void SeedData(IMongoCollection<Template> tempCollection)
        {
            var existTemplate = tempCollection.Find(x => true).Any();
            if (!existTemplate)
            {
                tempCollection.InsertMany(GetTemplates());
            }
        }

        private static IEnumerable<Template> GetTemplates() =>
            new List<Template>()
            {
                new Template()
                {
                    TemplateType = "Account",
                    TemplateName = "ConfirmAccount",
                    TemplateContent =
                        "<!DOCTYPE html><html><body><h2>Hi {UserName}</h2></br><p><strong>" +
                        "Confirm your registration:<strong> " +
                        "<a href={VerificationUri}>" +
                        "confirmation</a></p></body></html>"
                },
                new Template()
                {
                    TemplateType = "Account",
                    TemplateName = "ResetPassword",
                    TemplateContent =
                        "<!DOCTYPE html><html><body><h4>Reset Password Email</h4>" +
                        "<p>Please use the below token to reset your password with the<code>" +
                        "/accounts/reset-password</code> api route:</p>" +
                        "<p><code>{resetToken}</code></p></body></html>"
                }
            };
    }
}
