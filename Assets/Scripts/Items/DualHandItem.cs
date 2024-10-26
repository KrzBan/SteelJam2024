using UnityEngine;

public class DualHandItem : IItem
{
    ItemData ItemData;

    public DualHandItem()
    {
        ItemData = new ItemData
        {
            amount = 1,
            handRequirement = 2,

        };
    }

    public GameObject GetPrefab()
    {
        throw new System.NotImplementedException();
    }

    public void Use(PlayerManager User)
    {
        throw new System.NotImplementedException();
    }
}
