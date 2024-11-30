using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEditor;
using UnityEditor.Build;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Class responsible for performing pre-build and post-build processing,
    /// managing both build and environment configurations.
    /// </summary>
    public class ProcessBuilder
    {
        #region Public Methods

        /// <summary>
        /// Executes the pre-build process, loading and applying build and environment configurations.
        /// </summary>
        /// <param name="buildSettingsPath">Path to the build settings file.</param>
        /// <param name="environmentSettingsPath">Path to the environment settings file.</param>
        /// <param name="environmentBuilder">Object responsible for environment construction.</param>
        /// <exception cref="BuildFailedException">Throws an exception if the environment construction fails.</exception>
        public virtual void PreprocessBuild (string buildSettingsPath, string environmentSettingsPath, IEnvironmentBuilder environmentBuilder)
        {
            try
            {
                BuildSettingsScriptableObject buildSettings = AssetDatabase.LoadAssetAtPath<BuildSettingsScriptableObject> (
                    buildSettingsPath
                );
                IEnvironmentProvider provider = buildSettings.BuildSettings.GetProvider ();
                provider.GetEnvironmentSettings (out EnvironmentSettings environmentSettings);
                if (!environmentBuilder.Build (environmentSettings, environmentSettingsPath))
                    throw new System.Exception ($"{nameof(ProcessBuilder)} Não foi possível realizar a construção do environment.");
            }
            catch (System.Exception exception)
            {
                throw new BuildFailedException (exception);
            }
        }

        /// <summary>
        /// Executes the post-build process, destroying the configured environment.
        /// </summary>
        /// <param name="environmentBuilder">Object responsible for constructing and destroying the environment.</param>
        /// <exception cref="System.Exception">Throws an exception if an error occurs while destroying the environment.</exception>
        public virtual void PostprocessBuild (IEnvironmentBuilder environmentBuilder)
        {
            try
            {
                environmentBuilder.Destroy ();
            }
            catch (System.Exception exception)
            {
                throw exception;
            }
        }

        #endregion
    }
}