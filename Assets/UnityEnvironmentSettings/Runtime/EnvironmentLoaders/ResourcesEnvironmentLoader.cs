using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public class ResourcesEnvironmentLoader : IEnvironmentLoader
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        private string resourcesPath = Application.isEditor ? $"{nameof (EnvironmentSettings)}.Editor" : $"{nameof(EnvironmentSettings)}";

        /// <summary>
        /// Em construção.
        /// </summary>
        public const string ResourcesPathKey = "resourcesPath";

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public string ResourcesPath { get { return resourcesPath; } }

        #endregion

        #region IEnvironmentLoader Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="settings">Em construção.
        /// <para>resourcesPath</para>
        /// </param>
        public void Init (Dictionary<string, string> settings)
        {
            if (settings == null || settings.Count == 0)
                return;

            if (settings.TryGetValue (ResourcesPathKey, out string resourcesPath))
                this.resourcesPath = string.IsNullOrEmpty (resourcesPath) ? this.resourcesPath : resourcesPath;
        }

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
