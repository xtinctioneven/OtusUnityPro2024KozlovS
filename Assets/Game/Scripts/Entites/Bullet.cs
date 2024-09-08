using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

public class Bullet : AtomicEntity
{
    [Get(MoveAPI.MOVE_DIRECTION)]
    public IAtomicValue<Vector3> MoveDirection => _moveComponent.MoveDirection;

    [SerializeField] private int _damage = 1;
    [SerializeField] private MoveComponent _moveComponent;
    private bool _hasCollided = false;

    private void Awake()
    {
        _moveComponent.Compose();
    }

    private void Update()
    {
        _moveComponent.Update(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasCollided)
        {
            return;
        }
        _hasCollided = true;
        if (other.TryGetComponent(out IAtomicEntity atomicEntity))
        {
            if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION, out var damageAction))
            {
                damageAction.Invoke(_damage);
            }
        }
        Destroy(this.gameObject);
    }
}