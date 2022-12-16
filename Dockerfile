FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as dev

FROM dev as tests

COPY ./src /app/

# Run tests and generate tests report
WORKDIR /app/SpaceBattleTests/
RUN dotnet test --collect:"XPlat Code Coverage" .
# Install reportgenerator
RUN dotnet add package ReportGenerator --version 5.1.10

# Create Report
WORKDIR /app/
CMD dotnet /$(whoami)/.nuget/packages/reportgenerator/5.1.10/tools/net6.0/ReportGenerator.dll \
     -reports:$(find . -name "coverage.cobertura.xml") \
     -targetdir:coveragereport \
     "-reporttypes:HtmlInline;Badges" \
     "-assemblyfilters:+space-battle"
