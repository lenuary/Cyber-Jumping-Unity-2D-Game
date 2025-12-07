using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Range(0f, 1f)]
    [SerializeField] private float predkosc;

    private float spriteWidth;
    private float loopWidth;
    private Vector3 startPosition;
    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        startPosition = transform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;  
        loopWidth = spriteWidth * 2f;
    }

    void LateUpdate()
    {
        float relativeCamMove = cameraTransform.position.x * (1 - predkosc);
        float distanceToMoveX = cameraTransform.position.x * predkosc;
        float distanceToMoveY = cameraTransform.position.y * predkosc;

        transform.position = new Vector3(startPosition.x + distanceToMoveX, startPosition.y + distanceToMoveY, transform.position.z);

        if (relativeCamMove > startPosition.x + spriteWidth)
        {
            startPosition.x += loopWidth;
        }
        else if (relativeCamMove < startPosition.x - spriteWidth)
        {
            startPosition.x -= loopWidth;
        }
    }
}