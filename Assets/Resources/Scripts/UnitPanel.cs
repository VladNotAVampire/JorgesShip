using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    public virtual void Set(UnitStats unit)
    {
        Text name = transform.GetChild(1).GetComponent<Text>();
        Text stats = transform.GetChild(2).GetComponent<Text>();

        name.text = unit.name;
        stats.text = string.Format("{0}\t{1}\t{2}", unit.hitPoinsts, unit.damage, unit.speed);
    }
}