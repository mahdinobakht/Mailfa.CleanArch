using Ardalis.Result;
using Ardalis.Specification;

using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.ProjectAggregate;
using Mailfa.CleanArch.Core.ProjectAggregate.Helpers;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications.hMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Specifications.webMail;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;
using Mailfa.CleanArch.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Mailfa.CleanArch.Core.ProjectAggregate.Enums;

namespace Mailfa.CleanArch.Core.Services
{
    class UserService : IUserService
    {
        private readonly IRepository<WebMail_Users> userRepository;
        private readonly IRepository<WebMail_PreSignupConfirm> preSignupConfirmRepository;
        private readonly IRepository<WebMail_Groups> groupRepository;
        private readonly IRepository<WebMail_CheckRequest> checkRequestRepository;
        private readonly IRepository<hm_accounts> hmAccountsRepositroy;
        private readonly IHmAccountsService hmAccountsService;

        public UserService(IRepository<WebMail_Users> _userRepository, IRepository<WebMail_PreSignupConfirm> _preSignupConfirmRepository,
            IRepository<WebMail_Groups> _groupRepository, IHmAccountsService _hmAccountsService, IRepository<hm_accounts> _hmAccountsRepositroy,
             IRepository<WebMail_CheckRequest> _checkRequestRepository)
        {
            userRepository = _userRepository;
            preSignupConfirmRepository = _preSignupConfirmRepository;
            groupRepository = _groupRepository;
            checkRequestRepository = _checkRequestRepository;
            hmAccountsService = _hmAccountsService;
            hmAccountsRepositroy = _hmAccountsRepositroy;

        }

