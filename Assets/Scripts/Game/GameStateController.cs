public class GameStateController
{
    public enum GameStateEnum
    {
        Title,
        Game
    }

    public enum StageStateEnum
    {
        Normal,
        MiddleBoss,
        Boss
    }

    public GameStateEnum GameState { get; private set; } = GameStateEnum.Title;
    public StageStateEnum StageState { get; private set; } = StageStateEnum.Normal;

    public void ChangeGameState(GameStateEnum state)
    {
        GameState = state;
    }

    public void ChangeStageState(StageStateEnum state)
    {
        StageState = state;
    }
}
