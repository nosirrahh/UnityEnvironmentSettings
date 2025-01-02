using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// ScriptableObject class that stores build settings and environment provider.
    /// Uses the creation menu to generate an instance of this object in the project.
    /// </summary>
    [CreateAssetMenu (fileName = "BuildSettings", menuName = "Packages/UnityEnvironmentSettings/BuildSettings", order = 1)]
    public class BuildSettingsScriptableObject : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Stores the build settings, including the environment provider.
        /// </summary>
        [SerializeField]
        private BuildSettings buildSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Property to access the build settings.
        /// </summary>
        public BuildSettings BuildSettings { get { return buildSettings; } }

        #endregion
    }
}