using UnityEngine;

public class ChestPresenterFactory
{
    public ChestPresenter Create(ChestModel chestModel)
    {
        ChestPresenter chestPresenter = new ChestPresenter(chestModel);
        Debug.Log("ChestPresenterFactory: Created ChestPresenter!");
        return chestPresenter;
    }
}