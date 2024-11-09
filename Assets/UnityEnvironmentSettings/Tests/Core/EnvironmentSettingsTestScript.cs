using Nosirrahh.UnityEnvironmentSettings.Runtime;
using NUnit.Framework;
using System;

namespace Nosirrahh.UnityEnvironmentSettings.Tests
{
    public class EnvironmentSettingsTestScript
    {
        #region Test Methods

        [Test]
        public void NewEnvironmentSettingsTest ()
        {
            string environment = $"{DateTime.Now}";
            EnvironmentSettings environmentSettings = new EnvironmentSettings (environment);
            Assert.AreEqual (environment, environmentSettings.Environment);
            Assert.IsNull (environmentSettings.Settings, "Foi encontrado valor para as configurações iniciais.");
        }

        [Test]
        public void AddValueTest ()
        {
            EnvironmentSettings environmentSettings = new EnvironmentSettings ("testing");
            KeyValue keyValueToBeAdded = new KeyValue ($"MyKey_{DateTime.Now}", $"MyValue_{DateTime.Now}");
            environmentSettings.AddValue (keyValueToBeAdded.key, keyValueToBeAdded.value);

            KeyValue keyValueFound = environmentSettings.Settings.Find (
                (KeyValue kv) => kv.key == keyValueToBeAdded.key && kv.value == keyValueToBeAdded.value
            );

            Assert.AreEqual (
                keyValueToBeAdded.key,
                keyValueFound.key,
                $"A chave - '{keyValueFound.key}', encontrada não corresponde a chave esperada - {keyValueToBeAdded.key}."
            );

            Assert.AreEqual (
                keyValueToBeAdded.value,
                keyValueFound.value,
                $"O valor - '{keyValueFound.value}', encontrado não corresponde ao valor esperado - {keyValueToBeAdded.value}."
            );
        }

        [Test]
        public void TryGetValueTest ()
        {
            EnvironmentSettings environmentSettings = new EnvironmentSettings ("testing");
            KeyValue keyValueToBeAdded = new KeyValue ($"MyKey_{DateTime.Now}", $"MyValue_{DateTime.Now}");
            environmentSettings.AddValue (keyValueToBeAdded.key, keyValueToBeAdded.value);

            bool itFound = environmentSettings.TryGetValue (keyValueToBeAdded.key, out string value);

            Assert.IsTrue (itFound, $"Não foi possível encontrar um valor para a chave '{keyValueToBeAdded.key}'.");
            Assert.AreEqual (
                keyValueToBeAdded.value,
                value,
                $"O valor - '{value}', encontrado para a chave '{keyValueToBeAdded.key}', não corresponde ao valor esperado - {keyValueToBeAdded.value}."
            );
        }

        #endregion
    }
}