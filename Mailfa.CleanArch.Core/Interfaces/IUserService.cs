using Ardalis.Result;
using Mailfa.CleanArch.Core.ProjectAggregate.ViewModel;

namespace Mailfa.CleanArch.Core.Interfaces
{
    public  interface IUserService
    {
        Task<Result<GetVerificationCodeOutput>> GetVerificationCode(string mobileNumber);
        Task<Result<GetVerificationCodeOutput>> VerifyVerificationCode(VerifyVerificationCodeInput request);
        Task<Result<GetVerificationCodeOutput>> Signup(VerifyVerificationCodeInput request);
        Task<Result<ForgotPasswordOutput>> ForgotPassword(ForgotPasswordInput request);
    }

}