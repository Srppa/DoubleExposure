using System.Collections;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DemoMenu : MonoBehaviour
{

    public GameObject AnimalReference;

    [SerializeField] private Button SharkButton;

    [SerializeField] private Button ShowButton;

    [SerializeField] private Button HideButton;

    [SerializeField] private GameObject SharkPrefab;

    [SerializeField] private GameObject RatPrefab;

    [SerializeField] private GameObject SpiderPrefab;

    [SerializeField] private GameObject AnimalSelectionParent;

    [SerializeField] private GameObject AnimalControlParent;

    [SerializeField] private GameObject ConsoleParent;

    [SerializeField] private TMP_Text Console;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private float distanceFromCamera = 2f;

    [SerializeField] private Vector3 offset = Vector3.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SharkButton.onClick.AddListener(OnSharkButtonClicked);

        ShowButton.onClick.AddListener(OnShowClicked);

        HideButton.onClick.AddListener(OnHideClicked);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Transform cam = playerCamera.transform;
        Vector3 temp = cam.forward * distanceFromCamera;
        transform.position = cam.position + cam.forward * distanceFromCamera + offset;
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    void OnSharkButtonClicked()
    {
        if (AnimalReference == null)
        {
            GameObject obj = Instantiate(SharkPrefab);
            AnimalReference = obj;
            obj.GetComponent<NetworkObject>().Spawn();


            AnimalSelectionParent.SetActive(false);
            AnimalControlParent.SetActive(true);
        }
    }

    void OnShowClicked()
    {
        if(AnimalReference != null)
        {
            AnimalReference.GetComponent<AnimalController>().Show();
        }
    }

    void OnHideClicked()
    {
        if (AnimalReference != null)
        {
            AnimalReference.GetComponent<AnimalController>().Hide();
        }
    }

    public void ReciveInfo(string info)
    {
        StartCoroutine(RunCodes(info));
    }

    private IEnumerator RunCodes(string info)
    {
        if (info == "roomCreated")
        {
            yield return new WaitForSeconds(1);
            Console.text += "\nNo other room found; Creating a new lobby";
            yield return new WaitForSeconds(1);
            Console.text += "\nRole set as therapeut";
            yield return new WaitForSeconds(1);
            Console.text += "\nLoading menu";
            yield return new WaitForSeconds(2);
            ConsoleParent.SetActive(false);
            AnimalSelectionParent.SetActive(true);
        }
        else if (info == "roomFound")
        {
            Console.text += "\nAvailable lobby found; Joining in";
            yield return new WaitForSeconds(1);
            Console.text += "\nRole set as pacient";
            yield return new WaitForSeconds(1);
            Console.text += "\nHiding menu...";
            yield return new WaitForSeconds(2);
            //Destroy(gameObject);
        }
        else if(info == "colocationReady")
        {
            Console.text += "\nColololo";
        }
        else
        {
            Console.text += "\n" + info;
        }



    }
}
