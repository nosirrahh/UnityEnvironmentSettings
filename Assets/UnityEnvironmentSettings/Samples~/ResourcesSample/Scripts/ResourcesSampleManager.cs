using Nosirrahh.UnityEnvironmentSettings.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.ResourcesSample
{
    /// <summary>
    /// Sample manager demonstrating the use of environment settings loaded as resources in Unity.
    /// </summary>
    public class ResourcesSampleManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Reference to a TextMeshProUGUI component used to display the environment settings.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI settingsText;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Called at the start of the MonoBehaviour's lifecycle.
        /// Initializes the environment resources loader and loads the environment settings.
        /// </summary>
        private void Start ()
        {
            ResourcesEnvironmentLoader loader = new ResourcesEnvironmentLoader ();
            loader.Init (new Dictionary<string, string> () {
                {
                    ResourcesEnvironmentLoader.ResourcesPathKey,
                    Application.isEditor ? $"{nameof(EnvironmentSettings)}.Editor.ResourcesSample" : $"{nameof(EnvironmentSettings)}.ResourcesSample"
                }
            });

            EnvironmentSettingsManager.Load (
                loader,
                (EnvironmentSettings settings) =>
                {
                    SetEnvironmentSettingsText ();
                }
            );
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the text displayed in the TextMeshProUGUI component with the loaded environment settings.
        /// </summary>
        private void SetEnvironmentSettingsText ()
        {
            List<KeyValue> settings = EnvironmentSettingsManager.EnvironmentSettings.Settings;

            if (settings == null || settings.Count == 0)
            {
                settingsText.text = "Settings is null or empty.";
                return;
            }

            string content = $"Environment: {EnvironmentSettingsManager.EnvironmentSettings.Environment}\nSettings:\n";
            for (int i = 0; i < settings.Count; i++)
                content += $"Key: {settings[i].key}, Value: {settings[i].value}\n";
            settingsText.text = content;
        }

        #endregion
    }
}
