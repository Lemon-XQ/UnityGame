using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int MaxHP = 100;
    [SyncVar(hook="OnChangeHealth")]
    public int currentHP = 100;
    public Slider healthslider;
    public bool DestoryOnDeath=false;
    public NetworkStartPosition[] SpawnPoints;

    void Start()
    {
        SpawnPoints = GameObject.FindObjectsOfType<NetworkStartPosition>();
    }

    public void TakeDamage(int damage)
    {
        if (isServer == false) return;

        currentHP -= damage;
        if (currentHP <= 0)
        {
            if (DestoryOnDeath)
            {
                Destroy(this.gameObject);
                return;
            }
            currentHP = MaxHP;
            RpcRespawn();
        }        
    }

    public void OnChangeHealth(int health)
    {
        healthslider.value = (float)health / MaxHP;
    }

    [ClientRpc]//ClientRemoteProcessCall
    public void RpcRespawn()
    {
        if (isLocalPlayer == false) return;
        Vector3 spawnPos;
        if (SpawnPoints == null || SpawnPoints.Length == 0) spawnPos = Vector3.zero;
        else
            spawnPos = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
        transform.position = spawnPos;
    }

}
