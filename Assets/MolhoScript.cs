using UnityEngine;

public class MolhoScript : MonoBehaviour
{
    private Vector3 initialPos;
    private float pressCD;
    private float pressTime;

    public void Start()
    {
        initialPos = transform.position;
    }
    public void Update()
    {
        pressTime += Time.deltaTime * Mathf.Lerp(1, 5, pressCD);
        transform.position = initialPos + new Vector3(0,Mathf.Sin(pressTime),0);


        if (Input.GetMouseButton(0))
        {
            pressCD += Time.deltaTime;
        }
        else
        {
            pressCD -= Time.deltaTime/2;
        }
        pressCD = Mathf.Clamp01(pressCD);

    }
}
