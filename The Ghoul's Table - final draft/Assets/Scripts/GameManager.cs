using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable


public class BlackjackGameManager : MonoBehaviour
{
    [SerializeField] private Transform playerArea = null!;
    [SerializeField] private Transform dealerArea = null!;
    [SerializeField] private TMP_Text statusText = null!;
    [SerializeField] private DeckManager deckManager = null!;
    [SerializeField] private BlackjackAnimations animations = null!;
    [SerializeField] private BetManager betManager = null!;

    private BlackjackGame game = null!;
    [SerializeField] private Button hitButton = null!;
    [SerializeField] private Button standButton = null!;
    [SerializeField] private Button newGameButton = null!;
    [SerializeField] private Button bet20Button = null!;
    [SerializeField] private Button lockInBetsButton = null!;
    [SerializeField] private Button shootDealerButton = null!;
    [SerializeField] private Button keepWinningsButton = null!;

    [SerializeField] private TMP_Text keepWinningsText = null!;

    [SerializeField] private float cardOffset = 0.5f; //offset between x position of cards
    [SerializeField] private float cardzOffset = 0.2f;

    private BlackjackOutcome blackjackOutcome;
    public BlackjackOutcome BlackjackOutcome => blackjackOutcome;

    private GameState gameState;
    public GameState GameState => gameState;

    public Transform PlayerArea => playerArea;
    public Transform DealerArea => dealerArea;
    private IEnumerator StartRoundAnimations()
    { 
        Hand playerHand = game.GetPlayerHand();
        Hand dealerHand = game.GetDealerHand(); //right now, both hands should have only 2 cards
        List<CardView> initialCards = new();
        for (int i = 0; i < 2; i++) //dealer's first card should be face up
        {
            Card playerDrawnCard = playerHand.GetCards()[i];
            CardView playerCardView = deckManager.DrawCardPrefab(playerDrawnCard);
            initialCards.Add(playerCardView);
            Card dealerDrawnCard = dealerHand.GetCards()[i];
            CardView dealerCardView = deckManager.DrawCardPrefab(dealerDrawnCard);
            initialCards.Add(dealerCardView);
            dealerCardView.transform.SetParent(dealerArea, worldPositionStays: true);
        }
        yield return StartCoroutine(animations.StartRoundAnimations(initialCards));
        //EnableButton(true);
        if (game.GetPlayerHand().IsNaturalBlackjack() || game.GetDealerHand().IsNaturalBlackjack())
            StartCoroutine(EndGame());
        else
            statusText.text = "Your turn!";
        gameState = GameState.Waiting;
    }

    private IEnumerator PlayerDrawAnimation()
    {
        gameState = GameState.Drawing;
        Card? drawnCard = game.PlayerHit();
        CardView cardView = deckManager.DrawCardPrefab(drawnCard);
        int playerHandCount = game.GetPlayerHandCount();
        yield return StartCoroutine(animations.PlayerDrawAnimation(cardView, playerHandCount));
        (int total, bool isSoft) = game.GetPlayerHand().GetValue();
        if (game.GetPlayerHand().IsBust())
        {
            StartCoroutine(EndGame());
        }
        else if (total == 21)
        {
            PlayerStand();
        }
        else 
        {
            //EnableButton(true);
            gameState = GameState.Waiting;
        }
    }

    private IEnumerator DealerDrawAnimation(int numCardDrawn)
    {
        gameState = GameState.Drawing;
        Hand dealerHand = game.GetDealerHand();
        List<CardView> cardsDrawn = new();
        for (int i = 0; i < numCardDrawn; i++)
        {
            Card drawnCard = dealerHand.GetCards()[i + 2];
            CardView cardView = deckManager.DrawCardPrefab(drawnCard);
            cardView.transform.SetParent(dealerArea, worldPositionStays: true);
            cardsDrawn.Add(cardView); 
        }
        yield return StartCoroutine(animations.DealerDrawAnimation(cardsDrawn, numCardDrawn));
        StartCoroutine(EndGame());
    }

    private IEnumerator DealerFlipAnimation()
    {
        yield return StartCoroutine(animations.DealerFlipAnimation());
        BlackjackOutcome gameOutcome = game.DetermineOutcome();
        blackjackOutcome = gameOutcome;
        Debug.Log("The game outcome is " + gameOutcome);
        DisplayOutcome(gameOutcome);
    }
    private void DisplayOutcome(BlackjackOutcome outcome)
    {
        string textOutcome = OutcomeText.OutcomeToText[outcome];
        statusText.text = textOutcome;
    }

