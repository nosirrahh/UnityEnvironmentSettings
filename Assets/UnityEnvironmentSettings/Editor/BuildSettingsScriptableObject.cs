using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    [CreateAssetMenu (fileName = "BuildSettings", menuName = "Nosirrahh/BuildSettings", order = 1)]
    public class BuildSettingsScriptableObject : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        [SerializeField]
        private BuildSettings buildSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public BuildSettings BuildSettings { get { return buildSettings; } }

        #endregion
    }
}