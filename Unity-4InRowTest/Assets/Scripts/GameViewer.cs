using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourInRow;
using UnityEngine.UI;
using System.Linq;

public class GameViewer : MonoBehaviour, I4InRow
{
    public float StepsPause { get { return pauseSlider.value; } }
    public int XSize = 6;
    public int YSize = 7;
    public GameObject Ball;

    bool isAiMakeItsStep = false;
    bool isInputAlloved = true;
    bool AiNextStep = true;

    AICore aICore;

    public Slider pauseSlider;
    public Text scoreText;
    public Image firstImg;
    public Image secondImg;

    FourInRowGame game;
    ObjectPool firstPool;
    ObjectPool secondPool;

    public delegate void PlayTask();
    PlayTask playTask;
    void Start()
    {
        game = new FourInRowGame(7, 6, 2, 4, this);
        aICore = new AICore(game, 1000);
        firstImg.sprite = GameManager.firstPlayerSkin.roundSprite;
        secondImg.sprite = GameManager.secondPlayerSkin.roundSprite;
        PrepareScoreSize(firstImg);
        PrepareScoreSize(secondImg);
        scoreText.text = game.GetScore();
        InitiatePools();
        pauseSlider.gameObject.SetActive(!(GameManager.CurrentPlayMode == PlayMode.TwoPlayers));
        switch (GameManager.CurrentPlayMode)
        {
            case PlayMode.Multiplayer:
                break;
            case PlayMode.TwoAI:
                playTask = BothAIPlayTask;
                break;
            case PlayMode.TwoPlayers:
                playTask = TwoPlayersTask;
                break;
            case PlayMode.WithAI:
                playTask = PlayWithAITask;
                break;
        }
    }
    void InitiatePools()
    {
        var firstcopy = Instantiate(Ball);
        var secondcopy = Instantiate(Ball);
        firstcopy.GetComponent<SpriteRenderer>().sprite = GameManager.firstPlayerSkin.squareSrtite;
        secondcopy.GetComponent<SpriteRenderer>().sprite = GameManager.secondPlayerSkin.squareSrtite;
        firstPool = new ObjectPool(firstcopy);
        secondPool = new ObjectPool(secondcopy);
    }
    void PrepareScoreSize(Image sprite)
    {
        float multiplier = 2f;
        sprite.SetNativeSize();
        var rectTransform = sprite.GetComponent<RectTransform>();
        var rect = rectTransform.rect;
        rectTransform.transform.localScale = new Vector3(multiplier, multiplier, 1);
        rect.width *= multiplier;
        rect.height *= multiplier;
    }
    void BothAIPlayTask()
    {
        if (AiNextStep)
        {
            StartCoroutine(NeyralNetworkAutoPlay());
        }
    }
    void TwoPlayersTask()
    {
        if (isInputAlloved && Input.GetMouseButtonDown(0))
        {
            var x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            game.MakeOneStep(x);
        }
    }
    void PlayWithAITask()
    {
        if (game.CurrentPlayer == CellInfo.Yellow)
        {
            if (isInputAlloved && Input.GetMouseButtonDown(0))
            {
                var x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
                game.MakeOneStep(x);
                isAiMakeItsStep = true;
                StartCoroutine(NeyralNetworkAutoPlay());
            }
        }
        else if (isAiMakeItsStep)
        {
            StartCoroutine(NeyralNetworkAutoPlay());
        }
        else if (game.FirstStepPlayer == CellInfo.Red)
        {
            StartCoroutine(NeyralNetworkAutoPlay());
        }
    }
    IEnumerator NeyralNetworkAutoPlay()
    {
        if (!AiNextStep)
        {
            yield break;
        }
        AiNextStep = false;
        yield return new WaitForSeconds(StepsPause);
        game.MakeOneStep(aICore.GetStepCoord());
        isAiMakeItsStep = false;
        AiNextStep = true;
    }
    public void ShowIncorrectIndexError(int x)
    {
        aICore.IncorrectIndexes(x);
    }
    public void ShowFinalWinMessage(CellInfo winner)
    {

    }
    public void ShowMove(int x, int y, CellInfo cell)
    {
        var currentBall = cell == CellInfo.Red ?
            firstPool.GetElement() : secondPool.GetElement();
        currentBall.transform.position = new Vector3(x, y, 0);
    }
    public IEnumerator Rainbow(SpriteRenderer sprite)
    {
        float step = 0.05f;
        float grayIndex = 0.6f;
        Color color = new Color(1, 1, 1, 1);
        while (color.b > grayIndex)
        {
            color.b -= step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.g > grayIndex)
        {
            color.g -= step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.b <= 1)
        {
            color.b += step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.r > grayIndex)
        {
            color.r -= step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.g <= 1)
        {
            color.g += step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.b > grayIndex)
        {
            color.b -= step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.r <= 1)
        {
            color.r += step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
        while (color.b <= 1)
        {
            color.b += step;
            sprite.color = color;
            yield return new WaitForEndOfFrame();
        }
    }
    private void Update()
    {
        playTask();
    }
    public void ShowWinMessage(CellInfo winner, (int x, int y)[] winCells)
    {
        StartCoroutine(WinCorutine(winCells, winner));
    }
    IEnumerator WinCorutine((int x, int y)[] winCells, CellInfo winner)
    {
        AiNextStep = false;
        isInputAlloved = false;
        if (GameManager.CurrentPlayMode == PlayMode.TwoAI)
            aICore.Learn(winner);

        yield return StartCoroutine(RainbowPause(winCells, winner));
        scoreText.text = game.GetScore();
        ClearField();
        isInputAlloved = true;
        AiNextStep = true;
    }
    IEnumerator RainbowPause((int x, int y)[] winCells, CellInfo winner)
    {
        yield return StartCoroutine(MakeAllRainbow(winCells, winner));
    }
    IEnumerator MakeAllRainbow((int x, int y)[] winCells, CellInfo winner)
    {
        var coords = from cell in winCells select new Vector3(cell.x, cell.y, 0);

        ObjectPool pool = winner == CellInfo.Yellow ? firstPool : secondPool;

        var WinCells = from cell in pool.pool
                       where IsContainsVector(coords, cell)
                       orderby cell.transform.position.x
                       orderby cell.transform.position.y
                       select cell;

        foreach (var Go in WinCells)
        {
            yield return StartCoroutine(Rainbow(Go.GetComponent<SpriteRenderer>()));
            yield return null;
        }
    }
    bool IsContainsVector(IEnumerable<Vector3> coords, GameObject cell)
    {
        foreach (var coord in coords)
        {
            if ((coord - cell.transform.position).magnitude <= 0.1f) return true;
        }
        return false;
    }
    public void ClearField()
    {
        firstPool.UncheckAll();
        secondPool.UncheckAll();
    }
}
