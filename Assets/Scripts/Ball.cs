using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject palette;
    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverTextObj;

    public float speed = 1f;

    private float tmr = 0;

    private bool isMoving = false;
    private float xVelocity = 0f; // if true x++ else x--
    private float yVelocity = -0.1f; // if true y++ else y--

    // Start is called before the first frame update
    void Start()
    {
        ball.transform.position = new Vector3(palette.transform.position.x, palette.transform.position.y + 1, palette.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenu.gameIsStarted)
        {
            lvlText.enabled = true;
            lvlText.SetText("Level " + GlobalVariables.level);
            lvlText.enabled = true;
            livesText.SetText("Lives " + GlobalVariables.lives);
        }
        else
        {
            lvlText.enabled = false;
            livesText.enabled = false;
        }

        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        if (isMoving)
        {
            if (ball.transform.position.x <= -14)
            {
                xVelocity *= -1;
            }
            else if (ball.transform.position.x >= 14)
            {
                xVelocity *= -1;
            }
            else if (ball.transform.position.y >= 14)
            {
                yVelocity *= -1;
            }
            else if (ball.transform.position.y <= -5)
            {
                isMoving = false;
                xVelocity = 0f;
                yVelocity = -0.1f;
                GlobalVariables.lives--;
                livesText.SetText("Lives " + GlobalVariables.lives);
            }

            ball.transform.position = new Vector3(ball.transform.position.x + xVelocity * speed, ball.transform.position.y + yVelocity * speed, 0);
        }
        else
        {
            xVelocity = 0f;
            yVelocity = 0.1f;
            ball.transform.position = new Vector3(palette.transform.position.x, palette.transform.position.y + 1, palette.transform.position.z);
        }

        if (GlobalVariables.lives == 0)
        {
            MainMenu.gameIsStarted = false;
            PauseMenu.GameIsPaused = false;
            gameOverTextObj.SetActive(true);
            tmr += 0.2f;
            Debug.Log(tmr);
            if (tmr >= 100)
            {
                gameOver();
                tmr = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && MainMenu.gameIsStarted)
        {
            isMoving = true;
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Palette")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            yVelocity = -1 * yVelocity;

            xVelocity = (xVelocity + (ball.transform.position.x - palette.transform.position.x)) / 15;
        }

        if(collision.gameObject.tag == "block")
        {
            if (ball.transform.position.y > collision.gameObject.transform.position.y && yVelocity < 0)
                yVelocity = -1 * yVelocity;
            if (collision.gameObject.transform.position.y > ball.transform.position.y && yVelocity > 0)
                yVelocity = -1 * yVelocity;
            if (ball.transform.position.x > collision.gameObject.transform.position.x && xVelocity < 0)
                xVelocity = -1 * xVelocity;
            if (collision.gameObject.transform.position.x > ball.transform.position.x && xVelocity > 0)
                xVelocity = -1 * xVelocity;
            if (collision.gameObject != null)
            {
                if (!MainMenu.gameIsStarted)
                    return;
                Destroy(collision.gameObject);
                GlobalVariables.countOfVblocks--;
                Debug.Log(GlobalVariables.countOfVblocks);
            }
            if(GlobalVariables.countOfVblocks == 0)
            {
                nextLevel();
            }
        }
    }

    void nextLevel()
    {
        if (!MainMenu.gameIsStarted)
            return;
        isMoving = false;
        Debug.Log(GlobalVariables.countOfVblocks);
        GlobalVariables.createBlocks = true;
        lvlText.SetText("Level " + GlobalVariables.level);
    }

    private void gameOver()
    {
        Debug.Log("Hello World!");
        Debug.Log("Line 133");
        gameOverTextObj.SetActive(false);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScene"));
        Destroy(this);
        return;
    }
}
