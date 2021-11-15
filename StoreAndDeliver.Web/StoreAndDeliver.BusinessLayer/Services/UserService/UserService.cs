using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Exceptions;
using StoreAndDeliver.BusinessLayer.Services.EmailService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public UserService(IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<AppUser> CreateUserAsync(CreateUserDto model, string language, bool sendConfirmationEmail)
        {
            if(model.ConfirmPassword != model.Password)
            {
                throw new PasswordsMismatchException();
            }
            AppUser existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException();
            }

            AppUser user = _mapper.Map<AppUser>(model);
            user.RegistryDate = DateTime.Now;
            if (!sendConfirmationEmail)
            {
                user.EmailConfirmed = true;
            }
            IdentityResult addUserResult = await _userManager.CreateAsync(user, model.Password);

            ValidateIdentityResult(addUserResult);

            if (sendConfirmationEmail)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", user.Email }
                };
                string url = QueryHelpers.AddQueryString(model.ClientURIForEmailConfirmation, param);
                await _emailService.SendEmail(user, url, language);
            }
            return await GetUserByUsername(user.UserName);
        }

        public async Task<ConfirmEmailDto> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null)
                return null;
            var confirmResult = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!confirmResult.Succeeded)
                return null;
            return confirmEmailDto;
        }

        public async Task<AppUser> UpdateUserAsync(UpdateUserDto model)
        {
            List<string> errors = new List<string>();
            Boolean result = ValidatePasswords(model, out errors);

            if (!result)
            {
                throw new InvalidUserPasswordException(String.Join(" ", errors));
            }

            AppUser existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null && existingUser.Id != model.Id)
            {
                throw new UsernameAlreadyTakenException();
            }

            AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            _mapper.Map(model, user);

            IdentityResult updateUserResult = await _userManager.UpdateAsync(user);
            ValidateIdentityResult(updateUserResult);

            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                IdentityResult changePasswordsResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                if (!changePasswordsResult.Succeeded)
                {
                    throw new InvalidUserPasswordException(String.Join(" ", errors));
                }
            }

            return await GetUserByUsername(user.UserName);
        }

        public async Task DeleteUser(Guid userId)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(userId.ToString());
            IdentityResult deleteUserResult = await _userManager.DeleteAsync(userToDelete);

            ValidateIdentityResult(deleteUserResult);
        }

        private static void ValidateIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                String errorsMessage = result.Errors
                                         .Select(er => er.Description)
                                         .Aggregate((i, j) => i + ";" + j);
                throw new Exception(errorsMessage);
            }
        }

        private static bool ValidatePasswords(UpdateUserDto model, out List<String> errors)
        {
            errors = new List<string>();
            if (String.IsNullOrEmpty(model.Password) &&
                String.IsNullOrEmpty(model.NewPassword) &&
                String.IsNullOrEmpty(model.ConfirmPassword))
            {
                return true;
            }

            if (String.IsNullOrEmpty(model.Password) ||
                String.IsNullOrEmpty(model.NewPassword) ||
                String.IsNullOrEmpty(model.ConfirmPassword))
            {
                errors.Add(ErrorMessagesConstants.NOT_ALL_PASS_FIELDS_FILLED);
            }

            if (!model.NewPassword.Equals(model.ConfirmPassword))
            {
                errors.Add(ErrorMessagesConstants.PASSWORDS_DO_NOT_MATCH);
            }

            return !errors.Any();
        }
    }
}
