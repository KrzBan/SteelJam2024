using UnityEngine;

public class ItemData {
    public int handRequirement;
    public int amount;
}
public class PlayerManager { };
public interface IItem
{
    GameObject GetPrefab();
    void Use(PlayerManager User);
}
