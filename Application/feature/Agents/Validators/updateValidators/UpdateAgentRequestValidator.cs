using Application.Models.Requests;
using FluentValidation;

namespace Application.feature.Agents.Validators
{
    /// <summary>
    /// Validator for updating an existing agent
    /// Includes ID validation plus all the same field validations as Create
    /// </summary>
    public class UpdateAgentRequestValidator : AbstractValidator<UpdateAgentRequest>
    {
        private readonly IAgentService _agentService;  // Service to check if agent exists

        public UpdateAgentRequestValidator(IAgentService agentService)
        {
            // Store the service for use in validation methods
            _agentService = agentService;

            // ==========================================
            // ID VALIDATION - Ensures we're updating a valid agent
            // ==========================================
            RuleFor(x => x.Id)
                .GreaterThan(0)  // ID must be a positive number
                    .WithMessage("Agent ID must be greater than 0")
                .MustAsync(async (id, cancellation) => await _agentService.DoesExistAsync(id))
                    .WithMessage("Agent does not exist. Please provide a valid Agent ID.");

            // ==========================================
            // FIRST NAME VALIDATION
            // ==========================================
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithMessage("First name is required")
                .MaximumLength(50)
                    .WithMessage("First name must not exceed 50 characters")
                .MinimumLength(2)
                    .WithMessage("First name must be at least 2 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
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
                .Matches(@"^\+?[1-9]\d{1,14}$")
                    .WithMessage("Invalid phone number format. Use international format (e.g., +1234567890)")
                .MaximumLength(15)
                    .WithMessage("Phone number must not exceed 15 characters");

            // ==========================================
            // EMAIL VALIDATION
            // ==========================================
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Invalid email address format")
                .MaximumLength(100)
                    .WithMessage("Email must not exceed 100 characters")
                .Must(BeAValidEmailDomain)
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
                       addr.Host.Contains(".");
            }
            catch
            {
                return false;
            }
        }
    }
}