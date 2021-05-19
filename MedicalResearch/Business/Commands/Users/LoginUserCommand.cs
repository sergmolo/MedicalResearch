using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
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
