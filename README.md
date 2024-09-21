# Game Design Document (GDD) Maker

## Overview
The **GDD Maker** is a WPF-based desktop application that allows users to create, edit, and manage Game Design Documents (GDDs) in various formats including JSON, TXT, and CSV. The application provides an intuitive interface for filling in key sections of a GDD, such as the game title, genre, target audience, description, key features, and more. 

The GDD Maker helps game designers organize their thoughts and ideas in a structured manner, and supports exporting the document into different formats for easy sharing and presentation.

## Features
- **Create and Edit GDD**: Allows users to fill out fields for game title, genre, target audience, core mechanics, and more.
- **Property Binding**: Automatically detects changes to the GDD properties and marks the document as needing to be saved.
- **Save and Load GDD**: Users can save their work to JSON, TXT, or CSV files, and load existing files back into the application.
- **Export Options**: Export GDDs into JSON, TXT, or CSV formats with the ability to customize file output.
- **Simple UI**: An easy-to-use interface for managing GDD data, powered by WPF and bound to the `GameDesignDocument` class.
- **File Import and Export**: Supports importing and exporting in the following formats:
  - **JSON** (supported for both reading and writing)
  - **TXT** (write only)
  - **CSV** (write only)

## Screenshots
*Include screenshots of your application if available, especially the form where users fill in the GDD data.*

## Getting Started

### Prerequisites
- **.NET 7.0 SDK or later** (Make sure the .NET SDK is installed on your system).
- **NuGet Packages**:
  - Newtonsoft.Json (`Install-Package Newtonsoft.Json`)

### Running the Application

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/gdd-maker.git
   cd gdd-maker
   ```

### Key Sections Explained:
- **Overview**: Describes what the application is and what it's used for.
- **Features**: Lists the main features of the application.
- **Getting Started**: Provides instructions on how to set up and run the project, including the required dependencies and how to restore NuGet packages.
- **File Operations**: Explains how users can save, load, and export the GDD files in various formats.
- **Project Structure**: Gives a brief overview of the main files (`GameDesignDocument.cs` and `MainWindow.xaml.cs`) and their purpose in the application.
