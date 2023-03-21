using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    private bool firstGuess,secondGuess;
    private int countCorrectGuesses;
    private int countGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle,secondGuessPuzzle;
    public GameObject GameWin;
    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("");
        puzzles = Resources.LoadAll<Sprite>("");

    }
    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count;

    }

  void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("puzzleBtn");
        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
        
    }
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i= 0; i < looper; i++)
        {
            if (index == looper%2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }
    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }
    public void PickPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            if (firstGuessPuzzle == secondGuessPuzzle)
            {
                print("Match!");
            }
            else
            {
                print("Try it again:(");
            }
            gameGuesses++;
            StartCoroutine(CheckThePuzzleMatch());
        }
    }
    IEnumerator CheckThePuzzleMatch()
    {
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckTheGmaeFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(0.5f);
        firstGuess = secondGuess = false;
    }
    void CheckTheGmaeFinished()
    {
        countCorrectGuesses=8;
        if (countCorrectGuesses == gameGuesses)
        {
        print("Game Over!");
        GameWin.SetActive(true);
        print(countGuesses + "");
        }
       
    }
        public void NextBtnClick()
    {
        print("next click");
    }
    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[1];
            int randomIndex = Random.Range(i, list.Count);
            list[1] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
