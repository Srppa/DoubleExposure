using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public InputActionReference buttonXAction;

    public GameObject Prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two))
        {
            // CORRECT
            GameObject obj = Instantiate(Prefab);
            obj.GetComponent<NetworkObject>().Spawn();

        }
    }
}
