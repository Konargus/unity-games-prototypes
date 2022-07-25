using UnityEngine;

public class IsometricMovement : Movement
{
    private void Update()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.fixedDeltaTime);
            transform.LookAt(Target);
        }
        else
        {
            transform.Translate(Vector3.forward * (MoveDelta.x * Time.fixedDeltaTime * Speed));
            transform.Translate(Vector3.left * (MoveDelta.y * Time.fixedDeltaTime * Speed));
        }
    }
}