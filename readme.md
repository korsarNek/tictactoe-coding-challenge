To build the project, dotnet is needed.
Linux

`sudo apt install dotnet-sdk-7.0`

`wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh`
`chmod +x ./dotnet-install.sh`
`./dotnet-install.sh --channel 7.0`

`https://learn.microsoft.com/de-de/dotnet/core/install/linux-ubuntu#register-the-microsoft-package-repository`
`sudo ./dotnet-install.sh --architecture x64 --install-dir /usr/share/dotnet/ --runtime dotnet --version 7.0.14`

dotnet build
dotnet test

dotnet run --project Game