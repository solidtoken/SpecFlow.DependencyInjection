using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
{
    public class Calculator : ICalculator, IDisposable
    {
        private readonly ISpecFlowOutputHelper _output;
        private readonly Stack<int> _operands = new Stack<int>();

        public Calculator(ISpecFlowOutputHelper output)
        {
            output.WriteLine("Calculator: ctor");
            _output = output;
        }

        public void Enter(int operand)
        {
            _output.WriteLine("Calculator: Enter");
            _operands.Push(operand);
            _output.WriteLine(ToString());
        }

        public void Add()
        {
            _output.WriteLine("Calculator: Add");
            _operands.Push(_operands.Pop() + _operands.Pop());
            _output.WriteLine(ToString());
        }

        public void Multiply()
        {
            _output.WriteLine("Calculator: Multiply");
            Thread.Sleep(1000);
            _operands.Push(_operands.Pop() * _operands.Pop());
            _output.WriteLine(ToString());
        }

        public int Result
        {
            get => _operands.Peek();
        }

        public int Size
        {
            get => _operands.Count;
        }

        public override string ToString() => "[" + String.Join(",", _operands) + "]";

        public void Dispose()
        {
            _output.WriteLine("Calculator: Dispose");
        }
    }
}
