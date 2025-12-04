using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{

    public InputActionAsset inputActions;

    private InputAction arrowsAction;
    private InputAction keyAction;

    private int index = 0;

    // Start is called before the first frame update
    public static int dpad = 5;
    public static int hori = 0;
    public static int vert = 0;

    public static List<int> dirBuffer = new List<int>();

    private int[] codigo = {2,6,8};

    // example inputs being created for some common motions
    inputMotion shoryu = new inputMotion("Shoryuken!").Add(6, 8, true).Add(2, 8, false).Add(6, 12, false);

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        //sets application to vsync at 60fps
        QualitySettings.vSyncCount = 1;

        //ignored because vsync is on, but if vsync were turned off, would fall back to this
        Application.targetFrameRate = 60;

        arrowsAction = InputSystem.actions.FindAction("Move");
        keyAction = InputSystem.actions.FindAction("Interact");
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
            
            if (keyAction.WasPressedThisFrame())
            {
                dpad = dpad * 0;
            }
        }
        
        Debug.Log(dpad);
        VerificarInput(dpad);
    }

    private void VerificarInput(int dpad) 
    {

        if (dpad != 5)
        {
            if (dpad == codigo[index] && (arrowsAction.WasPressedThisFrame() || keyAction.WasPressedThisFrame()))
            {
                
                if (index >= codigo.Length - 1)
                {
                    Debug.Log("SECUENCIA COMPLETA");
                    index = 0; // Reset para siguiente uso
                    return;
                }
                index++;
                if (index < codigo.Length)
                {
                    Debug.Log("El numero que sigue es: " + codigo[index]);
                }

            }
            else if(dpad != codigo[index] && (arrowsAction.WasPressedThisFrame() || keyAction.WasPressedThisFrame()))
            {
                Debug.Log("Wrong!");
                index = 0;
            }
        }
       
    }
}
