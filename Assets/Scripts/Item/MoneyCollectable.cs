using UnityEngine;

public class MoneyCollectable : CollectableScript
{
    [SerializeField]
    private int amount = 1;

    public override void Collect(Player player)
    {
        base.Collect(player);
        player.Money.ChangeValue(amount);
    }
}