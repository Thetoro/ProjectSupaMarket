using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int dpad = 5;
    public static int hori = 0;
    public static int vert = 0;

    public static List<int> dirBuffer = new List<int>();

    // example inputs being created for some common motions
    inputMotion shoryu = new inputMotion("Shoryuken!").Add(6, 8, true).Add(2, 8, false).Add(6, 12, false);
    private void Awake()
    {
        //sets application to vsync at 60fps
        QualitySettings.vSyncCount = 1;

        //ignored because vsync is on, but if vsync were turned off, would fall back to this
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dirBuffer = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        //all of this makes a single directional value out of a bunch of possible inputs, eventually this would be rerouted to rebindable keys
        if (!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down"))
        {
            dpad = 5;
            hori = 0;
            vert = 0;
        }
        else
        {
            if (Input.GetKey("left") && Input.GetKey("right") || !Input.GetKey("left") && !Input.GetKey("right"))
            {
                hori = 0;
            }
            else if (Input.GetKey("left"))
            {
                hori = -1;
            }
            else if (Input.GetKey("right"))
            {
                hori = 1;
            }

            if (Input.GetKey("up") && Input.GetKey("down") || !Input.GetKey("up") && !Input.GetKey("down"))
            {
                vert = 0;
            }
            else if (Input.GetKey("down"))
            {
                vert = -1;
            }
            else if (Input.GetKey("up"))
            {
                vert = 1;
            }
            dpad = hori + 2 + ((vert + 1) * 3); //hashes the directions together to make a single number without a ton of if/elses
        }
        /*while (dirBuffer.Count > 60)
        { //clears inputs past the buffer length, which I've set to 60, should technically be a variable
            dirBuffer.RemoveAt(dirBuffer.Count - 1);
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("right") || Input.GetKeyDown("up") || Input.GetKeyDown("down") ||
        Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down"))
        { //checks if there is any change to directional keys this frame
            //dirBuffer.Add(dpad);
            dirBuffer.Insert(0, dpad);
        }
        else
        {
            //dirBuffer.Add(5); //5 is ignored
            dirBuffer.Insert(0, 5);
        }

        //this is the correct way inputs are supposed to be checked
        if (Input.GetKeyDown("space"))
        {
            if (shoryu.checkValidInput())
            {
                Debug.Log("success returning");
            }
        }*/
        Debug.Log(dpad);

    }
}
