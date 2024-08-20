using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IUserInfoPresenter : IPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        ReactiveCommand CloseCommand { get; }
    }
}