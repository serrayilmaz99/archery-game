using UnityEngine;
using System;
/*
public class ArcheryTCP : MonoBehaviour
{
    //public TCPData TCP;

    private string[] Degerler = new string[6];
    public string ModeName;
    public string StartStop;
    public float Q1;
    public float Q4;
    public float Q6;
    [SerializeField]
    private BowController Bow = null;
    [SerializeField]
    private ShootArrows Shootarrows = null;
    [SerializeField]
    private TargetSpawner target = null;

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
            Debug.Log(Degerler[2] + "  " + Degerler[3] + "  " + Degerler[4]);
            Q1 = Convert.ToSingle(Degerler[2]);
            Q4 = Convert.ToSingle(Degerler[3]);
            Q6 = Convert.ToSingle(Degerler[4]);
            Shootarrows.arrowtrigger = -Q6 * 180 / Mathf.PI;
            Bow.targetAngle.y = Q1;
            Bow.targetAngle.x = -(((Q4 * 180) / Mathf.PI) - 90);




        }
    }
} */