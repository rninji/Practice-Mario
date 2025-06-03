using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 startPos;
    Transform playerPos;
    Vector3 finishPos;

    [SerializeField] 
    float offset = 7f;
    
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        finishPos = GameObject.FindGameObjectWithTag("Finish").transform.position;
        
        startPos = playerPos.position;
    }

    void Update()
    {
        // 시작 지점 오프셋
        if (playerPos.position.x < startPos.x + offset )
            return; 
        // 최종 지점 오프셋
        if (playerPos.position.x > finishPos.x - offset)
            return; 
        // 플레이어 따라 이동
        transform.position = new Vector3(playerPos.position.x, 0, -10f);
    }
}
