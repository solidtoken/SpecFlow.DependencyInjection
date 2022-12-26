namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
{
    public interface ICalculator
    {
        void Enter(int operand);
        void Add();
        void Multiply();
        int Result { get; }
    }
}
