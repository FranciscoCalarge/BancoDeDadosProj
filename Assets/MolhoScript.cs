using UnityEngine;

public class MolhoScript : MonoBehaviour
{
    private Vector3 initialPos;

    public void Start()
    {
        initialPos = transform.position;
    }
    public void Update()
    {
        Vector3 auxVec3 = transform.right * Mathf.Sin(Time.time * 2) + transform.forward * Mathf.Cos(Time.time * 2);
        transform.position =Vector3.Lerp( transform.position,initialPos+ auxVec3*.2f, .2f);
    }
}
