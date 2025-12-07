using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Śledzenie Gracza")]
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    
    [SerializeField] private float cameraSpeed;

    [SerializeField] private float cameraYSpeed;

    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float minYPosition = -536f;
    
    private float lookAhead;

    private void LateUpdate()
    {
        if (player == null)
            return;
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        float newX = player.position.x + lookAhead;

        float targetY = player.position.y + yOffset;
        targetY = Mathf.Max(targetY, minYPosition);

        float newY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * cameraYSpeed);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        if (_newRoom != null)
        {
            transform.position = new Vector3(_newRoom.position.x, _newRoom.position.y, transform.position.z);
        }
    }
}