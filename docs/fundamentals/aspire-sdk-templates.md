---
title: .NET Aspire templates
description: Learn about the .NET Aspire templates and how to use them to create new apps.
ms.date: 03/11/2025
zone_pivot_groups: dev-environment
uid: dotnet/aspire/templates
---

# .NET Aspire templates

There are a number of .NET Aspire project templates available to you. You can use these templates to create full .NET Aspire solutions, or add individual projects to existing .NET Aspire solutions.

The .NET Aspire templates are available in the [📦 Aspire.ProjectTemplates](https://www.nuget.org/packages/Aspire.ProjectTemplates) NuGet package.

## Available templates

The .NET Aspire templates allow you to create new apps pre-configured with the .NET Aspire solutions structure and default settings. These projects also provide a unified debugging experience across the different resources of your app.

.NET Aspire templates are available in two categories: solution templates and project templates. Solution templates create a new .NET Aspire solution with multiple projects, while project templates create individual projects that can be added to an existing .NET Aspire solution.

### Solution templates

The following .NET Aspire solution templates are available, assume the solution is named _AspireSample_:

<a name="empty-app"></a>

- **.NET Aspire Empty App**: A minimal .NET Aspire project that includes the following:

  - [**AspireSample.AppHost**](#app-host): An orchestrator project designed to connect and configure the different projects and services of your app.
  - [**AspireSample.ServiceDefaults**](#service-defaults): A .NET Aspire shared project to manage configurations that are reused across the projects in your solution related to [resilience](/dotnet/core/resilience/http-resilience), [service discovery](../service-discovery/overview.md), and [telemetry](telemetry.md).

<a name="starter-app"></a>

- **.NET Aspire Starter App**: In addition to the [**.AppHost**](#app-host) and [**.ServiceDefaults**](#service-defaults) projects, the .NET Aspire Starter App also includes the following:

  - **AspireSample.ApiService**: An [ASP.NET Core Minimal API](/aspnet/core/fundamentals/minimal-apis) project is used to provide data to the frontend. This project depends on the shared [**AspireSample.ServiceDefaults**](#service-defaults) project.
  - **AspireSample.Web**: An [ASP.NET Core Blazor App](/aspnet/core/blazor) project with default .NET Aspire service configurations, this project depends on the [**AspireSample.ServiceDefaults**](#service-defaults) project.
  - **AspireSample.Test**: Either an [MSTest](#mstest-project), [NUnit](#nunit-project), or [xUnit](#xunit-project) test project with project references to the [**AspireSample.AppHost**](#app-host) and an example _WebTests.cs_ file demonstrating an integration test.

### Project templates

The following .NET Aspire project templates are available:

<a name="app-host"></a>

- **.NET Aspire App Host**: A standalone **.AppHost** project that can be used to orchestrate and manage the different projects and services of your app.

<a name="mstest-project"></a>
<a name="nunit-project"></a>
<a name="xunit-project"></a>

- **.NET Aspire Test projects**: These project templates are used to create test projects for your .NET Aspire app, and they're intended to represent functional and integration tests. The test projects include the following templates:

  - **MSTest**: A project that contains MSTest integration of a .NET Aspire AppHost project.
  - **NUnit**: A project that contains NUnit integration of a .NET Aspire AppHost project.
  - **xUnit**: A project that contains xUnit.net integration of a .NET Aspire AppHost project.
  
  For more information on the test templates, see [Testing in .NET Aspire](testing.md).

<a name="service-defaults"></a>

- **.NET Aspire Service Defaults**: A standalone **.ServiceDefaults** project that can be used to manage configurations that are reused across the projects in your solution related to [resilience](/dotnet/core/resilience/http-resilience), [service discovery](../service-discovery/overview.md), and [telemetry](./telemetry.md).

  > [!IMPORTANT]
  > The service defaults project template takes a `FrameworkReference` dependency on `Microsoft.AspNetCore.App`. This may not be ideal for some project types. For more information, see [.NET Aspire service defaults](service-defaults.md).

## Install the .NET Aspire templates

[!INCLUDE [Install templates](includes/install-templates.md)]

## Create solutions and projects using templates

To create a .NET Aspire solution or project, use Visual Studio, Visual Studio Code, or the .NET CLI, and base it on the available templates. Explore additional .NET Aspire templates in the [.NET Aspire samples](https://github.com/dotnet/aspire-samples) repository.

:::zone pivot="visual-studio"

To create a .NET Aspire project using Visual Studio, search for *Aspire* in the Visual Studio new project window and select your desired template.

:::image type="content" source="media/vs-create-dotnet-aspire-proj.png" lightbox="media/vs-create-dotnet-aspire-proj.png" alt-text="Visual Studio: .NET Aspire templates.":::

Follow the prompts to configure your project or solution from the template, and then select **Create**.

:::zone-end
:::zone pivot="vscode"

To create a .NET Aspire project using Visual Studio Code, search for *Aspire* in the Visual Studio Code new project window and select your desired template.

:::image type="content" source="media/vscode-create-dotnet-aspire-proj.png" lightbox="media/vscode-create-dotnet-aspire-proj.png" alt-text="Visual Studio Code: .NET Aspire templates.":::

Select the desired location, enter a name, and select **Create**.

:::zone-end
:::zone pivot="dotnet-cli"

To create a .NET Aspire solution or project using the .NET CLI, use the [dotnet new](/dotnet/core/tools/dotnet-new) command and specify which template you would like to create. Consider the following examples:

To create a basic [.NET Aspire app host](app-host-overview.md) project targeting the latest .NET version:

```dotnetcli
dotnet new aspire-apphost
```

To create a .NET Aspire starter app, which is a full solution with a sample UI and backing API included:

```dotnetcli
dotnet new aspire-starter
```

> [!TIP]
> .NET Aspire templates default to using the latest .NET version, even when using an earlier version of the .NET CLI. To manually specify the .NET version, use the `--framework <tfm>` option, e.g. to create a basic [.NET Aspire app host](app-host-overview.md) project targeting .NET 8:
>
> ```dotnetcli
> dotnet new aspire-apphost --framework net8.0
> ```

You need to trust the ASP.NET Core :::no-loc text="localhost"::: certificate before running the app. Run the following command:

```dotnetcli
dotnet dev-certs https --trust
```

For more information, see [Troubleshoot untrusted localhost certificate in .NET Aspire](../troubleshooting/untrusted-localhost-certificate.md). For in-depth details about troubleshooting localhost certificates on Linux, see [ASP.NET Core: GitHub repository issue #32842](https://github.com/dotnet/aspnetcore/issues/32842).

:::zone-end

## See also

- [.NET Aspire SDK](dotnet-aspire-sdk.md)
- [.NET Aspire setup and tooling](setup-tooling.md)
- [Testing in .NET Aspire](testing.md)
