public interface IPriceTable
{
    public int GetPrice(int level);
    public void OnValidate(int maxLevel);
}