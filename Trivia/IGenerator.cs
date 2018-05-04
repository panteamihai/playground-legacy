namespace Trivia
{
    public interface IGenerator<out TResult>
    {
        TResult Generate();
    }
}
