using Nosirrahh.UnityEnvironmentSettings.Runtime;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Nosirrahh.UnityEnvironmentSettings.Tests
{
    public class ResourcesEnvironmentLoaderTestScript
    {
        #region Tests Methods

        [Test]
        public void InitTest ()
        {
            ResourcesEnvironmentLoader loader = new ResourcesEnvironmentLoader ();
            string defaultResourcesPath = loader.ResourcesPath;
            loader.Init (default);
            Assert.AreEqual (defaultResourcesPath, loader.ResourcesPath);
        }

        [Test]
        public void InitWithSettingsTest ()
        {
            ResourcesEnvironmentLoader loader = new ResourcesEnvironmentLoader ();
            string resourcesPath = "CustomFolder/EnvironmentSettings";
            Dictionary<string, string> settings = new Dictionary<string, string> ()
            {
                { $"{nameof(resourcesPath)}", resourcesPath }
            };
            loader.Init (settings);
            Assert.AreEqual (resourcesPath, loader.ResourcesPath);
        }

        [UnityTest]
        public IEnumerator LoadTest ()
        {
            EnvironmentSettings environmentSettings = new EnvironmentSettings ($"{DateTime.Now}");
            environmentSettings.AddValue (
                $"MyKey_1_{System.DateTime.Now}",
                $"MyValue_1_{System.DateTime.Now}"
            );
            environmentSettings.AddValue (
                $"MyKey_2_{System.DateTime.Now}",
                $"MyValue_2_{System.DateTime.Now}"
            );

            ResourcesEnvironmentLoader loader = new ResourcesEnvironmentLoader ();

            CreateTestsFolder (loader.ResourcesPath, out string testFolderPath, out string assetFullPath, out string resourcesPath);
            CreateEnvironmentSettingsAsset (assetFullPath, environmentSettings);

            Dictionary<string, string> loaderSettings = new Dictionary<string, string> ()
            {
                { $"{nameof(resourcesPath)}", resourcesPath }
            };
            loader.Init (loaderSettings);

            EnvironmentSettings loadedEnvironmentSettings = null;

            bool isLoading = true;
            loader.Load ((EnvironmentSettings settings) =>
            {
                loadedEnvironmentSettings = settings;
                isLoading = false;
            });

            yield return new WaitWhile (() => isLoading);

            Assert.NotNull (loadedEnvironmentSettings);
            Assert.AreEqual (environmentSettings.Environment, loadedEnvironmentSettings.Environment);
            Assert.AreEqual (environmentSettings.Settings.Count, loadedEnvironmentSettings.Settings.Count);
            for (int i = 0; i < loadedEnvironmentSettings.Settings.Count; i++)
            {
                Assert.AreEqual (
                    environmentSettings.Settings[i].key,
                    loadedEnvironmentSettings.Settings[i].key
                );
                Assert.AreEqual (
                    environmentSettings.Settings[i].value,
                    loadedEnvironmentSettings.Settings[i].value
                );
            }

            DeleteTestsFolder (testFolderPath);
        }

        #endregion

        #region Private Methods

        private void CreateTestsFolder (string assetPath, out string testFolderPath, out string assetFullPath, out string resourcesPath)
        {
            testFolderPath = "Assets/TESTS_TEMP_FOLDER";
            assetFullPath = $"{testFolderPath}/Resources/Temp/{assetPath}.asset";
            resourcesPath = $"Temp/{assetPath}";
            AssetDatabase.CreateFolder ("Assets", "TESTS_TEMP_FOLDER");
            AssetDatabase.CreateFolder (testFolderPath, "Resources");
            AssetDatabase.CreateFolder ($"{testFolderPath}/Resources", "Temp");
        }

        private void CreateEnvironmentSettingsAsset (string assetPath, EnvironmentSettings settings)
        {
            // Cria uma instância do ScriptableObject
            EnvironmentSettingsScriptableObject environmentSettingsAsset = ScriptableObject.CreateInstance<EnvironmentSettingsScriptableObject> ();
            // Atribui o objeto criado ao campo environmentSettings do ScriptableObject
            typeof (EnvironmentSettingsScriptableObject)
                .GetField ("environmentSettings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue (environmentSettingsAsset, settings);

            // Cria o asset no projeto
            AssetDatabase.CreateAsset (environmentSettingsAsset, $"{assetPath}");
            // Salva as alterações no banco de dados do Unity
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }

        private void DeleteTestsFolder (string path)
        {
            AssetDatabase.DeleteAsset (path);
        }

        #endregion
    }
}