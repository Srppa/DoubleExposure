using Unity.Netcode;
using UnityEngine;

public class AnimalController : NetworkBehaviour
{

    public SessionController sessionControllerReference;

    [SerializeField] private GameObject SharkVisuals;


    public override void OnNetworkSpawn()
    {
       
    }


    // Call this from server to run on ALL clients
    public void Show()
    {
        if (!IsServer) return;
        ShowClientRpc();
    }


    [ClientRpc]
    private void ShowClientRpc()
    {
        if (!IsServer)
        {
            Debug.Log("Executed on all clients!");
            SharkVisuals.SetActive(true);
        }
            // This runs on every connected client
            
    }


    // Call this from server to run on ALL clients
    public void Hide()
    {
        if (!IsServer) return;
        HideClientRpc();
    }


    [ClientRpc]
    private void HideClientRpc()
    {
        if(!IsServer)
        {
            // This runs on every connected client
            Debug.Log("Executed on all clients!");
            SharkVisuals.SetActive(false);
        }
        
    }

}
