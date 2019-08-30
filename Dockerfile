FROM centos:latest

WORKDIR /root

RUN yum -y update && \
   rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm && \
   yum -y install dotnet-sdk-2.2

COPY Core/UrlShortener.Api/obj/Debug/netcoreapp2.2/linux-x64 ./

ENTRYPOINT [ "dotnet", "UrlShortener.Api.dll" ]

EXPOSE 5000
