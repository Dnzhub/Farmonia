using GrassMan.Animation;
using GrassMan.Interactable;
using GrassMan.Inventory;
using GrassMan.Movement;
using GrassMan.Pools;
using UnityEngine;


namespace GrassMan.Controller
{
    public class PlayerControl : MonoBehaviour, IEntityController
    {

        PlayerController _playerInput;
        Bag _bag;
        IMover _mover;
        IAnimation _animator;
        [SerializeField] float moveSpeed = 5f;

        private void Awake()
        {

            _playerInput = new PlayerController();
            _bag = GetComponent<Bag>();
            _animator = new CharacterAnimation(GetComponentInChildren<Animator>());
            _mover = new MovementWithCharacterController(this, GetComponent<CharacterController>(), moveSpeed);
        }


        private void OnEnable()
        {
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Update()
        {

            Vector2 movementInput = _playerInput.Player.Move.ReadValue<Vector2>();
            if (_playerInput.Player.Reap.triggered)
            {
                _animator.ReapAnimation();

            }
            if (_animator.IsReap() || _animator.IsPickUp()) return;

            _animator.MoveAnimation(movementInput.sqrMagnitude);
            _mover.Tick(movementInput);
        }
        private void OnTriggerStay(Collider other)
        {
            Block block = other.GetComponent<Block>();
           
            if (block != null)
            {
                if (_playerInput.Player.Collect.triggered && _bag.currentCrop <= _bag.bagCapacity)
                {
                    _animator.PickAnimation();
                    BlockPool.Instance.SetBack(block);
                    _bag.PutToBag();                 
                }
            }
           
        }
        private void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _bag.DeliverToBarn();
            }
        }
    }
}
