using System.Collections;
using UnityEngine;

public class VenomAttackManager : MonoBehaviour
{
    [SerializeField] private VenomBall venomBallPrefab;
    [SerializeField] private float venomThrowSpeed = 3f;
    private int time = 3;
    public GameObject roomPrefab;

    void Start()
    {
        bossAttack.SpiderBall += Attack;
    }

    void OnDestroy(){
        bossAttack.SpiderBall -= Attack;
    }

    public void Attack()
    {
        time = 3;
        StartCoroutine(VenomBallVolley(Random.Range(-50f, 50f)));
    }

    // Shoots 5 balls of venom
    IEnumerator VenomBallVolley(float angleChange)
    {
        Vector2 throwDirection = Vector2.down;
        float rotationDegrees = 30f;
        Vector3 rotationAxis = Vector3.forward;

        // Rotate the empty GameObject
        transform.eulerAngles = new Vector3(0, 0, angleChange);

        VenomBall venomBall;

        // Spawn the initial VenomBall
        venomBall = Instantiate(venomBallPrefab, transform.position, Quaternion.identity);
        venomBall.SetVelocity(throwDirection * venomThrowSpeed);

        // Outer loop: Flips the direction that each venom ball will be moving towards
        for(int k = 0; k < 2; k++)
        {
            // Inner loop: Handles VenomBall spawning and movement setting
            for(int i = 1; i < 3; i++)
            {
                Vector3 rotatedVector = Quaternion.AngleAxis(rotationDegrees * i, rotationAxis) * throwDirection;
                venomBall = Instantiate(venomBallPrefab, transform.position, Quaternion.identity);
                venomBall.SetVelocity(rotatedVector * venomThrowSpeed);
                venomBall.transform.SetParent(roomPrefab.transform);
            }
            rotationDegrees = -rotationDegrees;
        }

        yield return new WaitForSeconds(1.5f);
        time--;

        if(time > 0)
        {
            StartCoroutine(VenomBallVolley(Random.Range(-5f, 5f)));
        }
    }
}
