FROM mcr.microsoft.com/dotnet/sdk:9.0.200-alpine3.21 AS base
WORKDIR /workspace

FROM base AS build
COPY --link . .
