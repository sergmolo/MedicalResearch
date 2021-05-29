using MediatR;
using MedicalResearch.V1.Requests;

namespace MedicalResearch.Business.Commands.Users
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public RegisterRequest Model { get; }

        public RegisterUserCommand(RegisterRequest model)
        {
            Model = model;
        }
    }
}
