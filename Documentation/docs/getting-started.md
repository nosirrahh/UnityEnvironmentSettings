# Getting Started

## Prerequisites
Before using UnityEnvironmentSettings, ensure you have:
- **Unity Editor**: Version 2021.3 or later.
- **Basic C# Knowledge**: Understanding of Unity's scripting workflow.
- **TextMeshPro**: Installed via Unity Package Manager (if using samples).

## Installation
### Option 1: Clone the Repository
1. Clone the repository from GitHub:
   ```bash
   git clone https://github.com/nosirrahh/UnityEnvironmentSettings.git
2. Open the project in Unity.
### Option 2: Unity Package Manager
1. Open your Unity project.
2. Add the package via the Package Manager:
    URL: https://github.com/nosirrahh/UnityEnvironmentSettings.git?path=Assets/UnityEnvironmentSettings/package.json

## Configuration
### Step 1: Create Environment Settings
1. Navigate to Assets > Create > Nosirrahh > EnvironmentSettings.
2. Configure the environment and key-value pairs as needed.
### Step 2: Create a Loader
Use the built-in ResourcesEnvironmentLoader:
* Place the settings file in the Resources folder.
* Name the file according to your environment, e.g., EnvironmentSettings.Development.asset.
### Step 3: Initialize the Loader
Add the following script to your project:
```csharp
IEnvironmentLoader loader = new ResourcesEnvironmentLoader();
loader.Init(new Dictionary<string, string> {
    { ResourcesEnvironmentLoader.ResourcesPathKey, "EnvironmentSettings.Development" }
});
```

## Example Usage
Here’s how to load and access settings:
```csharp
EnvironmentSettingsManager.Load(
    loader,
    (EnvironmentSettings settings) => {
        Debug.Log($"Environment: {settings.Environment}");
        foreach (var kv in settings.Settings)
            Debug.Log($"Key: {kv.key}, Value: {kv.value}");
    }
);
```