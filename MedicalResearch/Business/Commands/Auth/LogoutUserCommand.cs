using MediatR;

namespace MedicalResearch.Business.Commands.Auth
{
    public class LogoutUserCommand : IRequest<Unit>
    {
    }
}
