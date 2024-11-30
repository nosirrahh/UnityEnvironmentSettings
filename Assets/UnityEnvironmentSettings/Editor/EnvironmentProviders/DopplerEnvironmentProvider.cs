using Newtonsoft.Json.Linq;
using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Provides environment settings using the Doppler API.
    /// This class retrieves environment secret configurations from the Doppler service.
    /// For more information, refer to the Doppler API documentation:
    /// <see href="https://docs.doppler.com/reference/secrets-list"/>.
    /// </summary>
    [System.Serializable]
    public class DopplerEnvironmentProvider : IEnvironmentProvider
    {
        #region Fields

        /// <summary>
        /// The personal authentication token required to access the Doppler API.
        /// </summary>
        [SerializeField]
        private string personalToken;
        /// <summary>
        /// The name of the Doppler project from which settings will be retrieved.
        /// </summary>
        [SerializeField]
        private string project;
        /// <summary>
        /// The configuration name within the Doppler project.
        /// </summary>
        [SerializeField]
        private string config;

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves environment settings from the Doppler service and populates the <paramref name="environmentSettings"/> object.
        /// The settings are fetched using <see cref="personalToken"/>, <see cref="project"/>, and <see cref="config"/>.
        /// </summary>
        /// <param name="environmentSettings">The object to be populated with settings retrieved from Doppler.</param>
        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings)
        {
            try
            {
                environmentSettings = new EnvironmentSettings (config);

                UnityWebRequest request = UnityWebRequest.Get ($"https://api.doppler.com/v3/configs/config/secrets?project={project}&config={config}");
                request.SetRequestHeader ("Authorization", $"Bearer {personalToken}");
                request.SendWebRequest ();
                while (!request.isDone)
                    Debug.Log ($"{nameof (DopplerEnvironmentProvider)} Buscando configurações do Doppler...");

                if (request.result != UnityWebRequest.Result.Success)
                    throw new System.Exception ($"{nameof (DopplerEnvironmentProvider)} respondeCode: {request.responseCode}, result: {request.result}, error: {request.error}");

                JObject jObject = JObject.Parse (request.downloadHandler.text);
                if (jObject.TryGetValue ("secrets", out JToken secrets))
                {
                    foreach (JProperty secret in secrets.Children<JProperty> ())
                        environmentSettings.AddValue (secret.Name, secret.Value["computed"]?.ToString ());
                }
            }
            catch (System.Exception exception)
            {
                throw exception;
            }
        }

        #endregion
    }
}