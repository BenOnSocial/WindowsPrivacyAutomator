# Windows Privacy Automator

A high-performance desktop utility built with **WinUI 3** and **.NET 10** to automate the hardening of Windows 11 privacy and diagnostic settings. This project demonstrates modern C# architecture, asynchronous UI automation, and the Page Object Model (POM) design pattern.

## Technical Highlights

- **Modern Stack:** Leverages **WinUI 3 (Windows App SDK)** for a native Windows 11 Fluent Design interface.
- **Reliable Automation:** Utilizes **FlaUI (UIA3)** for programmatic interaction with the Windows UI Automation (UIA) tree, prioritizing **AutomationIDs** and **UIA Patterns** over brittle coordinate-based clicks.
- **MVVM Architecture:** Implemented using the **CommunityToolkit.Mvvm**, utilizing Source Generators for boilerplate-free property notification and command binding.
- **Multi-Threaded Execution:** Automation logic runs on a background thread via `Task.Run` to prevent UI hang, with thread-safe logging marshaled back to the UI thread via `DispatcherQueue`.
- **Defensive Programming:** Implements **Smart Retries** and **Anchor Elements** to handle the asynchronous rendering and UI virtualization common in modern UWP/WinUI applications.

## Tech Stack

- **Language:** C# 12 / .NET 10
- **UI Framework:** WinUI 3 (Unpackaged / Self-Contained)
- **Automation Framework:** FlaUI.UIA3
- **Design Pattern:** MVVM (Model-View-ViewModel) + Page Object Model (POM)

## Project Structure

- **`/Services`**: Contains the Page Objects and automation infrastructure.
- **`/ViewModels`**: Manages application state, command logic, and UI-thread marshaling.
- **`/Models`**: Defines structured data types for execution logging and status reporting.
- **`/Views`**: XAML definitions and code-behind for the Fluent UI.

## Getting Started

### Prerequisites

- Windows 11 (Standard display scaling at 100% recommended for GUI automation).
- No external runtimes required (Self-contained build).

### Running the App

1. Download the latest release `.exe`.
2. Launch `WindowsPrivacyAutomator.exe`.
   - _Note: As a self-contained single-file app, the first launch may take several seconds to extract the runtime to a temporary directory. Subsequent launches are near-instant._
3. Click **Run Optimization** and observe the real-time execution log.

## Architectural Decisions

- **Why FlaUI?** FlaUI provides a managed wrapper around the UIA COM interfaces, offering better performance and more intuitive access to **Invoke**, **Toggle**, and **Selection** patterns compared to legacy libraries.
- **Why Unpackaged?** The app is deployed as an unpackaged, self-contained executable to ensure "zero-dependency" execution on target workstations without requiring MSIX sideloading or the Windows App SDK runtime to be pre-installed.
- **Programmatic Toggling:** Whenever possible, the tool uses the **Toggle Pattern** (`iface_toggle.Toggle()`) rather than simulated mouse clicks to ensure 100% reliability regardless of window focus or screen resolution.
