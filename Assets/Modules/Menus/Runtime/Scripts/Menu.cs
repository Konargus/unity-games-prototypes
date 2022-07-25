using UnityEngine;

public class Menu : MonoBehaviour, IMenu
{
    [SerializeField] protected ButtonText buttonText;
    [SerializeField] protected Label label;
    
    public virtual IButtonText AddButtonText()
    {
        return Instantiate(buttonText, transform);
    }
    
    public virtual ILabel AddLabel()
    {
        return Instantiate(label, transform);
    }
    
    public virtual void Stretch()
    {
        var rt = GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = new Vector2(0, 0);
        rt.offsetMax = new Vector2(0, 0);
    }
}
