using UnityEngine;

public class Move
{
    public GameObject MovedCard { get; private set; }
    public GameObject BeforeParentCard { get; private set; }
    public bool WasParentReversed { get; private set; }

    public Move (GameObject movedCard, GameObject beforeParentCard)
    {
        MovedCard = movedCard;
        BeforeParentCard = beforeParentCard;
    }

    public Move(GameObject movedCard, GameObject beforeParentCard, bool wasParentReversed)
    {
        MovedCard = movedCard;
        BeforeParentCard = beforeParentCard;
        WasParentReversed = wasParentReversed;
    }
}
