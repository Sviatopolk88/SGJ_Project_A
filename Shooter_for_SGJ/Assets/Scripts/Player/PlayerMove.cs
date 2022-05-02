using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    public LayerMask ground;
    public Vector3 drag;
    public bool IsDialog;
    private CharacterController _charController;
    private Vector3 _velocity;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _groundChecker = transform.Find("GroundChecker");
    }

    void Update()
    {
        if (!IsDialog)
        {
            // ���� �������� �� �����, �������� �������� �� �
            _isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0f;
            }

            // ����������� ���������
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            _charController.Move(move * Time.deltaTime * speed);

            // ������
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // ����������
            _velocity.y += gravity * Time.deltaTime;

            // ���� ������
            _velocity.x /= 1 + drag.x * Time.deltaTime;
            _velocity.y /= 1 + drag.y * Time.deltaTime;
            _velocity.z /= 1 + drag.z * Time.deltaTime;

            _charController.Move(_velocity * Time.deltaTime);
        }
    }
}
