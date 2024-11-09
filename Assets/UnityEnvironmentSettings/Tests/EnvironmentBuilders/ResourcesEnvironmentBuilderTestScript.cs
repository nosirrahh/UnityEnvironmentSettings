using Nosirrahh.UnityEnvironmentSettings.Editor;
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Nosirrahh.UnityEnvironmentSettings.Tests
{
    public class ResourcesEnvironmentBuilderTestScript
    {
        #region Tests Methods

        [Test]
        public void BuildTest ()
        {
            EnvironmentSettings settings = new EnvironmentSettings ("testing");
            settings.AddValue ($"MyKey_1_{DateTime.Now}", $"MyValue_1_{DateTime.Now}");
            settings.AddValue ($"MyKey_2_{DateTime.Now}", $"MyValue_2_{DateTime.Now}");

            ResourcesEnvironmentBuilder builder = new ResourcesEnvironmentBuilder ();

            EnvironmentSettingsScriptableObject asset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (builder.DefaultPath);
            if (asset != null)
                Assert.Inconclusive ($"Já existe um asset criado no caminho '{builder.DefaultPath}'.");

            List<string> assetsToDelete = new List<string> () { builder.DefaultPath };
            assetsToDelete.AddRange (GetUncreatedFolders (builder.DefaultPath));
            
            bool builded = builder.Build (settings);

            try
            {
                Assert.IsTrue (builded);

                EnvironmentSettingsScriptableObject createdAsset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (builder.DefaultPath);

                Assert.IsNotNull (createdAsset);
                Assert.IsNotNull (createdAsset.EnvironmentSettings);
                Assert.AreEqual (settings.Environment, createdAsset.EnvironmentSettings.Environment);
                Assert.AreEqual (settings.Settings.Count, createdAsset.EnvironmentSettings.Settings.Count);
                for (int i = 0; i < settings.Settings.Count; i++)
                    Assert.AreEqual (settings.Settings[i], createdAsset.EnvironmentSettings.Settings[i]);
            }
            finally
            {
                DeleteAssets (assetsToDelete);
            }
        }

        [Test]
        public void BuildWithPathTest ()
        {
            EnvironmentSettings settings = new EnvironmentSettings ("testing");
            settings.AddValue ($"MyKey_1_{DateTime.Now}", $"MyValue_1_{DateTime.Now}");
            settings.AddValue ($"MyKey_2_{DateTime.Now}", $"MyValue_2_{DateTime.Now}");
            string path = "Assets/TEST_FOLDER/Resources/MyCustomEnvironmentSettings.asset";

            ResourcesEnvironmentBuilder builder = new ResourcesEnvironmentBuilder ();
            
            EnvironmentSettingsScriptableObject asset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (path);
            if (asset != null)
                Assert.Inconclusive ($"Já existe um asset criado no caminho '{path}'.");

            List<string> assetsToDelete = new List<string> () { path };
            assetsToDelete.AddRange (GetUncreatedFolders (path));

            bool builded = builder.Build (settings, path);

            try
            {
                Assert.IsTrue (builded);

                EnvironmentSettingsScriptableObject createdAsset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (path);

                Assert.IsNotNull (createdAsset);
                Assert.IsNotNull (createdAsset.EnvironmentSettings);
                Assert.AreEqual (settings.Environment, createdAsset.EnvironmentSettings.Environment);
                Assert.AreEqual (settings.Settings.Count, createdAsset.EnvironmentSettings.Settings.Count);
                for (int i = 0; i < settings.Settings.Count; i++)
                    Assert.AreEqual (settings.Settings[i], createdAsset.EnvironmentSettings.Settings[i]);
            }
            finally
            {
                DeleteAssets (assetsToDelete);
            }
        }

        [Test]
        public void DestroyTest ()
        {
            EnvironmentSettings settings = new EnvironmentSettings ("testing");
            settings.AddValue ($"MyKey_1_{DateTime.Now}", $"MyValue_1_{DateTime.Now}");
            settings.AddValue ($"MyKey_2_{DateTime.Now}", $"MyValue_2_{DateTime.Now}");

            ResourcesEnvironmentBuilder builder = new ResourcesEnvironmentBuilder ();

            EnvironmentSettingsScriptableObject asset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (builder.DefaultPath);
            if (asset != null)
                Assert.Inconclusive ($"Já existe um asset criado no caminho '{builder.DefaultPath}'.");

            List<string> assetsToDelete = new List<string> () { builder.DefaultPath };
            assetsToDelete.AddRange (GetUncreatedFolders (builder.DefaultPath));

            bool builded = builder.Build (settings);

            try
            {
                EnvironmentSettingsScriptableObject createdAsset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (builder.DefaultPath);
                Assert.IsNotNull (createdAsset);
                builder.Destroy ();
                EnvironmentSettingsScriptableObject destroyedAsset = AssetDatabase.LoadAssetAtPath<EnvironmentSettingsScriptableObject> (builder.DefaultPath);
                Assert.IsNull (destroyedAsset);

            }
            finally
            {
                DeleteAssets (assetsToDelete);
            }
        }

        #endregion

        #region Private Methods

        private void DeleteAssets (List<string> assets)
        {
            for (int i = 0; i < assets.Count; i++)
                AssetDatabase.DeleteAsset (assets[i]);
        }

        private List<string> GetUncreatedFolders (string assetPath)
        {
            List<string> uncreatedFolders = new List<string> ();

            string directoryPath = Path.GetDirectoryName (assetPath).Replace ("\\", "/");
            string[] folders = directoryPath.Split ('/');
            string parentFolder = string.Empty;
            string currentPath = string.Empty;

            for (int i = 0; i < folders.Length; i++)
            {
                currentPath = i == 0 ? folders[i] : $"{currentPath}/{folders[i]}";
                if (!AssetDatabase.IsValidFolder (currentPath))
                    uncreatedFolders.Add ($"{parentFolder}/{folders[i]}");
                parentFolder = currentPath;
            }

            return uncreatedFolders;
        }

        #endregion
    }
}