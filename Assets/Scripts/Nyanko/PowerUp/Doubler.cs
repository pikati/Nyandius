public class Doubler
{
    public bool ValidDoubler { get; private set; } = false;

    public void ActivateDoubler()
    {
        ValidDoubler = true;
    }
}
