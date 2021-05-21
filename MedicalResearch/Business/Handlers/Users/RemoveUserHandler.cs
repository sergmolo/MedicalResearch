﻿using MediatR;
using MedicalResearch.Business.Commands.Users;
using MedicalResearch.Business.Enums;
using MedicalResearch.Business.Models;
using MedicalResearch.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalResearch.Business.Handlers.Users
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserByIdCommand, CommandResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public RemoveUserHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(RemoveUserByIdCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                return CommandResult.Failed(CommandErrorCode.UserNotFound);
            }

            user.IsRemoved = true;
            await _userManager.UpdateAsync(user);

            await _mediator.Send(new LogoutUserCommand(), ct);

            return CommandResult.Success();
        }
    }
}
