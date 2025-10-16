using System;
using UnityEngine;

/// <summary>
/// Класс здоровья
/// </summary>
[Serializable]
public class HealthStat : IStat
{
    [SerializeField]
    private int maxValue = 10;

    public int MaxValue => maxValue;

    [SerializeField]
    private int currentValue;

    public int CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, 0, MaxValue);

            OnValueChanged?.Invoke();

            if (currentValue == 0)
            {
                OnValueEmpty?.Invoke();
            }
        }
    }

    // События изменения значения
    public event Action OnValueChanged = delegate { };

    public event Action OnValueEmpty = delegate { };

    public HealthStat() { }

    public HealthStat(int max)
    {
        maxValue = max;
        CurrentValue = max;
    }

    /// <summary>
    /// Изменение кол-ва здоровья
    /// </summary>
    /// <param name="amount">Значение, на которое изменяется текущее значение здоровья</param>
    public void ChangeValue(int amount)
    {
        CurrentValue += amount;
        OnValueChanged?.Invoke();
    }

    /// <summary>
    /// Очистка событий
    /// </summary>
    public void ClearEvents()
    {
        OnValueChanged = null;
        OnValueEmpty = null;
    }
}