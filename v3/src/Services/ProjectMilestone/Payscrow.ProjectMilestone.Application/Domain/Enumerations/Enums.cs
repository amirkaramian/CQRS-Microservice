namespace Payscrow.ProjectMilestone.Application.Domain.Enumerations
{
    public enum ProjectPartnerRoleType
    {
        Owner = 5,
        Implementer = 10
    }

    public enum TransactionType
    {
        Credit = 5,
        Debit = 10
    }

    public enum ProjectInviteStatus
    {
        Pending = 5,
        Accepted = 10,
        Rejected = 15
    }
}
