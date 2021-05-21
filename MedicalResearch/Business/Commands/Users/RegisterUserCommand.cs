using MediatR;
using MedicalResearch.V1.Requests;
using Microsoft.AspNetCore.Identity;

namespace MedicalResearch.Business.Commands.Users
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public RegisterRequest Model { get; }

        public RegisterUserCommand(RegisterRequest model)
        {
            Model = model;
        }
    }
}
