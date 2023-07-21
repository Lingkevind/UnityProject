using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class moveSet : MonoBehaviour
{
    float distance=2.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveLeft() {
        transform.position = transform.position + (Vector3.left * distance);
    }

    public void moveRight()
    {
        transform.position = transform.position + (Vector3.right * distance);
    }

    public void moveUp()
    {
        transform.position = transform.position + (Vector3.up * distance);
    }

    public void moveDown()
    {
        transform.position = transform.position + (Vector3.down * distance);
    }
}
