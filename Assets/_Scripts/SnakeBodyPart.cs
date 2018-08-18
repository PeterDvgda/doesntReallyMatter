using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    public GameObject parentBodyPart;
    public Vector3 lastPosition;
    public Vector3 nextPosition;

    private float _timer = 0;


    void Update()
    {
        if (parentBodyPart == null) return; // prevents null refrence errors

        _timer = Mathf.Clamp(_timer + Time.deltaTime, 0, 1); // this will prevent us from over shooting
        
        float distanceToNextPosition = Vector3.Distance(transform.position, nextPosition);

        if (distanceToNextPosition > 0) // we are further than 0 away from where we want to be
        {
            transform.position = Vector3.Lerp(lastPosition, nextPosition, _timer*10);
        }

        else // we're not further than 0, so we must be at our destination
        {
            lastPosition = nextPosition;
            nextPosition = parentBodyPart.transform.position;
            _timer = 0;
        }
    }
}