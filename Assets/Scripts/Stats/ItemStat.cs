using System;
using UnityEngine;

public class ItemStat : MonoBehaviour, IStat
{
    private int currentValue = 0;

    public int CurrentValue => currentValue;

    public event Action OnValueChanged;
    public event Action OnValueEmpty;

    public void ChangeValue(int amount)
    {
        currentValue += amount;
    }

    public void ClearEvents()
    {
        OnValueChanged = null;
        OnValueEmpty = null;
    }
}
