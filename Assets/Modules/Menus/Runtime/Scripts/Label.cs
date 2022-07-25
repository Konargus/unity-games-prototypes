using TMPro;
using UnityEngine;

public class Label : TextMeshProUGUI, ILabel
{
    public virtual void SetText(string str)
    {
        text = str;
    }

    public virtual void SetOffsetPosition(Vector3 position)
    {
        transform.position -= position;
    }
}
