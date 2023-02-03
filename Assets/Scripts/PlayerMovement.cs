// using System;
// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float speedModifier = 3f;
//
//     private Transform _transform;
//     private Vector3 _currentMovement;
//     private DefaultInputActions _playerInputs;
//
//     public void Movement ( InputAction.CallbackContext ctx )
//     {
//         _currentMovement = _playerInputs.Player.Move.ReadValue<Vector3>() * speedModifier;
//     }
//     
//     private void Awake()
//     {
//         _transform = transform;
//         _playerInputs = new DefaultInputActions();
//     }
//
//     private void FixedUpdate()
//     {
//         _transform.position = _transform.position + _currentMovement;
//     }
// }
