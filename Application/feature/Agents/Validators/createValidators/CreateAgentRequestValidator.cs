using Application.Models.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.feature.Agents.Validators.createValidators
{
    /// <summary>
    /// Validator for creating a new agent
    /// This ensures all agent data is valid before saving to database
    /// </summary>
    public class CreateAgentRequestValidator : AbstractValidator<CreateAgentRequest>
    {
      //  private readonly IAgentService _agentService;  // Service to check if agent exists
        public CreateAgentRequestValidator()
        {
                

            // ==========================================
            // FIRST NAME VALIDATION
            // ==========================================
            RuleFor(x => x.FirstName)  // Which property we're validating
                .NotEmpty()  // Cannot be null or empty
                    .WithMessage("First name is required")  // Error message if validation fails
                .MaximumLength(50)  // Cannot exceed 50 characters
                    .WithMessage("First name must not exceed 50 characters")
                .MinimumLength(2)  // Must be at least 2 characters
                    .WithMessage("First name must be at least 2 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")  // Only allow letters, spaces, hyphens, apostrophes
                    .WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes");

            // ==========================================
            // LAST NAME VALIDATION
            // ==========================================
            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithMessage("Last name is required")
                .MaximumLength(50)
                    .WithMessage("Last name must not exceed 50 characters")
                .MinimumLength(2)
                    .WithMessage("Last name must be at least 2 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                    .WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes");

            // ==========================================
            // PHONE NUMBER VALIDATION
            // ==========================================
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$")  // International format: +1234567890
                    .WithMessage("Invalid phone number format. Use international format (e.g., +1234567890)")
                .MaximumLength(15)  // Maximum length for international phone numbers
                    .WithMessage("Phone number must not exceed 15 characters");

            // ==========================================
            // EMAIL VALIDATION
            // ==========================================
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()  // Built-in email validation
                    .WithMessage("Invalid email address format")
                .MaximumLength(100)
                    .WithMessage("Email must not exceed 100 characters")
                .Must(BeAValidEmailDomain)  // Custom validation method
                    .WithMessage("Email domain must be valid");
        }

        /// <summary>
        /// Custom validation method to check if email domain is valid
        /// Example: user@domain.com - domain must have a dot (.)
        /// </summary>
        private bool BeAValidEmailDomain(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email &&
                       !string.IsNullOrEmpty(addr.Host) &&
                       addr.Host.Contains(".");  // Domain must have a dot (e.g., gmail.com)
            }
            catch
            {
                return false;  // Invalid email format
            }
        }
    }
}