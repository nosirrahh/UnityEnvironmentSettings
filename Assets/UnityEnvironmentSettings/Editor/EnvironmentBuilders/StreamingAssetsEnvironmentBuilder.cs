using Newtonsoft.Json;
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using System.IO;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Responsible for creating and managing the storage of an <see cref="EnvironmentSettings"/> 
    /// object in JSON format within the Unity project's `StreamingAssets` folder.
    /// </summary>
    public class StreamingAssetsEnvironmentBuilder : IEnvironmentBuilder
    {
        #region Fields

        /// <summary>
        /// Path where the environment asset will be saved after being built.
        /// </summary>
        private string assetPath;

        #endregion

        #region Properties

        /// <summary>
        /// Default path for the environment settings asset within the `StreamingAssets` folder.
        /// </summary>
        public string DefaultPath { get { return $"{Application.streamingAssetsPath}/{nameof (EnvironmentSettings)}.json"; } }

        #endregion

        /// <summary>
        /// Builds the JSON file with the environment settings and saves it to the specified path.
        /// </summary>
        /// <param name="settings">Environment settings to be serialized and saved to the file.</param>
        /// <param name="path">Optional path for the asset. If not specified, <see cref="DefaultPath"/> is used.</param>
        /// <returns>Returns <c>true</c> if the asset is successfully built; otherwise, <c>false</c>.</returns>
        public bool Build (EnvironmentSettings settings, string path = null)
        {
            assetPath = string.IsNullOrEmpty (path) ? DefaultPath : path;

            if (!BuildPath (assetPath))
                return false;

            string content = JsonConvert.SerializeObject (settings);
            
            File.WriteAllText (assetPath, content);
            return File.Exists (assetPath);
        }

        /// <summary>
        /// Removes the created JSON file for the environment settings.
        /// </summary>
        public void Destroy ()
        {
            if (File.Exists (assetPath))
                File.Delete (assetPath);
        }

        #region Private Methods

        /// <summary>
        /// Validates and creates the required directory structure for the JSON file at the specified path.
        /// </summary>
        /// <param name="assetPath">Full path for the JSON file, including file name and extension.</param>
        /// <returns>Returns <c>true</c> if the path is valid and created successfully; otherwise, <c>false</c>.</returns>
        private bool BuildPath (string assetPath)
        {
            // TODO: IMPLEMENTAR
            throw new System.NotImplementedException ();
        }

        #endregion
    }
}