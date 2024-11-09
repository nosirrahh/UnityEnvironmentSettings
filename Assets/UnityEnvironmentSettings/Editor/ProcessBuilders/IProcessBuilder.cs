using UnityEditor.Build;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public interface IProcessBuilder : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        /// <summary>
        /// Em construção.
        /// </summary>
        public string BuildSettingsPath { get; }
        /// <summary>
        /// Em construção.
        /// </summary>
        public string EnvironmentSettingsPath { get; }
        /// <summary>
        /// Em construção.
        /// </summary>
        public IEnvironmentBuilder EnvironmentBuilder { get; }
    }
}