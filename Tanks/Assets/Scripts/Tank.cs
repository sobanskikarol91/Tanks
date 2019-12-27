using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class Tank : MonoBehaviour
{
    private PCMovement movement;
    private PhotonView view;

    private void Awake()
    {
        movement = GetComponentInChildren<PCMovement>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
            movement.Execute();
    }
}