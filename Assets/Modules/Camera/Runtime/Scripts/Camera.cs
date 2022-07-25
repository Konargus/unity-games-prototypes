using UnityEngine;

public class Camera : MonoBehaviour, ICamera
{
    private static Camera _instance;
    private Transform _target;
    private Vector3 _offset;

    public static Camera Instance
    {
        get { return _instance ??= Instantiate(Resources.Load<Camera>("Camera")).GetComponent<Camera>(); }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (_target == null)
            return;
        
        var tr = _instance.transform;
        tr.LookAt(_target);
        var targetPos = _target.position;
        tr.position = new Vector3(targetPos.x - _offset.x, _offset.y, targetPos.z);
    }
    
    public virtual void LookAt(Transform target)
    {
        _target = target;
    }
    
    public virtual void Follow(Transform target, Vector3 offset)
    {
        _target = target;
        _offset = offset;
    }
}
