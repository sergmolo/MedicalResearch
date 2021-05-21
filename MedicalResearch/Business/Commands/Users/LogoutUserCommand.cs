using MediatR;
using MedicalResearch.Business.Models;

namespace MedicalResearch.Business.Commands.Users
{
    public class LogoutUserCommand : IRequest<CommandResult>
    {
    }
}
