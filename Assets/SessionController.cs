using UnityEngine;

public class SessionController : MonoBehaviour
{
    public Role SessionRole;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SessionRole = Role.Undecided;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTherapistRole()
    {
        SessionRole = Role.Therapist;
    }

    public void setPatientRole()
    {
        SessionRole = Role.Patient;
    }
}

public enum Role
{
    Therapist,
    Patient,
    Undecided
}