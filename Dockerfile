FROM microsoft/dotnet:sdk AS build-env
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh



# Copy csproj and restore as distinct layers
# COPY *.csproj ./

# Copy everything else and build
# COPY . ./
# RUN dotnet publish -c Release -o out

# # Build runtime image
# FROM microsoft/dotnet:aspnetcore-runtime
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "MvcAdoDemo.dll"]