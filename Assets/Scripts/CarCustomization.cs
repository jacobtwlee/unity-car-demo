using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarCustomization : MonoBehaviour
{
    public MeshRenderer carMesh;
    public Material[] carColours = new Material[4];

    void Start() {
        Material[] materials = carMesh.materials;
        int playerIndex = gameObject.GetComponent<PlayerInput>().playerIndex;
        materials[0] = carColours[playerIndex];
        carMesh.materials = materials;
    }
}
