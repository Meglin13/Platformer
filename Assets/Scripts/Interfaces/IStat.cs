using System;

/// <summary>
/// Интерфейс статистики
/// </summary>
public interface IStat
{
    public int CurrentValue { get; }

    public void ChangeValue(int amount);

    public event Action OnValueChanged;

    public event Action OnValueEmpty;

    public void ClearEvents();
}