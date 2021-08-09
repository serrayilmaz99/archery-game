using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    // [SerializeField]
    private float rotationSpeed = 1.0f;

    //[SerializeField]
    private Vector2 rotationRange = new Vector2(50, 90);

    public Vector3 targetAngle;

    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //targetAngle.y += Input.GetAxis("Mouse X") * rotationSpeed;
        //targetAngle.x += Input.GetAxis("Mouse Y") * rotationSpeed;


        //targetAngle.y = Mathf.Clamp(targetAngle.y, - rotationRange.y * 0.5f, rotationRange.y * 0.35f);
        //targetAngle.x = Mathf.Clamp(targetAngle.x, - rotationRange.x * 0.5f, rotationRange.x * 0.5f);

        Quaternion targetRotation = Quaternion.Euler(-targetAngle.x, targetAngle.y, 0);

        transform.localRotation = startRotation * targetRotation;
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
   // [SerializeField]
    private float rotationSpeed = 1.0f;

    //[SerializeField]
    private Vector2 rotationRange = new Vector2(50, 90);

    private Vector3 targetAngle;

    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        targetAngle.y += Input.GetAxis("Mouse X") * rotationSpeed;
        targetAngle.x += Input.GetAxis("Mouse Y") * rotationSpeed;

        targetAngle.y = Mathf.Clamp(targetAngle.y, - rotationRange.y * 0.5f, rotationRange.y * 0.35f);
        targetAngle.x = Mathf.Clamp(targetAngle.x, - rotationRange.x * 0.5f, rotationRange.x * 0.5f);

        Quaternion targetRotation = Quaternion.Euler(- targetAngle.x, targetAngle.y, 0);

        transform.localRotation = startRotation * targetRotation;
    }
}
*/
/*
using UnityEngine;
using System;

public class ArcheryTCP : MonoBehaviour
{
    public TCPData TCP;

    private string[] Degerler = new string[6];
    public string ModeName;
    public string StartStop;
    public float Q1;
    public float Q4;
    public float Q6;
    [SerializeField]
    private BowController Bow=null;
    [SerializeField]
    private ShootArrows Shootarrows=null;
    [SerializeField]
    private TargetSpawner target=null;

    private void OnApplicationQuit()
    {
        TCP.Connected = false;
    }
    private void Update()
    {
        if (TCP.UINetworkStream.DataAvailable)
        {
            Degerler = TCP.UIReader.ReadLine().Split('$');
            ModeName = Degerler[0];
            if (ModeName == "MainMenu")
            {
                StartCoroutine(SceneLoader.loadscene(ModeName));
                ModeName = string.Empty;
            }

            StartStop = Degerler[1];
            if (StartStop == "Start")
            {
                StartStop = string.Empty;
                target.SpawnTargets();
            }
            else if (StartStop == "Stop")
            {
                StartStop = string.Empty;

            }
            Debug.Log(Degerler[2]+"  "+Degerler[3]+"  "+Degerler[4]);
            Q1 = Convert.ToSingle(Degerler[2]);
            Q4 = Convert.ToSingle(Degerler[3]);
            Q6 = Convert.ToSingle(Degerler[4]);
            Shootarrows.arrowtrigger=-Q6*180/Mathf.PI;
            Bow.targetAngle.y=Q1;
            Bow.targetAngle.x=-(((Q4*180)/Mathf.PI)-90);
            



        }
    }
}

*/