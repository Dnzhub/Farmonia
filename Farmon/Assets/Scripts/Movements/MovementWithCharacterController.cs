using GrassMan.Controller;
using UnityEngine;

namespace GrassMan.Movement
{
    public class MovementWithCharacterController : IMover
    {
        IEntityController _entityController;
        CharacterController _characterController;
        float _moveSpeed;
        Camera _mainCam;
       
        public MovementWithCharacterController(IEntityController entityController,CharacterController characterController,float moveSpeed)
        {
            _entityController = entityController;
            _characterController = characterController;
            _moveSpeed = moveSpeed;
            _mainCam = Camera.main;
        }

        public void Tick(Vector2 playerInput)
        {
            Vector3 move = (_mainCam.transform.forward * playerInput.y + _mainCam.transform.right * playerInput.x).normalized;
            move.y = 0f;
            if(playerInput.magnitude >= 0.1f)
            {
                //Rotate character where its direction
                float directionAngle = Mathf.Atan2(playerInput.x, playerInput.y) * Mathf.Rad2Deg;
                _entityController.transform.rotation = Quaternion.Euler(0f, directionAngle, 0f);
                
                //Move Character
                _characterController.Move(move * _moveSpeed * Time.deltaTime);
            }
          

        }
    }

}
