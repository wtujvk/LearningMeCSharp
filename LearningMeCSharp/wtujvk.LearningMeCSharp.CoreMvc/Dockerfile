FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY LearningMeCSharp.sln ./
COPY wtujvk.LearningMeCSharp.CoreMvc/wtujvk.LearningMeCSharp.CoreMvc.csproj wtujvk.LearningMeCSharp.CoreMvc/
COPY wtujvk.LearningMeCSharp.ToolStandard/wtujvk.LearningMeCSharp.ToolStandard.csproj wtujvk.LearningMeCSharp.ToolStandard/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/wtujvk.LearningMeCSharp.CoreMvc
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "wtujvk.LearningMeCSharp.CoreMvc.dll"]
