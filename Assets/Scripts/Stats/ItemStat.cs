using System;
using UnityEngine;

/// <summary>
/// Класс лоя счета предметов
/// </summary>
[Serializable]
public class ItemStat : IStat
{
    [SerializeField]
    private int currentValue = 0;

    public int CurrentValue => currentValue;

    public event Action OnValueChanged;

    public event Action OnValueEmpty;

    public void ChangeValue(int amount)
    {
        currentValue += amount;

        OnValueChanged?.Invoke();

        if (currentValue == 0)
        {
            OnValueEmpty?.Invoke();
        }
    }

    public void ClearEvents()
    {
        OnValueChanged = null;
        OnValueEmpty = null;
    }
}