using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Loads environment settings from Unity's Resources folder.
    /// </summary>
    public class ResourcesEnvironmentLoader : IEnvironmentLoader
    {
        #region Consts

        /// <summary>
        /// Key used to specify the path to the resources file in the settings dictionary.
        /// </summary>
        public const string ResourcesPathKey = "resourcesPath";

        #endregion

        #region Fields

        /// <summary>
        /// Path to the resources file containing environment settings.
        /// Defaults to "EnvironmentSettings.Editor" in the editor, or "EnvironmentSettings" in runtime.
        /// </summary>
        private string resourcesPath = Application.isEditor ? $"{nameof (EnvironmentSettings)}.Editor" : $"{nameof(EnvironmentSettings)}";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current path to the resources file.
        /// </summary>
        public string ResourcesPath { get { return resourcesPath; } }

        #endregion

        #region IEnvironmentLoader Methods

        /// <summary>
        /// Initializes the loader with a dictionary of settings.
        /// If a path is provided under the key <see cref="ResourcesPathKey"/>, it overrides the default path.
        /// </summary>
        /// <param name="settings">
        /// A dictionary containing initial settings. 
        /// Expected to include a "resourcesPath" key for custom resource paths.
        /// </param>
        public void Init (Dictionary<string, string> settings)
        {
            if (settings == null || settings.Count == 0)
                return;

            if (settings.TryGetValue (ResourcesPathKey, out string resourcesPath))
                this.resourcesPath = string.IsNullOrEmpty (resourcesPath) ? this.resourcesPath : resourcesPath;
        }

        /// <summary>
        /// Asynchronously loads the environment settings from Unity's Resources folder.
        /// If the loading fails, it invokes the callback with a new empty <see cref="EnvironmentSettings"/> instance.
        /// </summary>
        /// <param name="onCompleted">
        /// A callback action invoked upon completion of the loading operation.
        /// Receives the loaded <see cref="EnvironmentSettings"/> or <c>null</c> if loading fails.
        /// </param>
        public void Load (UnityAction<EnvironmentSettings> onCompleted)
        {
            try
            {
                ResourceRequest request = Resources.LoadAsync<EnvironmentSettingsScriptableObject> (resourcesPath);
                request.completed += (AsyncOperation asyncOperation) =>
                {
                    EnvironmentSettingsScriptableObject asset = request.asset as EnvironmentSettingsScriptableObject;
                    EnvironmentSettings environmentSettings = asset == null ? new EnvironmentSettings (string.Empty) : asset.EnvironmentSettings;
                    onCompleted?.Invoke (environmentSettings);
                };
            }
            catch (Exception exception)
            {
                Debug.LogError ($"[{nameof (ResourcesEnvironmentLoader)}] exception: {exception}");
                onCompleted?.Invoke (null);
            }
        }

        #endregion
    }
}
