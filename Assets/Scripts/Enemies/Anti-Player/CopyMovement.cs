using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;

    private void Start()
    {
        StartCoroutine(FollowPlayerCoroutine());
    }

    private IEnumerator FollowPlayerCoroutine()
    {
        while (true)
        {
            if (player != null)
            {
                Vector3 targetPosition = player.position;
                Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
            yield return null;
        }
    }
}
