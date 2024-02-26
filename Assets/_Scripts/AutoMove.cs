using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public static AutoMove Instance { get; private set; }
    [SerializeField] private List<GameObject> _finishPiles = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private GameObject CheckIfPosibleToAutoMove(Card card) 
    {
        foreach(GameObject pile in _finishPiles) 
        {
            FinishPileDrop pileDrop = pile.GetComponent<FinishPileDrop>();
            if (!card.Suit.Equals(pileDrop.PileSuit)) 
            {
                continue;
            }
            if(pileDrop.pileSize == (int)card.Value) 
            {
                return pile;
            }
        }
        return null;
    }

    public void AutoMoveCard(GameObject card) 
    {
        CardInfo info = card.GetComponent<CardInfo>();
        GameObject pileToMove = CheckIfPosibleToAutoMove(info.Card);
        if (pileToMove != null) 
        {
            GameObject cardParent = card.transform.parent.gameObject;
            bool wasParentReversed = false;
            Transform reverse = cardParent.transform.Find("Reverse");
            if (reverse != null && reverse.gameObject.activeSelf) 
            {
                wasParentReversed = true;
                reverse.gameObject.SetActive(false);
            }
            PlayPileDrop.AddCardOnPile(card, pileToMove);
            pileToMove.GetComponent<FinishPileDrop>().pileSize++;
            MovesHistory.Instance.AddMove(card, cardParent, wasParentReversed);
        }
    }
}
