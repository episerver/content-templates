# Optimizely Templates for .NET

[![Continuous integration](https://github.com/episerver/content-templates/actions/workflows/ci.yml/badge.svg)](https://github.com/episerver/content-templates/actions/workflows/ci.yml)

This repository contains templates for Optimizely Digital Experience Cloud to be used with `dotnet new` and Visual Studio 2022 17.8.0+.

The templates in this repository are distributed as a NuGet package available from [NuGet.org](https://www.nuget.org/packages/EPiServer.Templates/) and can be installed using the `dotnet new` command.

```bash
$ dotnet new -i EPiServer.Templates
```

Once installed, these templates will also be available in Visual Studio 17.8.0+.
Note that Visual Studio currently only supports *Project Templates* and not *Item Templates*.

To generate code based on the template simply execute the following.

```bash
$ dotnet new epi-alloy-mvc
```

Replace `epi-alloy-mvc` with the name of the template you want to use. 

See below for a list of available templates and details on each individual template. For direct access to documentation on each template, execute:

```bash
$ dotnet new epi-alloy-mvc --help
```

For further documentation on how to work with `dotnet new` see https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new.

## Available templates

The following templates are included in this repository and available through the `EPiServer.Templates` package.

### Project Templates

- epi-alloy-mvc
- epi-cms-empty
- epi-commerce-empty


### Item Templates

- epi-cms-contentcomponent
- epi-cms-contenttype
- epi-cms-initializationmodule
- epi-cms-job
- epi-cms-pagecontroller
- epi-cms-razorpage

## Contributing

We would love community contributions here. The easiest way to contribute is to join in with the discussions on Github issues or create new issues with questions, suggestions or any other feedback. 
If you want to contribute code or documentation, you are more than welcome to create pull-requests, but make sure that you read the [contribution page](CONTRIBUTING.md) first.

## License

This project is licensed with the [Apache license](LICENSE.md)
