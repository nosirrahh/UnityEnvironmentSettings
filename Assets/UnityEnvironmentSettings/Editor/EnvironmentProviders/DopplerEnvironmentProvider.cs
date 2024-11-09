using Nosirrahh.UnityEnvironmentSettings.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    [System.Serializable]
    public class DopplerEnvironmentProvider : IEnvironmentProvider
    {
        // Doppler: dp.pt.Pd9NcNs1CGLnbQ5w3imxqvx5mRzrCacdZepm01WOQx2
        [SerializeField]
        private string personalToken;

        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings)
        {
            // TODO: UTILIZAR O PERSONAL TOKEN PARA BUSCAR AS CONFIGURAÇÕES DO DOPPLER
            throw new System.NotImplementedException ();
        }
    }
}