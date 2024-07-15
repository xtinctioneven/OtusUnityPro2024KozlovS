using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Services.Analytics;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Unit : MonoBehaviour
    {
        public GameObject ThisGameObject { get { return _thisGameObject; } }
        public MoveComponent MoveComponent { get { return _moveComponent; } }
        public WeaponComponent WeaponComponent { get { return _weaponComponent; } }
        public TeamComponent TeamComponent { get { return _teamComponent; } }
        public HitPointsComponent HitPointsComponent { get { return _hitPointsComponent; } }

        protected GameObject _thisGameObject;
        protected MoveComponent _moveComponent;
        protected WeaponComponent _weaponComponent;
        protected TeamComponent _teamComponent;
        protected HitPointsComponent _hitPointsComponent;

        [Inject]
        protected virtual void Construct(
            GameObject thisGameObject,
            MoveComponent moveComponent,
            WeaponComponent weaponComponent,
            TeamComponent teamComponent,
            HitPointsComponent hitPointsComponent
            )
        {
            _thisGameObject = thisGameObject;
            _moveComponent = moveComponent;
            _weaponComponent = weaponComponent;
            _teamComponent = teamComponent;
            _hitPointsComponent = hitPointsComponent;
        }
    }
}
