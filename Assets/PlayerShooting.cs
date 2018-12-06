using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShooting : NetworkBehaviour {

    NetworkIdentity networkIdentity;
    NetworkManager netManager;

    [SerializeField] GunBase defaultGun;
    [SerializeField] GunBase[] guns;
    private GameObject equippedGunPrefab;
    public GunBase equippedGun;

    public List<GunBase> availableGuns;

    public bool processInput;

    public Text GunGui;

    public GameObject ToEnable;

    public override void OnStartLocalPlayer()
    {
        processInput = isLocalPlayer;
        netManager = FindObjectOfType<NetworkManager>();

        networkIdentity = GetComponent<NetworkIdentity>();

        print("Initializing guns for" + gameObject);
        availableGuns = new List<GunBase>();


        foreach (GunBase gun in guns)
        {
            availableGuns.Add(gun);
        }

        EquipGun();

        CmdInitializeGuns();
    }

    [Command]
    private void CmdInitializeGuns()
    {
        print("Initializing guns for" + gameObject);
        availableGuns = new List<GunBase>();


        foreach (GunBase gun in guns)
        {
            availableGuns.Add(gun);
        }

        EquipGun();
    }

    bool AfterShoot = false;

    // Update is called once per frame
    void Update () {

        if (AfterShoot)
        {
            ToEnable.SetActive(false);
            AfterShoot = false;
        }

        if (!processInput) { return; }


        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            print(equippedGun.ReadyToShoot);
            if (equippedGun.ReadyToShoot)
            {
                CmdShoot(netId);
                if (gunIndex == 0)
                    ToEnable.SetActive(true);
                AfterShoot = true;
            }
        }

        //Weapon swapping.
        else if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") > 0f)  // Forward
        {
            CmdNextWeapon();
        }
        else if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") < 0f) // backward
        {
            CmdPreviousWeapon();
        }
    }

    void GunTextChanged()
    {
        if (gunIndex == 0)
            GunGui.text = "Pea Gun";
        if (gunIndex == 1)
            GunGui.text = "Box Gun";
        if (gunIndex == 2)
            GunGui.text = "Explosion Gun";

    }

    [Command]
    public  void CmdShoot(NetworkInstanceId netID)
    {
        print("Reached? " + gameObject);
        print(EquippedGun);
        EquippedGun.Shoot();
    }

    private int gunIndex = 0;
    private Animator anim;

    public GunBase EquippedGun { get { return equippedGun; } }


    public void EquipGun()
    {
        //Todo activate/de-active meshes once we have them.

        //Looping
        if (gunIndex >= availableGuns.Count)
        {
            gunIndex = 0;
        }
        else if (gunIndex < 0)
        {
            gunIndex = availableGuns.Count - 1;
        }

        //Change equipped.
        equippedGun = availableGuns[gunIndex];
        GunTextChanged();


        //Change visible mesh
        foreach (GunBase gun in availableGuns)
        {
            if (gun != equippedGun)
            {
                foreach (MeshRenderer mr in gun.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }
            }
            else
            {
                foreach (MeshRenderer mr in gun.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = true;
                }
            }
        }
    }

    [Command]
    public void CmdNextWeapon()
    {
        ++gunIndex;
        EquipGun();
    }

    [Command]
    public void CmdPreviousWeapon()
    {
        --gunIndex;
        EquipGun();
    }
}
