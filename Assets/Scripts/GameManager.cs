using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // TODO: GameState yapisi arastirilacak

    enum ColorOfDice
    {
        RED = 1,
        YELLOW = 2,
        GREEN = 3,
        BLUE = 4,
        WHITE = 5,
    };


    public GameObject _newDice;
    public GameObject _wallPiece;
    public static GameManager Instance;
    public bool _isCollisionExisting;
    private GameObject currentGO;

    Dictionary<string, GameObject> _diceMap;
    Dictionary<int, int> _successValueOfThePlayer;

    // class object definitions.
    Score scoreOfGame;
    SuccessValueOfThePlayer successValueOfThePlayer;
    RewardSystemManager rewardSystemManager;
    RewardSystem rewardSystem;
    Utils utils;

    

    float _timerForRecreation;
    int _numberOfOldObject;
    bool _isItCreatedBefore;
    Vector3 _beginPosition;
    Rigidbody rb;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreOfGame = Score.getInstance(); //singleton object
        successValueOfThePlayer = SuccessValueOfThePlayer.getInstance(); //singleton object
        utils = Utils.getInstance(); //singleton object
        rewardSystem = new RewardSystem();

        rewardSystem.signUpToBeAnObserver(GameObject.Find("RewardSystem").GetComponent<FireballReward>(),
            GameObject.Find("RewardSystem").GetComponent<WallAdditionReward>()) ;

        _beginPosition = new Vector3(0f, 5.0f, 40.0f);
        _diceMap = new Dictionary<string, GameObject>();
        _successValueOfThePlayer = new Dictionary<int, int>();
        _numberOfOldObject = 0;
        _isItCreatedBefore = false;
        _timerForRecreation = utils.getTimerValue();
    }
    
    void Update()
    {
        
        _timerForRecreation -= Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentGO.GetComponent<NewDice>().isLastSpawn = false;
            rb.AddForce(new Vector3(0, 0, -50) * utils.getSpeedOfDice());
            _isItCreatedBefore = false;
        }


        if(_timerForRecreation < 0)
        {
            if(!_isItCreatedBefore)
            {
                rewardSystem.notifyAllObserver();
                currentGO = createNewDice(_newDice, _beginPosition);
                rb = currentGO.GetComponent<Rigidbody>();
                _successValueOfThePlayer.Add(_numberOfOldObject, scoreOfGame.getScore());// oynanan oyun sayisi ve score.
                successValueOfThePlayer.calculateTheSuccessValueOfThePlayer(_successValueOfThePlayer);
                
            }

            _isItCreatedBefore = true;
            _timerForRecreation = utils.getTimerValue(); // time resetlendi.
        }

    }

    public void ObjectFallingDownToSpace(GameObject fallingDownObject)
    {
        int tmpText = int.Parse(fallingDownObject.GetComponent<NewDice>()._frontText.text);
        scoreOfGame.decreaseScoreOfGame(tmpText);
        Destroy(fallingDownObject.GetComponent<NewDice>()); // scripti silmek 
        Destroy(fallingDownObject);
        _diceMap.Remove(fallingDownObject.name);
        
    }

    public void changeNumberOfObject(GameObject objectFallingFromAbove,GameObject followingObject)
    {
        if(followingObject.gameObject.name != "Plane" && 
           followingObject.gameObject.name != "leftWall" &&
           followingObject.gameObject.name != "rightWall" &&
           followingObject.gameObject.name != "backWall" &&
           !followingObject.gameObject.name.Contains("pieceOfWall_")) 
        {
            if (objectFallingFromAbove.GetComponent<NewDice>()._frontText.text ==
                followingObject.GetComponent<NewDice>()._frontText.text)
            {
                int tmpValue = int.Parse(objectFallingFromAbove.GetComponent<NewDice>()._frontText.text);
                changeTextOfObject(followingObject, tmpValue);
                changeMassOfObject(followingObject);

                Destroy(GameObject.Find(objectFallingFromAbove.name).GetComponent<NewDice>());//TODO:
                Destroy(GameObject.Find(objectFallingFromAbove.name));
                _diceMap.Remove(objectFallingFromAbove.name);
            }
        }
            

    }

    public void changeTextOfObject(GameObject followingObject, int valueOfDice)
    {
        int tmpNumber = 0;

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._frontText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._frontText.text = tmpNumber.ToString();

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._rightText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._rightText.text = tmpNumber.ToString();

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._leftText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._leftText.text = tmpNumber.ToString();

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._backText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._backText.text = tmpNumber.ToString();

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._topText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._topText.text = tmpNumber.ToString();

        tmpNumber = int.Parse(followingObject.GetComponent<NewDice>()._downText.text) + valueOfDice;
        followingObject.GetComponent<NewDice>()._downText.text = tmpNumber.ToString();

        scoreOfGame.setScore(tmpNumber);
        
    }

    public void changeMassOfObject(GameObject followingObject)
    {
        int objectValue = 0;
        objectValue = int.Parse(followingObject.GetComponent<NewDice>()._frontText.text);
        followingObject.GetComponent<Rigidbody>().mass = (float)(1 + (objectValue * 0.05));
    }

    public void changeColorOfObject(GameObject dice, Color color)
    {// burada dice parametresinin null olup olmadigi kontrol edilecek
        dice.GetComponent<NewDice>()._frontButton.GetComponent<Image>().color   = color;
        dice.GetComponent<NewDice>()._rightButton.GetComponent<Image>().color   = color;
        dice.GetComponent<NewDice>()._leftButton.GetComponent<Image>().color    = color;
        dice.GetComponent<NewDice>()._backButton.GetComponent<Image>().color    = color;
        dice.GetComponent<NewDice>()._topButton.GetComponent<Image>().color     = color;
        dice.GetComponent<NewDice>()._downButton.GetComponent<Image>().color    = color;

    }

    public GameObject createNewDice(GameObject dice, Vector3 beginPos )
    {// burada dice ve beginPos parametresinin null olup olmadigi kontrol edilecek

        _numberOfOldObject++; // uretilen her yeni nesneye uniq deger vermek icin yapildi.
        
        var spawnedDice = Instantiate(dice, beginPos, Quaternion.identity);
        
        spawnedDice.GetComponent<NewDice>().assignAllChildObjectToGlobalVar();
        spawnedDice.name = "Dice_" + _numberOfOldObject.ToString();
        _diceMap.Add(spawnedDice.name, spawnedDice);

        /*   random enum secmek icin yapildi  */
        Array values = Enum.GetValues(typeof(ColorOfDice));
        ColorOfDice randomColor = (ColorOfDice)values.GetValue(Random.Range(0,values.Length));
        /* - */

        switch(randomColor)
        {
            case ColorOfDice.RED:
                changeColorOfObject(spawnedDice,Color.red);
                break;
            case ColorOfDice.YELLOW:
                changeColorOfObject(spawnedDice, Color.yellow);
                break;
            case ColorOfDice.GREEN:
                changeColorOfObject(spawnedDice, Color.green);
                break;
            case ColorOfDice.BLUE:
                changeColorOfObject(spawnedDice, Color.blue);
                break;
            case ColorOfDice.WHITE:
                changeColorOfObject(spawnedDice, Color.white);
                break;
            default:
                break;
        };
        
        return spawnedDice;

    }

    


}