        private async Task<Result<bool>> PreSignupConfirmInsert(string mobileNumber, string token)
        {
            var result = false;

            var pscSpec = new PreSignupConfirmWithItemsSpec(mobileNumber);
            var pscData = await preSignupConfirmRepository.FirstOrDefaultAsync(pscSpec);

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
                        await preSignupConfirmRepository.UpdateAsync(pscData);
                        return Result.Success(true, VerificationCode);
                    }
                }
            }
            else //insert SignupConfirm 
            {
                var insData = await preSignupConfirmRepository.AddAsync(new WebMail_PreSignupConfirm
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
                var userData = await userRepository.FirstOrDefaultAsync(userSpec);
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
                var pscData = await preSignupConfirmRepository.FirstOrDefaultAsync(pscSpec);

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
                // Create Account
                var domainId = hmAccountsService.GetDomainId();

                //var groupSpec = new GenericSpecification<WebMail_Groups>(x => x.Group_Default == true && x.Group_DomainId == domainId) as ISpecification<WebMail_Groups>;
                //var group = WebMailGroups.Where(x => x.GroupDefault == true && x.GroupDomainId == domainId).FirstOrDefault();

                var groupSpec = new GroupByDefaultAndDomainIdSpec(domainId);
                var groupData = await groupRepository.FirstOrDefaultAsync(groupSpec);
                if (groupData != null)
                {
                    var account = await hmAccountsService.CreateNewAccount(request.MobileNumber, "", "", "1qaz!QAZ", groupData.Group_Id);
                    if (account != null)
                    {
                        await userRepository.AddAsync(new WebMail_Users
                        {
                            User_AccountID = account.ID,
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


        public async Task<Result<ForgotPasswordOutput>> ForgotPassword(ForgotPasswordInput request)
        {
            var result = new ForgotPasswordOutput();
            try
            {
                var clientIp = GeneralHelper.GetLocalIPAddress();
                if (string.IsNullOrEmpty(request.Email))
                    return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("EmailIsEmpty"));

                var checkRequestSpec = new CheckRequestByIpAndTypeSpec(clientIp, (int)RequestTypes.ForgetPassword);
                var checkRequesData = await checkRequestRepository.FirstOrDefaultAsync(checkRequestSpec);

                if (checkRequesData == null)// First Time Request
                {
                    await checkRequestRepository.AddAsync(new WebMail_CheckRequest
                    {
                        CR_Count = 1,
                        CR_ExpireDate = DateTime.Now.AddHours(2),
                        CR_IP = clientIp,
                        CR_Type = (int)RequestTypes.ForgetPassword
                    });

                    return await GetForgotPasswordRecoveryMethod(request);
                }
                else
                {
                    if (checkRequesData.CR_ExpireDate <= DateTime.Now) // 2 Hour Left For Last Request
                    {
                        checkRequesData.CR_Count = 1;
                        checkRequesData.CR_ExpireDate = DateTime.Now.AddHours(2);
                        await checkRequestRepository.UpdateAsync(checkRequesData);
                    }

                    var maxForgotPasswordCount = Convert.ToInt32(GeneralHelper.GetConfigurationKey("MaxForgotPasswordCount"));
                    if (checkRequesData.CR_Count >= maxForgotPasswordCount)
                        return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("MaxRequestCountPassed"));
                    else//   Request Again
                    {
                        checkRequesData.CR_Count++;
                        await checkRequestRepository.UpdateAsync(checkRequesData);

                        return await GetForgotPasswordRecoveryMethod(request);
                    }
                }
            }
            catch (Exception ex)
            {
                return Result<ForgotPasswordOutput>.Error(ex.Message);
            }
        }

        private async Task<Result<ForgotPasswordOutput>> GetForgotPasswordRecoveryMethod(ForgotPasswordInput request)
        {
            var result = new ForgotPasswordOutput();

            if (!request.Email.Contains("@"))
                request.Email = request.Email + "@" + GeneralHelper.GetConfigurationKey("DefaultDomainName");

            var domainId = hmAccountsService.GetDomainId();
            var hmAccountsSpec = new HmAccountsWithAccountIdAndAddressSpec(domainId, request.Email);
            var hmAccountsData = await hmAccountsRepositroy.FirstOrDefaultAsync(hmAccountsSpec);


            //  Get Recovery Metohd
            if (hmAccountsData != null && hmAccountsData.Accountactive == 1)
            {
                List<string> types = new List<string>();

                var userSpec = new UserByAccountIdSpec(hmAccountsData.accountid);
                var userData = await userRepository.FirstOrDefaultAsync(userSpec);

                if (userData != null)
                {
                    if (userData.User_Mobile != null) //  Mobile Exists
                    {
                        if (userData.User_Mobile.Length == 10 && userData.User_Mobile.StartsWith("9") || userData.User_Mobile.Length == 11 && userData.User_Mobile.StartsWith("09"))
                            types.Add("mobile");
                    }
                    if (userData.User_OptionalEmail != null) //  Recovery Email Exists
                    {
                        if (!string.IsNullOrEmpty(userData.User_OptionalEmail) && userData.User_OptionalEmail.Contains("@"))
                            types.Add("email");
                    }
                    if (userData.User_QuestionID != null) //  Security Question Exists
                    {
                        if (!string.IsNullOrEmpty(userData.User_QuestionAnswer) && userData.User_QuestionAnswer.Length >= 3)
                            types.Add("question");
                    }
                }

                if (types.Count > 0)
                {
                    result.RecoveryMethodList = types.ToList();
                    return Result<ForgotPasswordOutput>.Success(value: result);
                }
                else
                    return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("NotRecoveryMethodsExist"));
            }
            else
            {
                if (hmAccountsData != null && hmAccountsData.Accountactive == 0)
                {
                    var xpo = GeneralHelper.GetResourcesValue("AccountIsDisabled");
                    return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("AccountIsDisabled"));
                }
                else
                {
                    var domainName = "";
                    if (!GeneralHelper.IsValidEmailAddress(request.Email))
                        return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("EmailNotExist"));
                    else
                    {
                        domainName = request.Email.Substring(request.Email.IndexOf("@") + 1);
                        try
                        {
                            if (hmAccountsService.GetDomainData(domainName) == null)
                                return Result<ForgotPasswordOutput>.Error(String.Format(GeneralHelper.GetResourcesValue("DomainIsNotRelatedSystem"), domainName));
                            else
                                return Result<ForgotPasswordOutput>.Error(GeneralHelper.GetResourcesValue("EmailNotExist"));
                        }
                        catch
                        {
                            return Result<ForgotPasswordOutput>.Error(String.Format(GeneralHelper.GetResourcesValue("DomainIsNotRelatedSystem"), domainName));
                        }
                    }
                }
            }
        }




    }
}
