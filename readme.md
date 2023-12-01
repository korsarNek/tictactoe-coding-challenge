To build the project, dotnet is needed.

# Installing dependencies

## Linux

### package manager installation
`sudo apt install dotnet-sdk-7.0`

This should install the sdk and the necessary runtimes, but this for example only works for Ubuntu 22.10 or newer.

For other versions, this [website](https://learn.microsoft.com/de-de/dotnet/core/install/linux-ubuntu#register-the-microsoft-package-repository) contains some useful information on how to add the dotnet repository to the local package manager.

### manual installation

dotnet can also be installed manually, but this process might be a bit more error prone, especially if a different version of dotnet is already installed.

```
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
sudo ./dotnet-install.sh --architecture x64 --install-dir /usr/share/dotnet/ --runtime dotnet --version 7.0.14
```

## Windows

You can download the dotnet sdk by running `winget install Microsoft.DotNet.SDK.7` on the command line.

# Build

The project can be build from the root directory by running `dotnet build`, it then builds all sub projects, too.

If you want to run just the tests, you can use `dotnet test`.

To execute the game, you can either find the executable in the related bin directory, or run this `dotnet run --project Game`.
