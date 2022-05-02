using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool IsDialog;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

    void Update()
    {
        if (!IsDialog)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.Find("Main Camera").localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
    }
    
}
