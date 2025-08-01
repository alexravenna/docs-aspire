---
title: Explore .NET Aspire dashboard
description: Explore the .NET Aspire dashboard features through the .NET Aspire Starter app.
ms.date: 07/21/2025
ms.topic: reference
ai-usage: ai-assisted
---

# Explore the .NET Aspire dashboard

In the upcoming sections, you discover how to create a .NET Aspire project and embark on the following tasks:

> [!div class="checklist"]
>
> - Investigate the dashboard's capabilities by using the app generated from the project template as explained in the [Quickstart: Build your first .NET Aspire project.](../../get-started/build-your-first-aspire-app.md)
>
> - Delve into the features of the .NET Aspire dashboard app.

The screenshots featured in this article showcase the dark theme. For more information on theme selection, see [Theme selection](#theme-selection).

The .NET Aspire dashboard also includes **GitHub Copilot**, your AI debugging assistant, which can help you analyze resources, logs, traces, and telemetry data. Copilot is available when you launch your app from VS Code or Visual Studio with a GitHub account that has a Copilot subscription. For complete details, see [GitHub Copilot in the .NET Aspire dashboard](copilot.md).

## Dashboard authentication

When you run a .NET Aspire app host, the orchestrator starts up all the app's dependent resources and then opens a browser window to the dashboard. The .NET Aspire dashboard requires token-based authentication for its users because it displays environment variables and other sensitive information.

When the dashboard is launched from Visual Studio or Visual Studio Code (with the [C# Dev Kit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)), the browser is automatically logged in, and the dashboard opens directly. This is the typical developer <kbd>F5</kbd> experience, and the authentication login flow is automated by the .NET Aspire tooling.

However, if you start the app host from the command line, you're presented with the login page. The console window displays a URL that you can select on to open the dashboard in your browser.

:::image type="content" source="media/explore/dotnet-run-login-url.png" lightbox="media/explore/dotnet-run-login-url.png" alt-text=".NET CLI run command output, showing the login URL with token query string.":::

The URL contains a token query string (with the token value mapped to the `t` name part) that's used to _log in_ to the dashboard. If your console supports it, you can hold the <kbd>Ctrl</kbd> key and then select the link to open the dashboard in your browser. This method is easier than copying the token from the console and pasting it into the login page. If you end up on the dashboard login page without either of the previously described methods, you can always return to the console to copy the token.

:::image type="content" source="media/explore/aspire-login.png" lightbox="media/explore/aspire-login.png" alt-text=".NET Aspire dashboard login page.":::

The login page accepts a token and provides helpful instructions on how to obtain the token, as shown in the following screenshot:

:::image type="content" source="media/explore/aspire-login-help.png" lightbox="media/explore/aspire-login-help.png" alt-text=".NET Aspire dashboard login page with instructions on how to obtain the token.":::

After copying the token from the console and pasting it into the login page, select the **Log in** button.

:::image type="content" source="media/explore/aspire-login-filled.png" lightbox="media/explore/aspire-login-filled.png" alt-text=".NET Aspire dashboard login page with the token pasted into the textbox.":::

The dashboard persists the token as a browser persistent cookie, which remains valid for three days. Persistent cookies have an expiration date and remain valid even after closing the browser. This means that users don't need to log in again if they close and reopen the browser. For more information, see the [Security considerations for running the .NET Aspire dashboard](security-considerations.md) documentation.

> [!TIP]
> You can also obtain the login token from the container logs. For more information, see [Login to the dashboard](standalone.md#login-to-the-dashboard).

## Resources page

The **Resources** page is the default home page of the .NET Aspire dashboard. This page lists all of the .NET projects, containers, and executables included in your .NET Aspire solution. For example, the starter application includes two projects:

- **apiservice**: A back-end API project built using Minimal APIs.
- **webfrontend**: The front-end UI project built using Blazor.

The dashboard also provides essential details about each resource:

- **Type**: Displays whether the resource is a project, container, or executable.
- **Name**: The name of the resource.
- **State**: Displays whether or not the resource is currently running.
  - **Errors**: Within the **State** column, errors are displayed as a badge with the error count. It's useful to understand quickly what resources are reporting errors. Selecting the badge takes you to the [semantic logs](#structured-logs-page) for that resource with the filter at an error level.
- **Start time**: When the resource started running.
- **Source**: The location of the resource on the device.
- **Endpoints**: One or more URLs to reach the running resource directly.
- **Logs**: A link to the resource logs page.
- **Actions**: A [set of actions](#resource-actions) that can be performed on the resource:
  - **Stop / Start**: Stop (or Start) the resource—depending on the current **State**.
  - **Console logs**: Navigate to the resource's console logs.
  - **Ellipsis**: A submenu with extra resource specific actions:
    - **View details**: View the resource details.
    - **Console log**: Navigate to the resource's console logs.
    - **Structured logs**: Navigate to the resource's structured logs.
    - **Traces**: Navigate to the resource's traces.
    - **Metrics**: Navigate to the resource's metrics.
    - **Restart**: Stop and then start the resource.

Consider the following screenshot of the resources page:

:::image type="content" source="media/explore/projects.png" lightbox="media/explore/projects.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page.":::

Alternatively, you can view the resources as a graph. The graph view provides a visual representation of the resources and their dependencies. This view is useful for understanding how the different resources in your app are connected and how they interact with each other:

:::image type="content" source="media/explore/project-graphs.png" lightbox="media/explore/project-graphs.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page in graph view.":::

You can interact with the graph by selecting a resource to view its details. The overall view also supports zooming in and out.

### Resource actions

Each resource has a set of available actions that are conditionally enabled based on the resource's current state. For example, if a resource is running, the **Stop** action is enabled. If the resource is stopped, the **Start** action is enabled. Likewise, some actions are disabled when they're unavailable, for example, some resources don't have structured logs. In these situations, the **Structured logs** action is disabled.

#### Stop or Start a resource

The .NET Aspire dashboard allows you to stop or start a resource by selecting the **Stop** or **Start** button in the **Actions** column. Consider the following screenshot of the resources page with the **Stop** button selected:

:::image type="content" source="media/explore/resource-stop-action.png" lightbox="media/explore/resource-stop-action.png" alt-text=".NET Aspire dashboard stop resource.":::

When you select **Stop**, the resource stops running, and the **State** column updates to reflect the change.

> [!NOTE]
> For project resources, when the debugger is attached, it's reattached on restart.

The **Start** button is then enabled, allowing you to start the resource again. Additionally, the dashboard displays a toast notification of the result of the action:

:::image type="content" source="media/explore/resource-stopped-action.png" lightbox="media/explore/resource-stopped-action.png" alt-text=".NET Aspire dashboard resource stopped.":::

When a resource is in a non-running state, the **Start** button is enabled. Selecting **Start** starts the resource, and the **State** column updates to reflect the change. The **Stop** button is then enabled, allowing you to stop the resource again. The dashboard displays a toast notification of the result of the action:

:::image type="content" source="media/explore/resource-started-action.png" lightbox="media/explore/resource-started-action.png" alt-text=".NET Aspire dashboard started resource.":::

> [!TIP]
> Resources that depend on other resources that are stopped, or restarted, might experience temporary errors. This is expected behavior and is typically resolved when the dependent resources are in a **Running** state once again.

##### Resource submenu actions

Selecting the horizontal ellipsis icon in the **Actions** column opens a submenu with additional resource-specific actions. In addition to the built-in resource submenu actions, you can also define custom resource actions by defining custom commands. For more information, see [Custom resource commands in .NET Aspire](../custom-resource-commands.md). For the built-in resource submenu actions, consider the following screenshot:

:::image type="content" source="media/explore/resource-actions.png" lightbox="media/explore/resource-actions.png" alt-text=".NET Aspire dashboard resource submenu actions.":::

The following submenu actions are available:

- **View details**: View the resource details.
- **Console log**: Navigate to the resource's console logs.
- **Structured logs**: Navigate to the resource's structured logs.
- **Traces**: Navigate to the resource's traces.
- **Metrics**: Navigate to the resource's metrics.
- **Restart**: Stop and then start the resource.

> [!IMPORTANT]
> There might be resources with disabled submenu actions. They're greyed out when they're disabled. For example, the following screenshot shows the submenu actions disabled:
>
> :::image type="content" source="media/explore/resource-submenu-actions.png" lightbox="media/explore/resource-submenu-actions.png" border="true" alt-text=".NET Aspire dashboard disabled submenu actions.":::

When GitHub Copilot is available (when launching from VS Code or Visual Studio with a Copilot subscription), resource context menus also include an **Ask GitHub Copilot** option. This allows you to investigate resources using AI analysis.

#### Copy or Open in text visualizer

To view a _text visualizer_ of certain columns, on hover you see a vertical ellipsis icon. Select the icon to display the available options:

- **Copy to clipboard**
- **Open in text visualizer**

Consider the following screenshot of the ellipsis menu options:

:::image type="content" source="media/explore/text-visualizer-selection-menu.png" lightbox="media/explore/text-visualizer-selection-menu.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page, showing the ellipsis menu options.":::

When you select the **Open in text visualizer** option, a modal dialog opens with the text displayed in a larger format. Consider the following screenshot of the text visualizer modal dialog:

:::image type="content" source="media/explore/text-visualizer-resources.png" lightbox="media/explore/text-visualizer-resources.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page, showing the text visualizer.":::

Some values are formatted as JSON or XML. In these cases, the text visualizer enables the **Select format** dropdown to switch between the different formats.

### Resource details

You can obtain full details about each resource by selecting the ellipsis button in the **Actions** column and then selecting **View details**. The **Details** page provides a comprehensive view of the resource:

:::image type="content" source="media/explore/resource-details-thumb.png" lightbox="media/explore/resource-details.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page with the details of a selected resource displayed.":::

The search bar in the upper right of the dashboard also provides the option to filter the list, which is useful for .NET Aspire projects with many resources. To select the types of resources that are displayed, drop down the arrow to the left of the filter textbox:

:::image type="content" source="media/explore/select-resource-type.png" alt-text="A screenshot of the resource type selector list in the .NET Aspire dashboard Resources page.":::

In this example, only containers are displayed in the list. For example, if you enable **Use Redis for caching** when creating a .NET Aspire project, you should see a Redis container listed:

:::image type="content" source="media/explore/resources-filtered-containers.png" lightbox="media/explore/resources-filtered-containers.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page filtered to show only containers.":::

Executables are stand-alone processes. You can configure a .NET Aspire project to run a stand-alone executable during startup, though the default starter templates don't include any executables by default.

The following screenshot shows an example of a project that has errors:

:::image type="content" source="media/explore/projects-errors.png" lightbox="media/explore/projects-errors.png" alt-text="A screenshot of the .NET Aspire dashboard Resources page, showing a project with errors.":::

Selecting the error count badge navigates to the [Structured logs](#structured-logs-page) page with a filter applied to show only the logs relevant to the resource:

:::image type="content" source="media/explore/structured-logs-errors.png" lightbox="media/explore/structured-logs-errors.png" alt-text="A screenshot of the .NET Aspire dashboard Structured logs page, showing a filter applied to show only the logs relevant to the resource.":::

To see the log entry in detail for the error, select the **View** button to open a window below the list with the structured log entry details:

:::image type="content" source="media/explore/structured-logs-errors-view-thumb.png" lightbox="media/explore/structured-logs-errors-view.png" alt-text="A screenshot of the .NET Aspire dashboard Structured logs page, showing a lower window with the structured log entry details.":::

For more information and examples of Structured logs, see the [Structured logs page](#structured-logs-page) section.

> [!NOTE]
> The resources page isn't available if the dashboard is started without a configured resource service. The dashboard starts on the [Structured logs page](#structured-logs-page) instead. This is the default experience when the dashboard is run in standalone mode without additional configuration.
>
> For more information about configuring a resource service, see [Dashboard configuration](configuration.md).

## Monitoring pages

The .NET Aspire dashboard provides various ways to view logs, traces, and metrics for your app. This information enables you to track the behavior and performance of your app and to diagnose any issues that arise.

### Console logs page

The **Console logs** page displays text that each resource in your app has sent to standard output. Logs are a useful way to monitor the health of your app and diagnose issues. Logs are displayed differently depending on the source, such as project, container, or executable.

When you open the Console logs page, you must select a source in the **Select a resource** drop-down list.

If you select a project, the live logs are rendered with a stylized set of colors that correspond to the severity of the log; green for information as an example. Consider the following example screenshot of project logs with the `apiservice` project selected:

:::image type="content" source="media/explore/project-logs.png" lightbox="media/explore/project-logs.png" alt-text="A screenshot of the .NET Aspire dashboard Console Logs page with a source selected.":::

When errors occur, they're styled in the logs such that they're easy to identify. Consider the following example screenshot of project logs with errors:

:::image type="content" source="media/explore/project-logs-error.png" lightbox="media/explore/project-logs-error.png" alt-text="A screenshot of the .NET Aspire dashboard Console Logs page, showing logs with errors.":::

If you select a container or executable, formatting is different from a project but verbose behavior information is still available. Consider the following example screenshot of a container log with the `cache` container selected:

:::image type="content" source="media/explore/container-logs.png" lightbox="media/explore/container-logs.png" alt-text="A screenshot of the .NET Aspire dashboard Console logs page with a container source selected.":::

You can download any console log to your local machine and use your preferred text programs to analyze it:

:::image type="content" source="media/explore/download-console-logs.png" lightbox="media/explore/download-console-logs.png" alt-text="A screenshot of the .NET Aspire dashboard Console logs page showing how to download a log to your local machine.":::

#### Resource replicas

When project resources are replicated using the <xref:Aspire.Hosting.ProjectResourceBuilderExtensions.WithReplicas%2A> API, they're represented in the resource selector under a top-level named resource entry with an icon to indicator. Each replicated resource is listed under the top-level resource entry, with its corresponding unique name. Consider the following example screenshot of a replicated project resource:

:::image type="content" source="media/explore/console-logs-with-replicas.png" lightbox="media/explore/console-logs-with-replicas.png" alt-text=".NET Aspire dashboard: Console logs page resource selector with nested replica resources.":::

The preceding screenshot shows the `catalogservice (application)` project with two replicas, `catalogservice-2bpj2qdq6k` and `catalogservice-6ljdin0hc0`. Each replica has its own set of logs that can be viewed by selecting the replica name.

### Structured logs page

.NET Aspire automatically configures your projects with logging using OpenTelemetry. Navigate to the **Structured logs** page to view the semantic logs for your .NET Aspire project. [Semantic, or structured logging](https://github.com/NLog/NLog/wiki/How-to-use-structured-logging) makes it easier to store and query log-events, as the log-event message-template and message-parameters are preserved, instead of just transforming them into a formatted message. You notice a clean structure for the different logs displayed on the page using columns:

- **Resource**: The resource the log originated from.
- **Level**: The log level of the entry, such as information, warning, or error.
- **Timestamp**: The time that the log occurred.
- **Message**: The details of the log.
- **Trace**: A link to the relevant trace for the log, if applicable.
- **Details**: Additional details or metadata about the log entry.

Consider the following example screenshot of semantic logs:

:::image type="content" source="media/explore/structured-logs.png" lightbox="media/explore/structured-logs.png" alt-text="A screenshot of the .NET Aspire dashboard Semantic logs page.":::

#### Filter structured logs

The structured logs page also provides a search bar to filter the logs by service, level, or message. You use the **Level** drop down to filter by log level. You can also filter by any log property by selecting the filter icon button, which opens the advanced filter dialog.

Consider the following screenshots showing the structured logs filter dialog:

:::image type="content" source="media/explore/structured-logs-filtered.png" lightbox="media/explore/structured-logs-filtered.png" alt-text="A screenshot of the .NET Aspire dashboard Structured logs page showing the filter dialog.":::

When GitHub Copilot is available and your app has structured logs with errors, an **Explain errors** button appears on the structured logs page. Selecting it makes all errors available to Copilot for AI-powered investigation. Additionally, structured log entries include an **Ask GitHub Copilot** option in their context menus.

### Traces page

Navigate to the **Traces** page to view all of the traces for your app. .NET Aspire automatically configures tracing for the different projects in your app. Distributed tracing is a diagnostic technique that helps engineers localize failures and performance issues within applications, especially those that might be distributed across multiple machines or processes. For more information, see [.NET distributed tracing](/dotnet/core/diagnostics/distributed-tracing). This technique tracks requests through an application and correlates work done by different application integrations. Traces also help identify how long different stages of the request took to complete. The traces page displays the following information:

- **Timestamp**: When the trace completed.
- **Name**: The name of the trace, prefixed with the project name.
- **Spans**: The resources involved in the request.
- **Duration**: The time it took to complete the request. This column includes a radial icon that illustrates the duration of the request in comparison with the others in the list.

:::image type="content" source="media/explore/traces.png" lightbox="media/explore/traces.png" alt-text="A screenshot of the .NET Aspire dashboard Traces page.":::

#### Filter traces

The traces page also provides a search bar to filter the traces by name or span. Apply a filter, and notice the trace results are updated immediately. Consider the following screenshot of traces with a filter applied to `weather` and notice how the search term is highlighted in the results:

:::image type="content" source="media/explore/trace-view-filter.png" lightbox="media/explore/trace-view-filter.png" alt-text="A screenshot of the .NET Aspire dashboard Traces page, showing a filter applied to show only traces with the term 'weather'.":::

When filtering traces in the **Add filter** dialog, after selecting a **Parameter** and corresponding **Condition**, the **Value** selection is pre-populated with the available values for the selected parameter. Consider the following screenshot of the **Add filter** dialog with the `http.route` parameter selected:

:::image type="content" source="media/explore/traces-filtering.png" lightbox="media/explore/traces-filtering.png" alt-text="A screenshot of the .NET Aspire dashboard Traces page, showing the Add filter dialog with the http.route parameter selected.":::

#### Combine telemetry from multiple resources

When a resource has multiple replicas, you can filter telemetry to view data from all instances at once. Select the parent resource, labeled `(application)`, as shown in the following screenshot:

:::image type="content" source="media/explore/telemetry-resource-filter.png" lightbox="media/explore/telemetry-resource-filter.png" alt-text="Filter by all instances of a resource":::

After selecting the parent resource, the traces page displays telemetry from all instances of the resource.

#### Trace details

The trace details page contains various details pertinent to the request, including:

- **Trace Detail**: When the trace started.
- **Duration**: The time it took to complete the request.
- **Resources**: The number of resources involved in the request.
- **Depth**: The number of layers involved in the request.
- **Total Spans**: The total number of spans involved in the request.

Each span is represented as a row in the table, and contains a **Name**. Spans also display the error icon if an error occurred within that particular span of the trace. Spans that have a type of client/consumer, but don't have a span on the server, show an arrow icon and then the destination address. This represents a client call to a system outside of the .NET Aspire project. For example, an HTTP request an external web API, or a database call.

Within the trace details page, there's a **View Logs** button that takes you to the structured logs page with a filter applied to show only the logs relevant to the request. Consider an example screenshot depicting the structured logs page with a filter applied to show only the logs relevant to the trace:

:::image type="content" source="media/explore/structured-logs-trace-errors.png" lightbox="media/explore/structured-logs-trace-errors.png" alt-text="A screenshot of the .NET Aspire dashboard Structured logs page, showing a filter applied to show only the logs relevant to the trace.":::

When GitHub Copilot is available, the trace details page also includes an **Explain trace** button that provides a quick way to analyze the currently viewed trace using AI. Additionally, traces and individual spans include **Ask GitHub Copilot** options in their context menus for detailed investigation.

The structured logs page is discussed in more detail in the [Structured logs page](#structured-logs-page) section.

#### Trace examples

Each trace has a color, which is generated to help differentiate between spans—one color for each resource. The colors are reflected in both the _traces page_ and the _trace detail page_. When traces depict an arrow icon, those icons are colorized as well to match the span of the target trace. Consider the following example screenshot of traces:

:::image type="content" source="media/explore/traces.png" lightbox="media/explore/traces.png" alt-text="A screenshot of the .NET Aspire dashboard Traces page.":::

In the **Actions** column, you can select **View details** to navigate to a detailed view of the request and the duration of time it spent traveling through each application layer. Consider an example selection of a trace to view its details:

:::image type="content" source="media/explore/trace.png" lightbox="media/explore/trace.png" alt-text="A screenshot of the .NET Aspire dashboard Trace details page.":::

For each span in the trace, select **View details** to see more details:

:::image type="content" source="media/explore/trace-span-details.png" lightbox="media/explore/trace-span-details.png" alt-text="A screenshot of the .NET Aspire dashboard Trace details page with the details of a span displayed.":::

Scroll down in the span details pain to see full information. At the bottom of the span details pane, some span types, such as this call to a cache, show span event timings:

:::image type="content" source="media/explore/trace-span-event-details.png" lightbox="media/explore/trace-span-event-details.png" alt-text="A screenshot of the .NET Aspire dashboard Trace details page with the event timings for a span displayed.":::

For complex traces with many spans, use the **Filter** textbox to display only matching spans:

:::image type="content" source="media/explore/filter-spans-trace-details.png" lightbox="media/explore/filter-spans-trace-details.png" alt-text="A screenshot of the .NET Aspire dashboard Trace details page with the filter used to display only weather spans.":::

When errors are present, the page renders an error icon next to the trace name. Consider an example screenshot of traces with errors:

:::image type="content" source="media/explore/traces-errors.png" lightbox="media/explore/traces-errors.png" alt-text="A screenshot of the .NET Aspire dashboard Traces page, showing traces with errors.":::

And the corresponding detailed view of the trace with errors:

:::image type="content" source="media/explore/trace-view-errors.png" lightbox="media/explore/trace-view-errors.png" alt-text="A screenshot of the .NET Aspire dashboard Trace details page, showing a trace with errors.":::

### Metrics page

Navigate to the **Metrics** page to view the metrics for your app. .NET Aspire automatically configures metrics for the different projects in your app. Metrics are a way to measure the health of your application and can be used to monitor the performance of your app over time.

Each metric-publishing project in your app has its own metrics. The metrics page displays a selection pane for each top-level meter and the corresponding instruments that you can select to view the metric.

Consider the following example screenshot of the metrics page, with the `webfrontend` project selected and the `System.Net.Http` meter's `http.client.request.duration` metric selected:

:::image type="content" source="media/explore/metrics-view.png" lightbox="media/explore/metrics-view.png" alt-text="A screenshot of the .NET Aspire dashboard Metrics page.":::

In addition to the metrics chart, the metrics page includes an option to view the data as a table instead. Consider the following screenshot of the metrics page with the table view selected:

:::image type="content" source="media/explore/metrics-table-view.png" lightbox="media/explore/metrics-table-view.png" alt-text="A screenshot of the .NET Aspire dashboard Metrics page with the table view selected.":::

Under the chart, there's a list of filters you can apply to focus on the data that interests you. For example, in the following screenshot, the **http.request.method** field is filtered to show only **GET** requests:

:::image type="content" source="media/explore/metrics-view-filtered.png" lightbox="media/explore/metrics-view-filtered.png" alt-text="A screenshot of the .NET Aspire dashboard Metrics page with a filter applied to the chart.":::

You can also choose to select the count of the displayed metric on the vertical access, instead of its values:

:::image type="content" source="media/explore/metrics-view-count.png" lightbox="media/explore/metrics-view-count.png" alt-text="A screenshot of the .NET Aspire dashboard Metrics page with the count option applied.":::

For more information about metrics, see [Built-in Metrics in .NET](/dotnet/core/diagnostics/built-in-metrics).

### Exemplars

The .NET Aspire dashboard supports and displays OpenTelemetry _Exemplars_. An _exemplar_ links a metric data point to the operation that recorded it, serving as a bridge between metrics and traces.

Exemplars are useful because they provide additional context about why a specific metric value was recorded. For example, if you notice a spike in latency in the `http.client.request.duration` metric, an exemplar could point to a specific trace or span that caused the spike, helping you understand the root cause.

Exemplars are displayed in the metrics chart as a small round dot next to the data point. When you hover over the indicator, a tooltip displays the exemplar details as shown in the following screenshot:

:::image type="content" source="media/explore/metrics-page-exemplars.png" lightbox="media/explore/metrics-page-exemplars.png" alt-text=".NET Aspire Dashboard: Metrics Page, with exemplar indicator hover details.":::

The preceding screenshot shows the exemplar details for the `http.client.request.duration` metric. The exemplar details include the:

- Resource name.
- Operation performed, in this case an HTTP GET to the `/catalog/images/{id}`.
- Corresponding value and the time stamp.

Selecting the exemplar indicator opens the trace details page, where you can view the trace associated, for example consider the following screenshot:

:::image type="content" source="media/explore/trace-page-from-exemplars.png" lightbox="media/explore/trace-page-from-exemplars.png" alt-text=".NET Aspire Dashboard: Trace Page, navigated to from the corresponding Metrics Page exemplar.":::

For more information, see [OpenTelemetry Docs: Exemplars](https://opentelemetry.io/docs/specs/otel/metrics/data-model/#exemplars).

### Interact with telemetry

Telemetry information is generated continuously while you run a .NET Aspire application and it can flood the dashboard with information. The dashboard includes tools you can use to reduce the amount of data displayed in monitoring pages and make it easier to target specific events.

#### Pause telemetry output

In the **Console logs**, **Structured logs**, **Traces**, and **Metrics** pages, you can pause the collection of telemetry data:

:::image type="content" source="media/explore/pause-telemetry.png" lightbox="media/explore/pause-telemetry.png" alt-text="A screenshot of the .NET Aspire dashboard Console logs page, showing how to pause telemetry.":::

The pause button affects only the type of telemetry displayed on that page. So, for example, if you pause console logs collection, your app continues to collect structured logs, traces, and metrics telemetry.

#### Remove data

Next to the **Pause** button, the **Remove data** button enables you to clear the telemetry on the current page. Drop down the list, and choose whether to remove the telemetry for all resource, or only for the current resource:

:::image type="content" source="media/explore/remove-telemetry-data-thumb.png" lightbox="media/explore/remove-telemetry-data.png" alt-text="A screenshot of the .NET Aspire dashboard Structured logs page, showing how to remove telemetry.":::

The button is available for control logs, structured logs, traces, and metrics. It works independently for each type of data.

If you want to remove all telemetry of all types and for all resources in a single step, use the **Remove all** button in the **Settings** dialog:

:::image type="content" source="media/explore/remove-all-telemetry.png" lightbox="media/explore/remove-all-telemetry.png" alt-text="A screenshot of the .NET Aspire dashboard Settings page, showing how to remove all telemetry.":::

## Settings dialog

<span id="theme-selection"></span>

The .NET Aspire dashboard provides a settings dialog that allows you to configure the theme, the language, functionality to clear all the logs and telemetry, and the .NET version used by the dashboard.

By default, the theme is set to follow the System theme, which means the dashboard uses the same theme as your operating system. You can also select the **Light** or **Dark** theme to override the system theme. Theme selections are persisted.

The following screenshot shows the theme selection dialog, with the default System theme selected:

:::image type="content" source="media/explore/theme-selection.png" lightbox="media/explore/theme-selection.png" alt-text="The .NET Aspire dashboard Settings dialog, showing the System theme default selection.":::

If you prefer the Light theme, you can select it from the theme selection dialog:

:::image type="content" source="media/explore/theme-selection-light.png" lightbox="media/explore/theme-selection-light.png" alt-text="The .NET Aspire dashboard Settings dialog, showing the Light theme selection.":::

## Dashboard shortcuts

The .NET Aspire dashboard provides various shortcuts to _help_ you navigate and control different parts of the dashboard. To display the keyboard shortcuts, press <kbd>Shift</kbd> + <kbd>?</kbd>, or select the question mark icon in the top-right corner of the dashboard:

:::image type="content" source="media/explore/dashboard-help.png" lightbox="media/explore/dashboard-help.png" alt-text=".NET Aspire dashboard Help modal dialog.":::

The following shortcuts are available:

**Panels**:

- <kbd>+</kbd>: Increase panel size.
- <kbd>-</kbd>: Decrease panel size.
- <kbd>Shift</kbd> + <kbd>r</kbd>: <u>R</u>eset panel size.
- <kbd>Shift</kbd> + <kbd>t</kbd>: <u>T</u>oggle panel orientation.
- <kbd>Shift</kbd> + <kbd>x</kbd>: Close panel.

**Page navigation**:

- <kbd>r</kbd>: Go to **<u>R</u>esources**.
- <kbd>c</kbd>: Go to **<u>C</u>onsole Logs**.
- <kbd>s</kbd>: Go to **<u>S</u>tructured Logs**.
- <kbd>t</kbd>: Go to **<u>T</u>races**.
- <kbd>m</kbd>: Go to **<u>M</u>etrics**.

**Site-wide navigation**:

- <kbd>?</kbd>: Got to **Help**.
- <kbd>Shift</kbd> + <kbd>s</kbd>: Go to **<u>S</u>ettings**.

## Interaction prompts

Some resources or commands might prompt you for values when using the dashboard. This interactive functionality is powered by the [interaction service](../../extensibility/interaction-service.md), which allows integrations to display notifications or to request input from users when needed.

For example, Azure resources that are missing required configuration might prompt you for configuration values when the dashboard starts or when you interact with those resources. These prompts help ensure that resources are properly configured and can function correctly within your .NET Aspire application.

In the dashboard, interaction prompts appear as:

- Input dialogs for missing configuration values.
- Confirmation dialogs for important actions.
- Notification messages with details about resource status.

These prompts appear directly in the dashboard interface, making it easy to provide the necessary information without switching to external tools or configuration files.

For detailed information about using the interaction service API, including examples and CLI support, see [Interaction Service](../../extensibility/interaction-service.md).

## GitHub Copilot in the dashboard

The .NET Aspire dashboard includes GitHub Copilot as your AI debugging assistant when you launch your app from VS Code or Visual Studio with a GitHub account that has a Copilot subscription. Copilot can help you:

- Review hundreds of log messages with a single click.
- Investigate the root cause of errors across multiple apps.
- Highlight performance issues in traces.
- Explain obscure error codes using AI's vast knowledge repository.

Copilot features appear throughout the dashboard interface:

- **Copilot button**: This button is available in the top-right corner to open the Copilot chat interface.
- **Context menus**: "Ask GitHub Copilot" options appear in resource, log, trace, and span context menus.
- **Explain buttons**: "Explain errors" buttons appear on structured logs and "Explain trace" buttons appear on trace details pages.
- **Suggested questions**: Contextual AI suggestions appear based on the current page.

For comprehensive information about GitHub Copilot features, requirements, troubleshooting, and configuration options, see [GitHub Copilot in the .NET Aspire dashboard](copilot.md).

## Next steps

> [!div class="nextstepaction"]
> [Standalone .NET Aspire dashboard](standalone.md)
