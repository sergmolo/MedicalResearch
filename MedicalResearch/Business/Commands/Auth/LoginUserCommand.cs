using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Auth
{
    public class LoginUserCommand : IRequest<Unit>
    {
        public LoginRequest Model { get; }

        public LoginUserCommand(LoginRequest model)
        {
            Model = model;
        }
    }
}
