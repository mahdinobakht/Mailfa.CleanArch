using Ardalis.Result;
using Ardalis.Specification;

using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate;
using Mailfa.CleanArch.Core.ProjectAggregate.Helpers;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Models;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;
using Mailfa.CleanArch.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Mailfa.CleanArch.Core.Services
{
    class UserService : IUserService
    {
        private readonly IRepository<WebMail_Users> _userRepository;
        private readonly IRepository<WebMail_PreSignupConfirm> _preSignupConfirmRepository;
        private readonly IRepository<WebMail_Groups> _groupRepository;
        private readonly IRepository<hm_accounts> _hmAccountsRepositroy;
        private readonly IHmAccountsService _hAccountsService;

        public UserService(IRepository<WebMail_Users> userRepository, IRepository<WebMail_PreSignupConfirm> preSignupConfirmRepository,
            IRepository<WebMail_Groups> groupRepository, IHmAccountsService hAccountsService, IRepository<hm_accounts> hmAccountsRepositroy)
        {
            _userRepository = userRepository;
            _preSignupConfirmRepository = preSignupConfirmRepository;
            _groupRepository = groupRepository;
            _hAccountsService = hAccountsService;
            _hmAccountsRepositroy = hmAccountsRepositroy;

        }

        private async Task<Result<bool>> PreSignupConfirmInsert(string mobileNumber, string token)
        {
            var result = false;

            var pscSpec = new PreSignupConfirmWithItemsSpec(mobileNumber);
            var pscData = await _preSignupConfirmRepository.FirstOrDefaultAsync(pscSpec);

            Random rnd = new Random();
            var code = rnd.Next(1000, 9999);
            string VerificationCode = "mf" + code.ToString();

            if (pscData != null)
            {
                if (pscData.PSC_Code != null)
                {
                    if (pscData.PSC_TryCount >= 7)
                        return Result.Error(GeneralHelper.GetResourcesValue("TryCodeOverLimit"));
                    else
                    {
                        pscData.PSC_Code = VerificationCode;
                        pscData.PSC_TryCount++;
                        pscData.PSC_Token = token;
                        await _preSignupConfirmRepository.UpdateAsync(pscData);
                        return Result.Success(true, VerificationCode);
                    }
                }
            }
            else //insert SignupConfirm 
            {
                var insData = await _preSignupConfirmRepository.AddAsync(new WebMail_PreSignupConfirm
                {
                    PSC_Mobile = mobileNumber,
                    PSC_TryCount = 1,
                    PSC_Code = VerificationCode,
                    PSC_CreateDate = DateTime.Now,
                    PSC_Valid = true,
                    PSC_IP = GeneralHelper.GetLocalIPAddress(),
                    PSC_Token = token
                }); ;

                if (insData != null && insData.PSC_Id != null)
                    return Result.Success(true, VerificationCode);
            }

            return result;
        }

        public async Task<Result<GetVerificationCodeOutput>> GetVerificationCode(string mobileNumber)
        {

            var result = new GetVerificationCodeOutput();
            Int64 mobileNumberInt = 0;

            try
            {

                Int64.TryParse(mobileNumber, out mobileNumberInt);

                if (mobileNumber.Trim().Length != 11)
                    return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("InvalidMobileNumber"));

                var userSpec = new UserByMobileWithItemsSpec(mobileNumber);
                var userData = await _userRepository.FirstOrDefaultAsync(userSpec);
                if (userData != null)
                {
                    return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("UserExist"));

                }
                else // User Not Exist
                {
                    string token = System.Guid.NewGuid().ToString();
                    var resultIns = await PreSignupConfirmInsert(mobileNumber, token);

                    if (resultIns.IsSuccess)
                    {
                        if (SMSHelper.SendVerifyCode(mobileNumber, resultIns.SuccessMessage))
                            result.Token = GeneralHelper.ComputeHash(token);
                        else
                            return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("SendSMSFailed"));
                    }
                    else
                        return Result<GetVerificationCodeOutput>.Error(resultIns.Errors.First());
                }

                return Result<GetVerificationCodeOutput>.Success(value: result);
            }
            catch (Exception ex)
            {
                return Result<GetVerificationCodeOutput>.Error(ex.Message);
            }
        }
        public async Task<Result<GetVerificationCodeOutput>> VerifyVerificationCode(VerifyVerificationCodeInput request)
        {
            var result = new GetVerificationCodeOutput();
            try
            {
                var pscSpec = new PreSignupConfirmWithItemsSpec(request.MobileNumber);
                var pscData = await _preSignupConfirmRepository.FirstOrDefaultAsync(pscSpec);

                if (pscData != null)
                {
                    if (GeneralHelper.ComputeHash(pscData.PSC_Token) != request.Token)
                        return Result.Error(GeneralHelper.GetResourcesValue("InvalidData"));

                    if (pscData.PSC_Code != request.Code)
                        return Result.Error(GeneralHelper.GetResourcesValue("InvalidData"));
                }

                return Result<GetVerificationCodeOutput>.Success(value: result);
            }
            catch (Exception ex)
            {
                return Result<GetVerificationCodeOutput>.Error(ex.Message);
            }
        }

        public async Task<Result<GetVerificationCodeOutput>> Signup(VerifyVerificationCodeInput request)
        {
            var result = new GetVerificationCodeOutput();
            try
            {
                // ایجاد اکانت
                var domainId = _hAccountsService.GetDomainId();

                //var groupSpec = new GenericSpecification<WebMail_Groups>(x => x.Group_Default == true && x.Group_DomainId == domainId) as ISpecification<WebMail_Groups>;
                //var group = WebMailGroups.Where(x => x.GroupDefault == true && x.GroupDomainId == domainId).FirstOrDefault();

                var groupSpec = new GroupByDefaultAndDomainIdSpec(domainId);
                var groupData = await _groupRepository.FirstOrDefaultAsync(groupSpec);
                if (groupData != null)
                {
                    var account = _hAccountsService.createNewAccount(request.MobileNumber, "", "", "1qaz!QAZ", groupData.Group_Id);
                    if (account != null)
                    {
                        await _userRepository.AddAsync(new WebMail_Users
                        {
                            User_AccountID = account.Id,
                            User_Mobile = request.MobileNumber,
                            User_SignupIP = GeneralHelper.GetLocalIPAddress(),
                            User_RegisterDate = DateTime.Now
                        });
                    }

                    return Result<GetVerificationCodeOutput>.Success(value: result);
                }
                else
                    return Result.Error(GeneralHelper.GetResourcesValue("InvalidData"));
            }
            catch (Exception ex)
            {
                return Result<GetVerificationCodeOutput>.Error(ex.Message);
            }
        }


        public async Task<Result<GetVerificationCodeOutput>> ForgotPassword(ForgotPasswordInput request)
        {
            var result = new GetVerificationCodeOutput();
            try
            {
                var groupSpec = new GenericSpecification<WebMail_Groups>(x => x.Group_Default == true && x.Group_DomainId == domainId) as ISpecification<WebMail_Groups>;

                //if (string.IsNullOrEmpty(request.Email))
                //    return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("EmailIsEmpty") );  
                //else if (!Common.Security.canSendRequest(ip, 13, Enums.RequestTypes.ForgetPassword, 3, out int requestCount))
                //    return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("MaxRequestCountPassed"));
                //else if (string.IsNullOrEmpty(request.Captcha) || !Common.Security.verifyCaptcha(captcha))
                //    return Result<GetVerificationCodeOutput>.Error(GeneralHelper.GetResourcesValue("InvalidCaptch"));
                //else
                //{

                //}
                return result;
            }
            catch (Exception ex)
            {
                return Result<GetVerificationCodeOutput>.Error(ex.Message);
            }


        }




    }
}