    private IEnumerator GameStart()
    {
        statusText.text = "Game Starting!";
        gameState = GameState.Drawing;
        yield return new WaitForSeconds(1f);
        statusText.text = "Drawing cards...";
        StartCoroutine(StartRoundAnimations());
    }

    void Start()
    {
        //EnableButton(false);
        EnableNewGameButton(false);
        //EnableBets(true); //StartRound() is in PlayerLockBets(), because round starts only after bets are placed
        //lockInBetsButton.interactable = false;
       // EnableWinChoices(false);
        gameState = GameState.Betting;
        statusText.text = "Minimum Bet is: 20 bucks";
        if (betManager.PlayerWinnings == 0)
            Debug.Log("player has died here");

    }

    public void PlayerBet()
    {
        if (betManager.PlayerWinnings == 0)
        {
            statusText.text = "Can't bet anymore!";
            return;
        }
        betManager.PlayerBetOnce();
        //lockInBetsButton.interactable = true; //ensures minimum betting is 20bucks to start the round.
    }

    public void PlayerLockBets()
    {
        //EnableBets(false);
        StartRound();
    }
    //void EnableButton(bool enable)
    //{
    //    hitButton.gameObject.SetActive(enable);
    //    standButton.gameObject.SetActive(enable);
    //}
    void EnableNewGameButton(bool enable)
    {
        newGameButton.gameObject.SetActive(enable);
    }

    //private void EnableBets(bool enable)
    //{
    //    bet20Button.interactable = enable;
    //    lockInBetsButton.interactable = enable;
    //}

    //private void EnableWinChoices (bool enable)
    //{
    //    shootDealerButton.gameObject.SetActive(enable);
    //    keepWinningsButton.gameObject.SetActive(enable);
    //}

    public void ShootDealer()
    {
        betManager.PlayerLoseOrShoot();
        Debug.Log("Player Shooting animation");
        //chain player shooting animation here
        //EnableWinChoices(false);
        if (Shoot())
            PlayerWins();
        else
            statusText.text = "You failed to kill the Dealer.. play on.";
        EnableNewGameButton(true);
    }

    public void KeepWinnings()
    {
        if (game.DetermineOutcome() == BlackjackOutcome.PlayerWin)
        {
            betManager.PlayerWin();

        }
        else
        {
            betManager.PlayerNatural(); 
        }
        //EnableWinChoices(false);
        EnableNewGameButton(true);
        keepWinningsText.text = "";
    }

    public void StartRound()
    {
        game = new BlackjackGame();
        ClearTable();
        StartCoroutine(GameStart());
        bool started = game.StartRound(); // calling BlackjackGame.StartRound()
        if (!started)
        {
            statusText.text = "Deck exhausted.";
            return;
        }
    }

    public void PlayerHit()
    {
        Debug.Log("Player Hit!");
        //EnableButton(false);
        StartCoroutine(PlayerDrawAnimation());
    }

    public void PlayerStand() //dealer plays immediately after 
    {
        game.PlayerStand();
        //EnableButton(false);
        statusText.text = "Dealer's turn!";
        bool dealerplayed = game.DealerPlay();
        if (dealerplayed)
        {
            int numCardDrawn = game.GetDealerHandCount() - 2;
            StartCoroutine(DealerDrawAnimation(numCardDrawn));
        } //EndGame() is chained into the DealerDrawAnimation coroutine
        else
            StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        //EnableButton(false);
        yield return StartCoroutine(DealerFlipAnimation());
        switch (game.DetermineOutcome())
        {
            case BlackjackOutcome.PlayerBust: //this does not control the bets yet
            case BlackjackOutcome.DealerWin:
                StartCoroutine(DealerWinChoice());
                gameState = GameState.EndGameLose;
                break;

            case BlackjackOutcome.DealerBlackjack:
                StartCoroutine(DealerBlackjack());
                gameState = GameState.EndGameLose;
                break;

            case BlackjackOutcome.Push:
                StartCoroutine(Push());
                gameState = GameState.EndGamePush;
                break;

            case BlackjackOutcome.PlayerWin:
            case BlackjackOutcome.DealerBust:
            case BlackjackOutcome.PlayerBlackjack:
                StartCoroutine(PauseForWinChoice()); //gamestate changes in this coroutine
                break;
        }
    }

