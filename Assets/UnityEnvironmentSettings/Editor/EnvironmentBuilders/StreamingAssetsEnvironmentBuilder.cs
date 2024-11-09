using Newtonsoft.Json;
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using System.IO;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    public class StreamingAssetsEnvironmentBuilder : IEnvironmentBuilder
    {
        #region Fields

        /// <summary>
        /// Em construção.
        /// </summary>
        private string assetPath;

        #endregion

        #region Properties

        /// <summary>
        /// Em construção.
        /// </summary>
        public string DefaultPath { get { return $"{Application.streamingAssetsPath}/{nameof (EnvironmentSettings)}.asset"; } }

        #endregion

        public bool Build (EnvironmentSettings settings, string path = null)
        {
            assetPath = string.IsNullOrEmpty (path) ? DefaultPath : path;

            if (!BuildPath (assetPath))
                return false;

            string content = JsonConvert.SerializeObject (settings);
            Debug.LogWarning ($"assetPath: {assetPath}, content: {content}");

            File.WriteAllText (assetPath, content);
            return File.Exists (assetPath);
        }

        public void Destroy ()
        {
            if (File.Exists (assetPath))
                File.Delete (assetPath);
        }

        #region Private Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="assetPath">Em construção.</param>
        /// <returns>Em construção.</returns>
        private bool BuildPath (string assetPath)
        {
            return true;
        }

        #endregion
    }
}