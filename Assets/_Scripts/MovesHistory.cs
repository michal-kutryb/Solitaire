using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovesHistory : MonoBehaviour
{
    public static MovesHistory Instance { get; private set; }

    [SerializeField] private GameObject _deckPile;

    //Dictionary stores pairs: a moved card and a parent before moving that card

    private Stack<Move> _moves = new Stack<Move>();

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

    public void AddMove(GameObject card, GameObject parent, bool wasParentReversed)
    {
        _moves.Push(new Move(card, parent, wasParentReversed));
    }

    public void UndoMove() 
    {
        if(_moves.Count == 0 || GameManager.Instance.isDraggingACard || GameManager.Instance.isDrawing) 
        {
            return;
        }
        Move lastMove = _moves.Pop();

        if(lastMove.BeforeParentCard == null) 
        {
            _deckPile.GetComponent<DrawPile>().UndoResetDrawPile();
            return;
        }
        if (lastMove.MovedCard.transform.parent.name.Contains("Finish"))
        {
            lastMove.MovedCard.transform.parent.GetComponent<FinishPileDrop>().pileSize--;
        }
        if (lastMove.BeforeParentCard.name.Contains("Pile")) 
        {
            PlayPileDrop.AddCardOnPile(lastMove.MovedCard, lastMove.BeforeParentCard);
            if (lastMove.BeforeParentCard.transform.name.Contains("Deck")) 
            {
                lastMove.MovedCard.transform.Find("Reverse").gameObject.SetActive(true);
            }
        }
        else 
        {
            PlayPileDrop.AddCardOnCard(lastMove.MovedCard, lastMove.BeforeParentCard);
            if(lastMove.WasParentReversed == true) 
            {
                lastMove.BeforeParentCard.transform.Find("Reverse").gameObject.SetActive(true);
            }
        }
    }

}
