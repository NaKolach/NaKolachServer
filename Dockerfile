FROM mcr.microsoft.com/dotnet/sdk:9.0.306-alpine3.22 AS base
WORKDIR /workspace

FROM base AS build
COPY --link . .
