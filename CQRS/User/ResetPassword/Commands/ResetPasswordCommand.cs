﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.ResetPassword;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.User.ResetPassword.Commands
{

    public class ResetPasswordCommand : IRequest<ResultViewModel<ResetPasswordRequest>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string newPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultViewModel<ResetPasswordRequest>>
    {
        UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultViewModel<ResetPasswordRequest>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return ResultViewModel<ResetPasswordRequest>.Failure(ErrorCode.ResourceNotFound, "User Not Found");
            }

            await _userManager.ResetPasswordAsync(user, request.Token, request.newPassword);
            var result = new ResetPasswordRequest()
            {
                Email = request.Email,
                newPassword = request.newPassword,
                ConfirmPassword = request.ConfirmPassword,
            };
            if (result == null || result.ConfirmPassword != result.newPassword)
                return ResultViewModel<ResetPasswordRequest>.Failure(ErrorCode.BadRequest, "Confirm Password Cant Match Password");

            return ResultViewModel<ResetPasswordRequest>.Success(result , "Your password has been successfully reset. You can now log in with your new password.");

        }
    }
}
