using Nosirrahh.UnityEnvironmentSettings.Editor;
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.ResourcesSample.Editor
{
    /// <summary>
    /// Example class that implements the resource building process for demonstrating Unity Environment Settings.
    /// </summary>
    public class ResourcesSampleProcessBuilder : ProcessBuilder, IProcessBuilder
    {
        #region Fields

        /// <summary>
        /// Instance of the environment builder used during the build process.
        /// </summary>
        private IEnvironmentBuilder environmentBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Execution order of the callback in the build lifecycle.
        /// </summary>
        public int callbackOrder => 0;

        /// <summary>
        /// Path to the build settings file.
        /// </summary>
        public string BuildSettingsPath => $"Assets/UnityEnvironmentSettings/Samples/ResourcesSample/{nameof (BuildSettings)}.ResourcesSample.asset";

        /// <summary>
        /// Path to the environment settings file.
        /// </summary>
        public string EnvironmentSettingsPath => $"Assets/UnityEnvironmentSettings/Samples/ResourcesSample/Resources/{nameof (EnvironmentSettings)}.ResourcesSample.asset";

        /// <summary>
        /// Gets the environment builder to be used during the build process.
        /// </summary>
        public IEnvironmentBuilder EnvironmentBuilder => environmentBuilder ??= new ResourcesEnvironmentBuilder ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes before the build process begins.
        /// </summary>
        /// <param name="report">Build report containing details about the platform and output path.</param>
        public void OnPreprocessBuild (BuildReport report)
        {
            Debug.Log ($"OnPreprocessBuild for target {report.summary.platform} at path {report.summary.outputPath}");
            Debug.Log ($"BuildSettingsPath: {BuildSettingsPath}");
            Debug.Log ($"EnvironmentSettingsPath: {EnvironmentSettingsPath}");
            Debug.Log ($"EnvironmentBuilder: {EnvironmentBuilder.GetType ()}");
            PreprocessBuild (BuildSettingsPath, EnvironmentSettingsPath, EnvironmentBuilder);
        }

        /// <summary>
        /// Executes after the build process is completed.
        /// </summary>
        /// <param name="report">Build report containing details about the platform and output path.</param>
        public void OnPostprocessBuild (BuildReport report)
        {
            Debug.Log ($"OnPostprocessBuild for target {report.summary.platform} at path {report.summary.outputPath}");
            PostprocessBuild (EnvironmentBuilder);
        }

        #endregion
    }
}