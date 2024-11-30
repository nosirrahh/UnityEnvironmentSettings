using Nosirrahh.UnityEnvironmentSettings.Runtime;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Responsible for creating and managing the storage of an <see cref="EnvironmentSettingsScriptableObject"/> 
    /// object in the `Resources` directory within the Unity project.
    /// </summary>
    public class ResourcesEnvironmentBuilder : IEnvironmentBuilder
    {
        #region Fields

        /// <summary>
        /// Path where the environment asset will be saved after being built.
        /// </summary>
        private string assetPath;

        #endregion

        #region Properties

        /// <summary>
        /// Default path for the environment settings asset within the `Resources` folder.
        /// </summary>
        public string DefaultPath { get { return $"Assets/Resources/{nameof (EnvironmentSettings)}.asset"; } }

        #endregion

        #region IEnvironmentBuilder Methods

        /// <summary>
        /// Builds the environment settings asset and saves it to the specified path.
        /// </summary>
        /// <param name="settings">Environment settings to be saved in the asset.</param>
        /// <param name="path">Optional path for the asset. If not specified, <see cref="DefaultPath"/> is used.</param>
        /// <returns>Returns <c>true</c> if the asset is successfully built; otherwise, <c>false</c>.</returns>
        public bool Build (EnvironmentSettings settings, string path = null)
        {
            try
            {
                EnvironmentSettingsScriptableObject environmentSettingsAsset = ScriptableObject.CreateInstance<EnvironmentSettingsScriptableObject> ();

                typeof (EnvironmentSettingsScriptableObject)
                    .GetField ("environmentSettings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue (environmentSettingsAsset, settings);

                assetPath = string.IsNullOrEmpty (path) ? DefaultPath : path;

                if (!BuildPath (assetPath))
                    return false;

                AssetDatabase.CreateAsset (environmentSettingsAsset, assetPath);
                AssetDatabase.SaveAssets ();
                AssetDatabase.Refresh ();

                return true;
            }
            catch (System.Exception exception)
            {
                Debug.LogWarning ($"[{nameof (ResourcesEnvironmentBuilder)}] {exception}");
                return false;
            }
        }

        /// <summary>
        /// Removes the created asset for the environment settings.
        /// </summary>
        public void Destroy ()
        {
            if (!string.IsNullOrEmpty (assetPath))
                AssetDatabase.DeleteAsset (assetPath);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates and creates the directory structure for the asset at the specified path.
        /// </summary>
        /// <param name="assetPath">Full path for the asset, including file name and extension.</param>
        /// <returns>Returns <c>true</c> if the path is valid and created successfully; otherwise, <c>false</c>.</returns>
        private bool BuildPath (string assetPath)
        {
            try
            {
                if (!assetPath.StartsWith ("Assets/"))
                {
                    Debug.LogError ($"[{nameof (ResourcesEnvironmentBuilder)}] O caminho precisa começar uma pasta 'Assets'.");
                    return false;
                }

                if (!assetPath.Contains ("Resources/"))
                {
                    Debug.LogError ($"[{nameof (ResourcesEnvironmentBuilder)}] O caminho precisa conter uma pasta 'Resources'.");
                    return false;
                }

                if (!assetPath.EndsWith (".asset"))
                {
                    Debug.LogError ($"[{nameof (ResourcesEnvironmentBuilder)}] O caminho precisa terminar com um arquivo '.asset'.");
                    return false;
                }

                string directoryPath = Path.GetDirectoryName (assetPath).Replace ("\\", "/");
                string[] folders = directoryPath.Split ('/');
                string parentFolder = string.Empty;
                string currentPath = string.Empty;

                for (int i = 0; i < folders.Length; i++)
                {
                    currentPath = i == 0 ? folders[i] : $"{currentPath}/{folders[i]}";

                    if (!AssetDatabase.IsValidFolder (currentPath))
                        AssetDatabase.CreateFolder (parentFolder, folders[i]);

                    parentFolder = currentPath;
                }

                AssetDatabase.Refresh ();
                return true;

            }
            catch (System.Exception exception)
            {
                Debug.LogWarning ($"[{nameof (ResourcesEnvironmentBuilder)}] {exception}");
                return false;
            }
        }

        #endregion
    }
}