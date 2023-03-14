#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src


# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY ["PayscrowOnContainers.sln", "PayscrowOnContainers.sln"]

COPY ["ApiGateway/Web.Bff/Payscrow.Web.HttpAggregator/Payscrow.Web.HttpAggregator.csproj", "ApiGateway/Web.Bff/Payscrow.Web.HttpAggregator/"]
COPY ["BuildingBlocks/Infrastructure/Payscrow.Infrastructure.Common/Payscrow.Infrastructure.Common.csproj", "BuildingBlocks/Infrastructure/Payscrow.Infrastructure.Common/Payscrow.Infrastructure.Common.csproj"]
COPY ["BuildingBlocks/Infrastructure/Payscrow.Infrastructure.RabbitMQBus/Payscrow.Infrastructure.RabbitMQBus.csproj", "BuildingBlocks/Infrastructure/Payscrow.Infrastructure.RabbitMQBus/Payscrow.Infrastructure.RabbitMQBus.csproj"]
COPY ["BuildingBlocks/Payscrow.Core/Payscrow.Core.csproj", "BuildingBlocks/Payscrow.Core/Payscrow.Core.csproj"]
COPY ["Presentation/Payscrow.WebUI/Payscrow.WebUI.csproj", "Presentation/Payscrow.WebUI/Payscrow.WebUI.csproj"]
COPY ["Presentation/Payscrow.Admin/Payscrow.Admin.csproj", "Presentation/Payscrow.Admin/Payscrow.Admin.csproj"]
COPY ["Services/Identity/Payscrow.Identity.Api/Payscrow.Identity.Api.csproj", "Services/Identity/Payscrow.Identity.Api/Payscrow.Identity.Api.csproj"]
COPY ["Services/PaymentInvite/Payscrow.PaymentInvite.Api/Payscrow.PaymentInvite.Api.csproj", "Services/PaymentInvite/Payscrow.PaymentInvite.Api/Payscrow.PaymentInvite.Api.csproj"]
COPY ["Services/PaymentInvite/Payscrow.PaymentInvite.Application/Payscrow.PaymentInvite.Application.csproj", "Services/PaymentInvite/Payscrow.PaymentInvite.Application/Payscrow.PaymentInvite.Application.csproj"]
COPY ["Services/PaymentInvite/Payscrow.PaymentInvite.Data/Payscrow.PaymentInvite.Data.csproj", "Services/PaymentInvite/Payscrow.PaymentInvite.Data/Payscrow.PaymentInvite.Data.csproj"]
COPY ["Services/PaymentInvite/Payscrow.PaymentInvite.Domain/Payscrow.PaymentInvite.Domain.csproj", "Services/PaymentInvite/Payscrow.PaymentInvite.Domain/Payscrow.PaymentInvite.Domain.csproj"]
COPY ["Services/PaymentInvite/Payscrow.PaymentInvite.UnitTests/Payscrow.PaymentInvite.UnitTests.csproj", "Services/PaymentInvite/Payscrow.PaymentInvite.UnitTests/Payscrow.PaymentInvite.UnitTests.csproj"]
COPY ["Services/Notifications/Api/Payscrow.Notifications.Api/Payscrow.Notifications.Api.csproj", "Services/Notifications/Api/Payscrow.Notifications.Api/Payscrow.Notifications.Api.csproj"]
COPY ["Services/Escrow/Payscrow.Escrow.Api/Payscrow.Escrow.Api.csproj", "Services/Escrow/Payscrow.Escrow.Api/Payscrow.Escrow.Api.csproj"]
COPY ["Services/Escrow/Payscrow.Escrow.Application/Payscrow.Escrow.Application.csproj", "Services/Escrow/Payscrow.Escrow.Application/Payscrow.Escrow.Application.csproj"]
COPY ["Services/Escrow/Payscrow.Escrow.Data/Payscrow.Escrow.Data.csproj", "Services/Escrow/Payscrow.Escrow.Data/Payscrow.Escrow.Data.csproj"]
COPY ["Services/Escrow/Payscrow.Escrow.Domain/Payscrow.Escrow.Domain.csproj", "Services/Escrow/Payscrow.Escrow.Domain/Payscrow.Escrow.Domain.csproj"]
COPY ["Services/Payments/Payscrow.Payments.Api/Payscrow.Payments.Api.csproj", "Services/Payments/Payscrow.Payments.Api/Payscrow.Payments.Api.csproj"]
COPY ["Services/ProjectMilestone/Payscrow.ProjectMilestone.Api/Payscrow.ProjectMilestone.Api.csproj", "Services/ProjectMilestone/Payscrow.ProjectMilestone.Api/Payscrow.ProjectMilestone.Api.csproj"]
COPY ["Services/ProjectMilestone/Payscrow.ProjectMilestone.Application/Payscrow.ProjectMilestone.Application.csproj", "Services/ProjectMilestone/Payscrow.ProjectMilestone.Application/Payscrow.ProjectMilestone.Application.csproj"]
COPY ["Services/ProjectMilestone/Payscrow.ProjectMilestone.Data/Payscrow.ProjectMilestone.Data.csproj", "Services/ProjectMilestone/Payscrow.ProjectMilestone.Data/Payscrow.ProjectMilestone.Data.csproj"]
COPY ["Services/DisputeResolution/Payscrow.DisputeResolution.Api/Payscrow.DisputeResolution.Api.csproj", "Services/DisputeResolution/Payscrow.DisputeResolution.Api/Payscrow.DisputeResolution.Api.csproj"]
COPY ["Services/DisputeResolution/Payscrow.DisputeResolution.Application/Payscrow.DisputeResolution.Application.csproj", "Services/DisputeResolution/Payscrow.DisputeResolution.Application/Payscrow.DisputeResolution.Application.csproj"]
COPY ["Services/DisputeResolution/Payscrow.DisputeResolution.Data/Payscrow.DisputeResolution.Data.csproj", "Services/DisputeResolution/Payscrow.DisputeResolution.Data/Payscrow.DisputeResolution.Data.csproj"]
COPY ["Services/EscrowDirect/Payscrow.EscrowDirect.Api/Payscrow.EscrowDirect.Api.csproj", "Services/EscrowDirect/Payscrow.EscrowDirect.Api/Payscrow.EscrowDirect.Api.csproj"]
COPY ["Services/EscrowDirect/Payscrow.EscrowDirect.Application/Payscrow.EscrowDirect.Application.csproj", "Services/EscrowDirect/Payscrow.EscrowDirect.Application/Payscrow.EscrowDirect.Application.csproj"]
COPY ["Services/EscrowDirect/Payscrow.EscrowDirect.Data/Payscrow.EscrowDirect.Data.csproj", "Services/EscrowDirect/Payscrow.EscrowDirect.Data/Payscrow.EscrowDirect.Data.csproj"]
COPY ["Services/MarketPlace/Payscrow.MarketPlace.Api/Payscrow.MarketPlace.Api.csproj", "Services/MarketPlace/Payscrow.MarketPlace.Api/Payscrow.MarketPlace.Api.csproj"]
COPY ["Services/MarketPlace/Payscrow.MarketPlace.Application/Payscrow.MarketPlace.Application.csproj", "Services/MarketPlace/Payscrow.MarketPlace.Application/Payscrow.MarketPlace.Application.csproj"]



COPY ["docker-compose.dcproj", "docker-compose.dcproj"]

COPY ["NuGet.config", "NuGet.config"]

RUN dotnet restore "PayscrowOnContainers.sln"

COPY . .

WORKDIR ApiGateway/Web.Bff/Payscrow.Web.HttpAggregator

RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Payscrow.Web.HttpAggregator.dll"]