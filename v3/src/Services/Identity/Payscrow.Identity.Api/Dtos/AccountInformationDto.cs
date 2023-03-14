namespace Payscrow.Identity.Api.Dtos
{
    public class AccountInformationDto
    {
        public UserInfoDto UserInfo { get; set; } = new UserInfoDto();

        public class UserInfoDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
        }
    }
}