using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Em construção.
    /// </summary>
    [CreateAssetMenu (fileName = "EnvironmentSettings", menuName = "Nosirrahh/EnvironmentSettings", order = 1)]
    public class EnvironmentSettingsScriptableObject : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        [SerializeField]
        private EnvironmentSettings environmentSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public EnvironmentSettings EnvironmentSettings { get { return environmentSettings; } }

        #endregion
    }
}