    private IEnumerator DealerBlackjack()
    {
        yield return new WaitForSeconds(2f);
        statusText.text = "Dealer Keeps Winnings!";
        betManager.PlayerLoseOrShoot();
        EnableNewGameButton(true);
    }

    private IEnumerator DealerWinChoice()
    {
        yield return new WaitForSeconds(2f);
        float choice = UnityEngine.Random.value;
        if (choice < 0.5f)
        {
            betManager.Push(); //dealer shoots
            statusText.text = "Dealer shoots!";
            //chain animation to shooting dealer here
            if (Shoot())
                PlayerDies();
            else
                statusText.text = "Dealer failed to kill you.. play on.";
            EnableNewGameButton(true);
        }
        else
        {
            betManager.PlayerLoseOrShoot(); //dealer keeps winnings
            statusText.text = "Dealer Keeps Winnings!";
            EnableNewGameButton(true);
        }
    }

    private bool Shoot()
    {
        float chance = UnityEngine.Random.value;
        if (chance < (1f / 6f))
            return true;
        return false;
    }

    private IEnumerator Push()
    {
        yield return new WaitForSeconds(2f);
        betManager.Push();
        statusText.text = "Keep your Winnings!";
        EnableNewGameButton(true);
    }

    private IEnumerator PauseForWinChoice() 
    {
        yield return new WaitForSeconds(2f);
        //EnableWinChoices(true);
        statusText.text = "Pick your poison!";
        gameState = GameState.EndGameWin;
    }


    public void RestartGame()
    {
        List<CardView> activeCards = deckManager.ActiveCards();
        for (int i = 0; i <  activeCards.Count; i++) {
            Destroy(activeCards[i].gameObject);
        }
        deckManager.DestroyActiveCards();
        Start();
    }

    void DisplayHand(Hand hand, Transform area)
    {

        foreach (Transform child in area)
            Destroy(child.gameObject);

        List<Card> cards = hand.GetCards();
        for (int i = 0; i < cards.Count; i++)
        { 
            CardView cardView = deckManager.DrawCardPrefab(cards[i]); //then, need to transform to the correct position
            Vector3 position = area.position; 
            position.x += i * cardOffset; 
            cardView.transform.position = position; 

            //if (hideSecondCard && i == 1)
            //    cardView.cardText.text = "Hidden";
            //else
            //    cardView.SetCard(cards[i]);
        } 
    }

    void ClearTable()
    {
        foreach (Transform child in playerArea)
            Destroy(child.gameObject);
        foreach (Transform child in dealerArea) 
            Destroy(child.gameObject);
    }

    private void PlayerDies()
    {
        statusText.text = "You have lost your life! loser.";
        betManager.PlayerLostRoulette();
        newGameButton.interactable = false;
    }

    private void PlayerWins()
    {
        statusText.text = "You have killed the Dealer and won the grand prize!";
        betManager.PlayerWinRoulette();
        newGameButton.interactable = false;
    }

    private void Update()
    {
        Debug.Log(gameState);
    }
    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Space)) 
    //    //{
    //    //    Card card = game.DrawCard();
    //    //    CardView drawnCard = deckManager.DrawCardPrefab(card);
    //    //    Debug.Log("Keyspace pressed");
    //    //    if (drawnCard != null)
    //    //    {
    //    //        Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
    //    //        // Move card to a visible position
    //    //        drawnCard.transform.position = testingArea.position;
    //    //    }
    //    //} //this is for testing purposes

    //    //if (Input.GetKeyDown(KeyCode.Q))
    //    //{
    //    //    Debug.Log("Player Hit!");
    //    //    Card? drawnCard = game.PlayerHit();
    //    //    if (drawnCard == null)
    //    //    {
    //    //        Debug.Log("null");
    //    //    }
    //    //    CardView cardView = deckManager.DrawCardPrefab(drawnCard);
    //    //    Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
    //    //    int playerHandCount = game.GetPlayerHandCount();
    //    //    Vector3 position = playerArea.position;
    //    //    position.x += (playerHandCount - 1) * cardOffset;
    //    //    cardView.transform.position = position;
    //    //}  //this was for debugging purposes
    //}

}
