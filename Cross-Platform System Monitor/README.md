# Cross Platform System Monitor

## Overview

A C# console application that monitors system resources and supports a plugin-based architecture for extending functionality.

## Features

* CPU Usage Monitoring
* RAM Usage Monitoring
* Disk Usage Monitoring
* Real-time Console Output
* File Logging Plugin
* REST API Plugin
* Dependency Injection
* Configurable Settings via appsettings.json

## Architecture

The application follows a clean architecture approach:

* Core

  * Interfaces
  * Models

* Infrastructure

  * Monitoring Providers
  * Plugins

* Services

  * Monitoring Orchestration

Platform-specific monitoring logic is abstracted behind the `IPlatformMetricsProvider` interface.

## Configuration

appsettings.json

{
"Monitoring": {
"IntervalSeconds": 5
},
"ApiSettings": {
"Endpoint": "https://httpbin.org/post"
}
}

## How to Run

1. Open the solution in Visual Studio 2022.
2. Restore NuGet packages.
3. Build the solution.
4. Run using Ctrl + F5.

## Sample Plugins

* FileLoggerPlugin
* ApiPlugin

## Design Decisions

* Used Dependency Injection to reduce coupling.
* Used Plugin Architecture for extensibility.
* Abstracted monitoring functionality through interfaces.
* Configuration is externalized using appsettings.json.

## Limitations

* Current monitoring implementation targets Windows.
* Linux and macOS providers can be added in the future by implementing IPlatformMetricsProvider.
