using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public abstract class UnitComponent
    {
        protected Unit _componentOwner;
        public Unit Unit { get { return _componentOwner; } }
        public CompositeCondition Condition { get; private set; } = new CompositeCondition();

        [Inject]
        private void Construct(Unit componentOwner)
        {
            _componentOwner = componentOwner;
        }
    }
}
