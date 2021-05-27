using MediatR;
using MedicalResearch.Business.Models;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Auth
{
    public class LoginUserCommand : IRequest<CommandResult>
    {
        public LoginRequest Model { get; }

        public LoginUserCommand(LoginRequest model)
        {
            Model = model;
        }
    }
}
