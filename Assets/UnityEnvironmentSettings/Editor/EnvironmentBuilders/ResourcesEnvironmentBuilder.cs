#if UNITY_EDITOR
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public class ResourcesEnvironmentBuilder : IEnvironmentBuilder
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
        public string DefaultPath { get { return $"Assets/Resources/{nameof (EnvironmentSettings)}.asset"; } }

        #endregion

        #region IEnvironmentBuilder Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="settings">Em construção.</param>
        /// <param name="path">Em construção.</param>
        /// <returns>Em construção.</returns>
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
        /// Em construção.
        /// </summary>
        public void Destroy ()
        {
            if (!string.IsNullOrEmpty (assetPath))
                AssetDatabase.DeleteAsset (assetPath);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="assetPath">Em construção.</param>
        /// <returns>Em construção.</returns>
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
#endif