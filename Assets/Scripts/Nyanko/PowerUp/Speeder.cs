public class Speeder
{
    public float SpeedUpNum { get; private set; } = 0;

    public void Initialize()
    {
        Singleton<GameManager>.Instance.PowerUpManager.SetSpeeder(this);
    }

    public void SpeedUp()
    {
        SpeedUpNum++;
        if(SpeedUpNum > 5)
        {
            SpeedUpNum = 5;
        }
    }

    public void Reset()
    {
        SpeedUpNum = 0;
    }
}
