using UnityEngine;

namespace com.konargus.persona
{
    public class IsometricMovement : Movement
    {
        private void Update()
        {
            if (!IsMoving)
            {
                return;
            }
            
            if (Target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
                transform.LookAt(Target);
            }
            else
            {
                var targetPos = new Vector3(MoveDelta.x, 0, MoveDelta.y);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
                transform.LookAt(targetPos);
            }
        }
    }
}