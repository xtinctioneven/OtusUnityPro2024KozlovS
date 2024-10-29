using UnityEngine;

public class ChestTimerFactory
{
    public ChestTimer Create(ChestModel chestModel, ServerTimeService serverTimeService)
    {
        ChestTimer chestTimer = new ChestTimer(chestModel, serverTimeService);
        Debug.Log("ChestTimerFactory: Created ChestTimer!");
        return chestTimer;
    }
}