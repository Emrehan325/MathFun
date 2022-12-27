using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float swerveRange;
    public float swerveSpeed;
    public float rotationSpeed;

    public Animator anim;
    public GameUI gameUI;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition.x;
        }
        HandleMovement();
    }

    float? lastPos = null;
    void HandleMovement()
    {

        float ratio = Screen.width / (swerveRange * 2 * swerveSpeed);

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0))
        {
            var touchPoint = Input.mousePosition.x - lastPos.GetValueOrDefault(Screen.width / 2);
            lastPos = Input.mousePosition.x;
            Vector3 startPos = player.position;
            Vector3 targetPos = new Vector3(Mathf.Clamp(startPos.x + touchPoint / ratio, -swerveRange, swerveRange), player.position.y, player.position.z);

            player.position = targetPos;
            var rot = Quaternion.Euler(0, Mathf.Clamp(touchPoint * 100, -45, 45), 0);
            player.rotation = Quaternion.Slerp(player.rotation, rot, rotationSpeed);
        }
        else
        {
            var rot = Quaternion.Euler(0, 0, 0);
            player.rotation = Quaternion.Slerp(player.rotation, rot, rotationSpeed);
        }
        if (!anim.GetBool("isRunning"))
            anim.SetBool("isRunning", true);
    }
}
