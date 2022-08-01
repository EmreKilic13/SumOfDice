using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewDice : MonoBehaviour
{
    public Text 
        _frontText,
        _rightText,
        _leftText,
        _backText,
        _topText,
        _downText;

    public Button 
        _frontButton,
        _rightButton,
        _leftButton,
        _backButton,
        _topButton,
        _downButton;

    public GameObject _leftWall;
    public GameObject _rightWall;
    public GameObject _backWall;

    Utils utils;

    public bool isLastSpawn;

    void Start()
    {
        isLastSpawn = true;
        utils = Utils.getInstance();
    }

    void Update()
    {
        
        if (transform.position.z < (_backWall.transform.position.z) ||
            transform.position.x > (_leftWall.transform.position.x) ||
            transform.position.x < (_rightWall.transform.position.x))
        {
            GameManager.Instance.ObjectFallingDownToSpace(this.gameObject);
        }
        
        if(isLastSpawn)
        {
            transform.Translate(-Input.GetAxis("Horizontal") * Time.deltaTime * utils.getMovementSpeedOfDice(), 0f, 0f);
        }

    }

    void OnCollisionEnter(Collision obj)
    {
        GameManager.Instance.changeNumberOfObject(this.gameObject, obj.gameObject);
    }

    public void assignAllChildObjectToGlobalVar()
    {
        GameObject tmpCanvas;

        string canvasName = "";
        int countOfChild = transform.childCount;
        
        _leftWall = GameObject.Find("leftWall");
        _rightWall = GameObject.Find("rightWall");
        _backWall = GameObject.Find("backWall");
        
        for (int i = 0; i < countOfChild; ++i)
        {
            tmpCanvas = transform.GetChild(i).gameObject;
            canvasName = tmpCanvas.name;

            switch(canvasName)
            {
                case "front":
                    _frontButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _frontText = _frontButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                case "back":
                    _backButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _backText = _backButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                case "right":
                    _rightButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _rightText = _rightButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                case "left":
                    _leftButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _leftText = _leftButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                case "top":
                    _topButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _topText = _topButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                case "down":
                    _downButton = tmpCanvas.transform.GetChild(0).gameObject.GetComponent<Button>();
                    _downText = _downButton.transform.GetChild(0).gameObject.GetComponent<Text>();
                    break;
                default: //TODO: burada uyari cikacak.
                    break;
            }
            
        }
        

    }




}
