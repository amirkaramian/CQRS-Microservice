using Moq;
using Payscrow.Core.Bus;
using Payscrow.Core.Interfaces;

namespace Payscrow.PaymentInvite.UnitTests.Application
{
    public class SellerCreateDealCommandHandlerTest
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IEventBus> _eventBusMock;
    }
}
