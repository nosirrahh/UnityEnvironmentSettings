using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEditor;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public class ProcessBuilder
    {
        #region Public Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="buildSettingsPath">Em construção.</param>
        /// <param name="environmentSettingsPath">Em construção.</param>
        /// <param name="environmentBuilder">Em construção.</param>
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
                    throw new System.Exception ("Não foi possível realizar a construção do environment.");
            }
            catch (System.Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="environmentBuilder">Em construção.</param>
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