using UnityEditor.Build;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Interface for build processors that perform actions before and after the build process,
    /// including configuring environment variables and specific build settings.
    /// Implements the <see cref="IPreprocessBuildWithReport"/> and <see cref="IPostprocessBuildWithReport"/> 
    /// interfaces to allow actions to be executed before and after the build.
    /// </summary>
    public interface IProcessBuilder : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        /// <summary>
        /// Path to the build settings that will be applied during the build process.
        /// </summary>
        public string BuildSettingsPath { get; }
        /// <summary>
        /// Path to the environment settings that will be used during the build process.
        /// </summary>
        public string EnvironmentSettingsPath { get; }
        /// <summary>
        /// Object responsible for configuring the environment at build time.
        /// Implementation of <see cref="IEnvironmentBuilder"/> used to define environment variables.
        /// </summary>
        public IEnvironmentBuilder EnvironmentBuilder { get; }
    }
}